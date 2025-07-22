using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyFirstBlog.Entities;

namespace MyFirstBlog.Entities
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public DbSet<Post> Posts { get; set; }
    }
}

