using System;

namespace CallCenterStatistics.BLL.StatisticCalculation.Models
{
    public class ActiveSessionsCalculationData
    {
        public DateTime DateTime { get; set; }
        public SessionState State { get; set; }
    }
}