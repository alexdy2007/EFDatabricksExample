using EFBricks.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace EFBricks.API.Database
{
public class EFBricksDbContext : DbContext
    {
        public DbSet<BronzeAssetEntity> BronzeAsset { get; set;}

        public string DbFullPath { get; set;}
        private readonly IConfiguration _config;

        public EFBricksDbContext(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            string dbPath = _config["connectionStrings:fakeSQLitePath"];
            string dbName = _config["connectionStrings:dbName"];
            DbFullPath = System.IO.Path.Join(dbPath,dbName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbFullPath}");
    }
    
}