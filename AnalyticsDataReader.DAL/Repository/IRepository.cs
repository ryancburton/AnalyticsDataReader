using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace AnalyticsDataReader.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetDataAllAsync();
        Task<IEnumerable<T>> GetDataRangeAsync(DateTime startdate, DateTime enddate);
        Task AddNewEntity(T entity);
    }
}
