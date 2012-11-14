using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroMonitor.ViewModels.Counters
{
    public class CounterBase
    {
        public string Description { get; set; }
        [Required(ErrorMessage = "You forgot to enter a {0}")]
        [DisplayName("Log Interval (sec)")]
        public int LogInterval { get; set; }
        [DisplayName("Read Interval (sec)")]
        [Required(ErrorMessage = "You forgot to enter a {0}")]
        public int ReadInterval { get; set; }
        [Required(ErrorMessage = "You forgot to enter a {0}")]
        [DisplayName("Max Threshold")]
        public int MaxThreshold { get; set; }
        [Required(ErrorMessage = "You forgot to enter a {0}")]
        [DisplayName("Min Threshold")]
        public int MinThreshold { get; set; }
    }
}
