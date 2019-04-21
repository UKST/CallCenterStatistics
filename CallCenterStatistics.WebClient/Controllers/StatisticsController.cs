using System.Data;
using System.IO;
using CallCenterStatistics.BLL.DataReaders;
using CallCenterStatistics.BLL.DataReaders.Models;
using CallCenterStatistics.BLL.StatisticCalculation;
using Microsoft.AspNetCore.Mvc;

namespace CallCenterStatistics.WebClient.Controllers
{
    public class StatisticsController : Controller
    {
        private IStatisticsCalculator _statisticsCalculator;
        private IDataReader<CallCenterData> _dataReader;
        
        public StatisticsController(
            IStatisticsCalculator statisticsCalculator,
            IDataReader<CallCenterData> dataReader)
        {
            _statisticsCalculator = statisticsCalculator;
            _dataReader = dataReader;
        }
        
        public IActionResult MaxActiveSessionsByDays(string reportFileName)
        {
            var fullPath = Path.GetTempPath() + reportFileName;
            var data = _dataReader.Read(fullPath);
            var result = _statisticsCalculator.GetMaxActiveSessionsByDays(data);
            return View(result);
        }
    }
}