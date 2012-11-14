using System;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.ServiceProcess;
using MetroMonitor.MonitoringService.Core;
using NDesk.Options;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace MetroMonitor.MonitoringService
{
    class Program
    {
        private const string SystemServiceName = ProjectInstaller.SystemServiceName;

        static void Main(string[] args)
        {
            if (RunningInInteractiveMode())
            {
                try
                {
                    RunInteractive(args);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    Environment.Exit(-1);
                }
            }
            else
            {
                ServiceBase.Run(new MonitorService());
            }
        }

        private static bool RunningInInteractiveMode()
        {
            return Environment.UserInteractive;
        }

        private static void RunInteractive(string[] args)
        {
            Action actionToTake = null;
            OptionSet optionSet = null;
            optionSet = new OptionSet
                            {
                                {
                                    "install|i",
                                    "Installs the service",
                                    key => actionToTake = () => AdminRequired(InstallAndStart, key)
                                    },
                                {
                                    "uninstall|u",
                                    "Uninstalls the service",
                                    key => actionToTake = () => AdminRequired(EnsureStoppedAndUninstall, key)
                                    },
                                {
                                    "start",
                                    "Starts the servce",
                                    key => actionToTake = () => AdminRequired(StartService, key)
                                    },
                                {
                                    "restart",
                                    "Restarts the service",
                                    key => actionToTake = () => AdminRequired(RestartService, key)
                                    },
                                {
                                    "stop",
                                    "Stops the service",
                                    key => actionToTake = () => AdminRequired(StopService, key)
                                    },
                                {
                                    "debug|d",
                                    "Runs in debug mode",
                                    key => actionToTake = () => RunInDebugMode()
                                    },
                                {
                                    "help|?|h",
                                    "Help about the command line interface",
                                    key => actionToTake = () => PrintUsage(optionSet)
                                    },
                            };

            try
            {
                if (args.Length == 0) // default to executing in debug mode 
                    args = new[] { "--debug" };

                optionSet.Parse(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                PrintUsage(optionSet);
                return;
            }

            if (actionToTake == null)
                actionToTake = () => PrintUsage(optionSet);

            actionToTake();
        }

        private static void PrintUsage(OptionSet optionSet)
        {
            Console.WriteLine(
                @"
" + SystemServiceName + @"
----------------------------------------
Copyright (C) {0} - Asa Carrington
----------------------------------------
Command line options:", DateTime.Now.Year);

            optionSet.WriteOptionDescriptions(Console.Out);
        }

        private static void RunInDebugMode()
        {
            ConfigureDebugLogging();

            Console.WriteLine("Running in debug");

            var deviceMonitor = new DeviceMonitor(ConfigurationManager.ConnectionStrings["MetroMonitorData"].ConnectionString);
            deviceMonitor.Initialise();
            deviceMonitor.Start();

            Console.WriteLine("Press <enter> to stop or 'cls' and <enter> to clear the log");
            while (true)
            {
                var command = Console.ReadLine() ?? "";
                switch (command)
                {
                    case "cls":
                        Console.Clear();
                        break;
                    default:
                        deviceMonitor.Stop();
                        return;
                }
            }

        }

        private static void ConfigureDebugLogging()
        {
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")))
                return;

            var loggerRepository = log4net.LogManager.GetRepository(typeof(MonitorService).Assembly);

            var patternLayout = new PatternLayout(PatternLayout.DefaultConversionPattern);
            var consoleAppender = new ConsoleAppender
            {
                Layout = patternLayout,
            };
            consoleAppender.ActivateOptions();

            ((Logger)loggerRepository.GetLogger(typeof(MonitorService).FullName)).AddAppender(consoleAppender);

            var fileAppender = new RollingFileAppender
            {
                AppendToFile = false,
                File = "activity.log",
                Layout = patternLayout,
                MaxSizeRollBackups = 3,
                MaximumFileSize = "1024KB",
                StaticLogFileName = true,
                LockingModel = new FileAppender.MinimalLock()
            };
            fileAppender.ActivateOptions();

            ((Hierarchy)loggerRepository).Root.AddAppender(fileAppender);

            loggerRepository.Configured = true;
        }

        #region Service Installation

        private static void AdminRequired(Action actionThatMayRequiresAdminPrivileges, string cmdLine)
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent() ?? WindowsIdentity.GetAnonymous());
            if (principal.IsInRole(WindowsBuiltInRole.Administrator) == false)
            {
                if (RunAgainAsAdmin(cmdLine))
                    return;
            }
            actionThatMayRequiresAdminPrivileges();
        }

        private static bool RunAgainAsAdmin(string cmdLine)
        {
            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    Arguments = "--" + cmdLine,
                    FileName = Assembly.GetExecutingAssembly().Location,
                    Verb = "runas",
                });
                process.WaitForExit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void EnsureStoppedAndUninstall()
        {
            if (!ServiceIsInstalled())
            {
                Console.WriteLine("Service is not installed");
                return;
            }
            var stopController = new ServiceController(SystemServiceName);

            if (stopController.Status == ServiceControllerStatus.Running)
                stopController.Stop();

            ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
        }

        private static void StopService()
        {
            if (!ServiceIsInstalled())
            {
                Console.WriteLine("Service is not installed");
                return;
            }
            var stopController = new ServiceController(SystemServiceName);

            if (stopController.Status != ServiceControllerStatus.Running) return;
            stopController.Stop();
            stopController.WaitForStatus(ServiceControllerStatus.Stopped);
        }

        private static void StartService()
        {
            if (!ServiceIsInstalled())
            {
                Console.WriteLine("Service is not installed");
                return;
            }
            var startController = new ServiceController(SystemServiceName);

            if (startController.Status == ServiceControllerStatus.Running) return;
            startController.Start();
            startController.WaitForStatus(ServiceControllerStatus.Running);
        }

        private static void RestartService()
        {
            if (!ServiceIsInstalled())
            {
                Console.WriteLine("Service is not installed");
                return;
            }
            var restartController = new ServiceController(SystemServiceName);

            if (restartController.Status == ServiceControllerStatus.Running)
            {
                restartController.Stop();
                restartController.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            if (restartController.Status == ServiceControllerStatus.Running) return;
            restartController.Start();
            restartController.WaitForStatus(ServiceControllerStatus.Running);
        }

        private static void InstallAndStart()
        {
            if (ServiceIsInstalled())
            {
                Console.WriteLine("Service is already installed");
            }
            else
            {
                ManagedInstallerClass.InstallHelper(new[] { Assembly.GetExecutingAssembly().Location });
                var startController = new ServiceController(SystemServiceName);
                startController.Start();
            }
        }

        private static bool ServiceIsInstalled()
        {
            return (ServiceController.GetServices().Count(s => s.ServiceName == SystemServiceName) > 0);
        }

        #endregion
    }
}
