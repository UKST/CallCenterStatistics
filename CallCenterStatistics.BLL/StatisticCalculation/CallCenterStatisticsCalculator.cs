using System;
using System.Collections.Generic;
using System.Linq;
using CallCenterStatistics.BLL.DataReaders.Models;
using CallCenterStatistics.BLL.StatisticCalculation.Models;

namespace CallCenterStatistics.BLL.StatisticCalculation
{
    public class CallCenterStatisticsCalculator : IStatisticsCalculator
    {
        public IEnumerable<ActiveSessionsResultData> GetMaxActiveSessionsByDays(IEnumerable<CallCenterData> records)
        {
            var calculationData = ToCalculationData(records);

            var sortedCalculationData = calculationData.OrderBy(i => i.DateTime).ToArray();

            return GetMaxActiveSessionsByDaysInternal(sortedCalculationData);
        }

        public IEnumerable<OperatorAvailableStatusesResultData> GetTotalTimeThatOperatorSpentInAvailableStatuses(IEnumerable<CallCenterData> records)
        {
            var recordsArray = records.ToArray();
            var availableStates = recordsArray.Select(i => i.Status).Distinct().ToArray();

            return recordsArray.GroupBy(i => i.Operator).Select(group => new OperatorAvailableStatusesResultData
            {
                OperatorName = group.Key,
                DurationByStatuses = GetDurationByStatuses(availableStates, group)
            });
        }

        private static IEnumerable<ActiveSessionsCalculationData> ToCalculationData(IEnumerable<CallCenterData> records)
        {
            var calculationData = new List<ActiveSessionsCalculationData>();

            foreach (var record in records)
            {
                calculationData.Add(new ActiveSessionsCalculationData
                {
                    DateTime = record.SessionStart,
                    State = SessionState.Start
                });
                calculationData.Add(new ActiveSessionsCalculationData
                {
                    DateTime = record.SessionEnd,
                    State = SessionState.End
                });
            }

            return calculationData;
        }

        private static IEnumerable<ActiveSessionsResultData> GetMaxActiveSessionsByDaysInternal(ICollection<ActiveSessionsCalculationData> sortedCalculationData)
        {
            if(!sortedCalculationData.Any())
                yield break;
            
            var activeDay = sortedCalculationData.First().DateTime;
            var maxSessionsCount = 0;
            var currentSessionsCount = 0;
            
            foreach (var data in sortedCalculationData)
            {
                if (IsNewDay(activeDay, data.DateTime))
                {
                    yield return new ActiveSessionsResultData
                    {
                        Date = activeDay.Date,
                        MaxActiveSessionsCount = maxSessionsCount
                    };
                        
                    activeDay = data.DateTime;
                    maxSessionsCount = 0;
                    currentSessionsCount = 0;
                }

                if (data.State == SessionState.Start)
                    currentSessionsCount++;
                else
                    currentSessionsCount--;

                if (maxSessionsCount < currentSessionsCount)
                    maxSessionsCount = currentSessionsCount;
            }
            
            yield return new ActiveSessionsResultData
            {
                Date = activeDay.Date,
                MaxActiveSessionsCount = maxSessionsCount
            };
        }
        
        private static bool IsNewDay(DateTime activeDay, DateTime currentDateTime)
        {
            return activeDay.Date < currentDateTime.Date;
        }
        
        private static IEnumerable<DurationByStatus> GetDurationByStatuses(IEnumerable<string> availableStates, IEnumerable<CallCenterData> records)
        {
            var durationByStatus = availableStates.ToDictionary(key => key, element => TimeSpan.Zero);

            foreach (var record in records)
            {
                durationByStatus[record.Status] += TimeSpan.FromSeconds(record.Duration);
            }

            return durationByStatus.Select(i => new DurationByStatus
            {
                State = i.Key,
                Duration = i.Value
            });
        }
    }
}