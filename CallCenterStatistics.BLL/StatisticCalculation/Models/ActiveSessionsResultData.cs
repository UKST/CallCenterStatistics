using System;

namespace CallCenterStatistics.BLL.StatisticCalculation.Models
{
    public class ActiveSessionsResultData
    {
        public DateTime Date { get; set; }
        public long MaxActiveSessionsCount { get; set; }
    }
}