using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
         System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }
        public DbSet<VersionHistory> VersionHistories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Magzine> Magzines { get; set; }
        public DbSet<ContentType> ContentTypes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<ContentLike> ContentLikes { get; set; }
        public DbSet<ContentComment> ContentComments { get; set; }
        public DbSet<ContentGroup> ContentGroups { get; set; }
        public DbSet<ArAsset> ArAssets { get; set; }

    }
}
