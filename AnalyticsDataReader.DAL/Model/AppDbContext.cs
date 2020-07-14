using Microsoft.EntityFrameworkCore;

namespace AnalyticsDataReader.DAL.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnalyticalDataPoint> AnalyticalData { get; set; }

        public DbSet<AnalyticalMetaData> AnalyticalMetaData { get; set; }
    }
}
