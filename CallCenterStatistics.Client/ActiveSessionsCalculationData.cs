using System;

namespace CallCenterStatistics.Client
{
    public class ActiveSessionsCalculationData
    {
        public DateTime DateTime { get; set; }
        public SessionState State { get; set; }
    }
}