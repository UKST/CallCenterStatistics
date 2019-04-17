using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CallCenterStatistics.BLL;
using CallCenterStatistics.BLL.DataReaders;
using CallCenterStatistics.BLL.DataReaders.Models;
using CallCenterStatistics.BLL.StatisticCalculation;
using CsvHelper;

namespace CallCenterStatistics.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
                throw new ArgumentException("Single arg expected. There should be call center data file name");
            
            var reader = new CallCenterDataReader();
            var records = reader.Read(args[0]);

            var calculator = new CallCenterStatisticsCalculator();

            foreach (var sessionsByDay in calculator.GetMaxActiveSessionsByDays(records))
            {
                Console.WriteLine($"{sessionsByDay.Date:dd.MM.yyyy} {sessionsByDay.MaxActiveSessionsCount}");   
            }
        }
    }
}
