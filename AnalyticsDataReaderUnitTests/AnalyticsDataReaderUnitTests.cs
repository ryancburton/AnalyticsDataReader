using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AnalyticsDataReader.DAL.Model;
using AnalyticsDataReader.DAL.Repository;
using System.Linq;

namespace AnalyticsDataReaderUnitTests
{
    [TestClass]
    public sealed class StockTickerUnitTests : IDisposable
    {
        private AppDbContext _appDbContext;

        public StockTickerUnitTests()
        {
            //Database should be mocked in Memory db
            //Calling Controller would be more of an integration test than Unit test
            InitContext();
        }

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("AppDbContextDB");
            _appDbContext = new AppDbContext(builder.Options);

            AnalyticalDataPoint analyticalDataPoint = new AnalyticalDataPoint
            {
                DateTime = DateTime.Now,
                point = 123.456m
            };

            if (!_appDbContext.AnalyticalData.Any())
            {
                _appDbContext.AnalyticalData.Add(analyticalDataPoint);
                _appDbContext.AnalyticalData.Add(analyticalDataPoint);
                _appDbContext.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task FindPointsAsync()
        {
            var AnalyticalDataRepository = new AnalyticalDataRepository(_appDbContext);

            var point = await AnalyticalDataRepository.GetDataAllAsync();//.Result.FirstOrDefault<AnalyticalDataPoint>();

            Assert.AreEqual(123.456m, point.FirstOrDefault().point);
        }

        [TestMethod]
        public async Task AddNewPoint()
        {
            var AnalyticalDataRepository = new AnalyticalDataRepository(_appDbContext);

            AnalyticalDataPoint analyticalDataPoint = new AnalyticalDataPoint
            {
                DateTime = DateTime.Now,
                point = 333.444m
            };

            await AnalyticalDataRepository.AddNewEntity(analyticalDataPoint).ConfigureAwait(false);
            var point = await AnalyticalDataRepository.GetDataAllAsync();//.Result.FirstOrDefault<AnalyticalDataPoint>();

            Assert.AreEqual(analyticalDataPoint.point, point.Last().point);
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
