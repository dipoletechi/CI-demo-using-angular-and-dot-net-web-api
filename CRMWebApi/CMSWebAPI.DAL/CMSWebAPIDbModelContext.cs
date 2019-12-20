using Microsoft.EntityFrameworkCore;

using CMSWebAPI.Models.DbModels;

namespace CMSWebAPI.DAL
{
    public class CMSWebAPIDbModelContext : DbContext
    {
        public CMSWebAPIDbModelContext(DbContextOptions<CMSWebAPIDbModelContext> options) :base(options)
        {
            
        }

        public CMSWebAPIDbModelContext() 
        {
           
        }
        public DbSet<AccessToken> AccessToken { get; set; }              
        public DbSet<User> Users { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Novel> Novels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);            
        }
    }
}
