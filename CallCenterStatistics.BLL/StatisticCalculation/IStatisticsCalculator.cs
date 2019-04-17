using System.Collections.Generic;
using CallCenterStatistics.BLL.DataReaders.Models;
using CallCenterStatistics.BLL.StatisticCalculation.Models;

namespace CallCenterStatistics.BLL.StatisticCalculation
{
    public interface IStatisticsCalculator
    {
        IEnumerable<ActiveSessionsResultData> GetMaxActiveSessionsByDays(IEnumerable<CallCenterData> records);
    }
}