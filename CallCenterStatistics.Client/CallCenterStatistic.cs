using System;
using CsvHelper.Configuration.Attributes;

namespace CallCenterStatistics.Client
{
    public class CallCenterStatistic
    {
        [Index(0)]
        public DateTime SessionStart { get; set; }
        [Index(1)]
        public DateTime SessionEnd { get; set; }
    }
}