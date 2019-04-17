using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace CallCenterStatistics.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("export_ohbffedq_969386035.csv"))
            using (var csv = new CsvReader(reader))
            {    
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.Delimiter = ";";
                var records = csv.GetRecords<CallCenterStatistic>();

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

                var sortedCalculationData = calculationData.OrderBy(i => i.DateTime).ToArray();

                var activeDay = sortedCalculationData.First().DateTime;
                var maxSessionsCount = 0;
                var currentSessionsCount = 0;
                foreach (var data in sortedCalculationData)
                {
                    if (IsNewDay(activeDay, data.DateTime))
                    {
                        Console.WriteLine($"{activeDay.Date} {maxSessionsCount}");
                        
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
            }
        }

        private static bool IsNewDay(DateTime activeDay, DateTime currentDateTime)
        {
            return activeDay.Date < currentDateTime.Date;
        }
    }
}
