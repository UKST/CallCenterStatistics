using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CallCenterStatistics.BLL;
using CallCenterStatistics.BLL.DataReaders;
using CallCenterStatistics.BLL.DataReaders.Models;
using CallCenterStatistics.BLL.StatisticCalculation;
using CallCenterStatistics.BLL.StatisticCalculation.Models;
using CsvHelper;

namespace CallCenterStatistics.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            if(args.Length != 1)
                throw new ArgumentException("Single arg expected. There should be call center data file name");
            
            var reader = new CallCenterDataReader();
            var records = reader.Read(args[0]);

            var calculator = new CallCenterStatisticsCalculator();

            foreach (var sessionsByDay in calculator.GetMaxActiveSessionsByDays(records))
            {
                Console.WriteLine($"{sessionsByDay.Date:dd.MM.yyyy} {sessionsByDay.MaxActiveSessionsCount}");   
            }

            var operatorStatuses = calculator.GetTotalTimeThatOperatorSpentInAvailableStatuses(records).ToArray();
            if (!HasDurationsByStatuses(operatorStatuses)) return;

            PrintHeader(operatorStatuses);
            
            foreach (var operatorStatus in operatorStatuses)
            {
                Console.Write($"{operatorStatus.OperatorName} ");
                
                foreach (var durationByStatus in operatorStatus.DurationByStatuses)
                {
                    Console.Write($"{durationByStatus.Duration} ");
                }
                
                Console.WriteLine();
            }
        }

        private static void PrintHeader(ICollection<OperatorAvailableStatusesResultData> operatorStatuses)
        {
            if (!HasDurationsByStatuses(operatorStatuses)) return;
            
            Console.Write("Operator ");
            foreach (var durationByState in operatorStatuses.First().DurationByStatuses)
            {
                Console.Write($"{durationByState.State} ");
            }
            
            Console.WriteLine();
        }
        
        private static bool HasDurationsByStatuses(ICollection<OperatorAvailableStatusesResultData> operatorStatuses)
        {
            return operatorStatuses.Any() && operatorStatuses.First().DurationByStatuses.Any();
        }
    }
}
