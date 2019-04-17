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
    }
}