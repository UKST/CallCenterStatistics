using System;
using CsvHelper.Configuration.Attributes;

namespace CallCenterStatistics.BLL.DataReaders.Models
{
    public class CallCenterData
    {
        [Index(0)]
        public DateTime SessionStart { get; set; }
        [Index(1)]
        public DateTime SessionEnd { get; set; }
        [Index(3)]
        public string Operator { get; set; }
        [Index(4)]
        public string Status { get; set; }
        [Index(5)]
        public int Duration { get; set; }
    }
}