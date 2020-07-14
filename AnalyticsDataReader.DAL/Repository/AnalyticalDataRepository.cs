using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticsDataReader.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsDataReader.DAL.Repository
{
    public class AnalyticalDataRepository : IRepository<AnalyticalDataPoint>, IAnalyticalDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public AnalyticalDataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<AnalyticalDataPoint>> GetDataAllAsync() => await _appDbContext.Set<AnalyticalDataPoint>().ToListAsync();


        public async Task<IEnumerable<AnalyticalDataPoint>> GetDataRangeAsync(DateTime startdate, DateTime enddate)
        {
            return await _appDbContext.Set<AnalyticalDataPoint>().Where(t => t.DateTime >= startdate && t.DateTime <= enddate).ToListAsync();
        }
        public async Task AddNewEntity(AnalyticalDataPoint analyticalData)
        {
            if (analyticalData.point.Equals(null))
            {
                throw new Exception("Data point is null.");
            }
            await _appDbContext.Set<AnalyticalDataPoint>().AddAsync(analyticalData).ConfigureAwait(false);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
