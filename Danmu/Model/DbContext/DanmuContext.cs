using System.Threading.Tasks;
using Danmu.Model.DataTable;
using Microsoft.EntityFrameworkCore;

namespace Danmu.Model.DbContext
{
    public class DanmuContext : BaseContext
    {
        public DanmuContext(DbContextOptions<DanmuContext> options) : base(options) { }

        public DbSet<DanmuTable> Danmu { get; set; }
        public DbSet<UserTable> User { get; set; }
        public DbSet<VideoTable> Video { get; set; }
        public DbSet<HttpClientCacheTable> HttpClientCache { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanmuTable>().Property(p => p.IsDelete).HasDefaultValue(false);
            modelBuilder.Entity<DanmuTable>().HasIndex(d => new {d.Vid, d.IsDelete});

            modelBuilder.Entity<HttpClientCacheTable>().HasIndex(h => h.Key).HasMethod("hash");
        }

        public async Task<int> ClearTable(string tableName)
        {
            return await Database.ExecuteSqlRawAsync($"TRUNCATE \"{tableName}\" RESTART IDENTITY;");
        }
    }
}
