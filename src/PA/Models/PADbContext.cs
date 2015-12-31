using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using PA.Models.Entities;

namespace PA.Models
{

    public class PADbContext : DbContext
    {
        public PADbContext()
        {
            Database.EnsureCreated();
        }

        #region Configuration
        private IConfigurationRoot _Configuration;
        public IConfigurationRoot Configuration
        {
            get
            {
                if (_Configuration == null)
                {
                    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables();
                    _Configuration = builder.Build();
                }
                return _Configuration;
            }
        }
        #endregion

        #region Setup

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region ContactArea
            builder.Entity<ContactArea>().HasKey(t => new { t.IdContact, t.IdArea });

            builder.Entity<ContactArea>()
                .HasOne(ca => ca.Contact)
                .WithMany(c => c.ContactAreas)
                .HasForeignKey(ca => ca.IdContact);


            builder.Entity<ContactArea>()
                .HasOne(ca => ca.Area)
                .WithMany(c => c.ContactAreas)
                .HasForeignKey(ca => ca.IdArea);
            #endregion
        }

        #endregion

        #region Entities
        public DbSet<Contact> ContactSet { get; set; }

        public DbSet<Area> AreaSet { get; set; }

        public DbSet<ContactArea> ContactAreaSet { get; set; }

        public DbSet<Subject> SubjectSet { get; set; }
        #endregion
    }
}
