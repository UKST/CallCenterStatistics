using System.Collections.Generic;
using System.IO;
using System.Linq;
using CallCenterStatistics.BLL.DataReaders.Models;
using CsvHelper;

namespace CallCenterStatistics.BLL.DataReaders
{
    public class CallCenterDataReader : IDataReader<CallCenterData>
    {
        private const bool HasHeaderRecord = true;
        private const string Delimiter = ";";
        
        public ICollection<CallCenterData> Read(string file)
        {
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = HasHeaderRecord;
                csv.Configuration.Delimiter = Delimiter;
                return csv.GetRecords<CallCenterData>().ToArray();
            }
        }
    }
}
