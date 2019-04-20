using System.Collections.Generic;

namespace CallCenterStatistics.BLL.StatisticCalculation.Models
{
    public class OperatorAvailableStatusesResultData
    {
        public string OperatorName { get; set; }
        public IEnumerable<DurationByStatus> DurationByStatuses { get; set; }
    }
}