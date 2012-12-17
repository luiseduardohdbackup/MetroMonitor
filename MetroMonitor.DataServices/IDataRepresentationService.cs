using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetroMonitor.ViewModels.Statistics;
using System.Threading.Tasks;
using MetroMonitor.Entities;

namespace MetroMonitor.DataServices
{
    public interface IDataRepresentationService
    {
        //Graph GetGraphOverviewView(GraphMetaData graph);
        //GraphData GetGraphCounterView(int id, string deviceName);
        //GraphCreate GetGraphIndexView();
        //GraphMetaData GetGraphMetaDataView(int id);
        GraphData GetSystemOverviewGraph(int deviceId);
        List<Result> GetResultSet(int deviceId);
     
    }
}
