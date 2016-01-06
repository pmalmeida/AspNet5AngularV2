using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using PA.Models.Entities;

namespace PA.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        IConfigurationRoot _appSetting;
        public ApplicationDbContext(IConfigurationRoot appSetting)
        {
            _appSetting = appSetting;

            Database.EnsureCreated();
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

            #region IdentityUsers

            //builder.Entity<ApplicationUser>().HasKey(l => l.UserName);
            builder.Entity<IdentityUserLogin<string>>().HasKey(l => l.UserId);
            builder.Entity<IdentityUserRole<string>>().HasKey(l => new { l.RoleId,l.UserId });
            #endregion
        }


        #region Entities
        public DbSet<Contact> ContactSet { get; set; }

        public DbSet<Area> AreaSet { get; set; }

        public DbSet<ContactArea> ContactAreaSet { get; set; }

        public DbSet<Subject> SubjectSet { get; set; }
        #endregion


        #region Configuration      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = _appSetting["Data:DefaultConnection:ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }
        #endregion
    }
}
