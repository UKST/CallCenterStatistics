using System;

namespace CallCenterStatistics.BLL.StatisticCalculation.Models
{
    public class DurationByStatus
    {
        public string State { get; set; }
        public TimeSpan Duration { get; set; }
    }
}