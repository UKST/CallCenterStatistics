using System.Collections.Generic;

namespace CallCenterStatistics.BLL.DataReaders
{
    public interface IDataReader<T>
    {
        ICollection<T> Read(string file);
    }
}