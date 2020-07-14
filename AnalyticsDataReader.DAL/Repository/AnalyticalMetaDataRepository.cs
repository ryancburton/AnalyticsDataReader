using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticsDataReader.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsDataReader.DAL.Repository
{
    public class AnalyticalMetaDataRepository : IRepository<AnalyticalMetaData>, IAnalyticalMetaDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public AnalyticalMetaDataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<AnalyticalMetaData>> GetDataAllAsync() => await _appDbContext.Set<AnalyticalMetaData>().ToListAsync();


        public async Task<IEnumerable<AnalyticalMetaData>> GetDataRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _appDbContext.Set<AnalyticalMetaData>().Where(t => t.DateOfUpload >= startDate && t.DateOfUpload <= endDate).ToListAsync();
        }
        public async Task AddNewEntity(AnalyticalMetaData analyticalData)
        {
            await _appDbContext.Set<AnalyticalMetaData>().AddAsync(analyticalData).ConfigureAwait(false);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
