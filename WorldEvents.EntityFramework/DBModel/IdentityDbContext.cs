using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using WorldEvents.Entities;

namespace WorldEvents.DBModel
{
    /// <summary>
    /// Identity context
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
       /// <summary>
        /// Site application roles
        /// </summary>
        public DbSet<ApplicationRole> AppRoles { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        //public virtual DbSet<UsersInRoles> UsersInRoles { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Project roles
        /// </summary>
        public virtual DbSet<CategoryPermission> CategoryPermissions { get; set; }
        public virtual DbSet<CategorySubscription> Subscriptions { get; set; }

        static IdentityDbContext()
        {
            //System.Data.Entity.Database.SetInitializer(new IdentityDBInitializer());
        }

        //public WorldEventDbContext()
        //    : base("IdentityConnection")
        //{

        //}

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //TO BE MOVED to class UserMapping : EntityMappingBase<User>
            builder.Entity<ApplicationUser>()
                .HasOne(e => e.UserProfile)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<IdentityUser>().ToTable("AppUsers");
            builder.Entity<ApplicationUser>().ToTable("AppUsers");
            //builder.Entity<ApplicationUser>()
            //    .HasOptional(e => e.UserProfile) //Optional - to be ceated\filled up later
            //    .WithRequired(e => e.User)
            //    .WillCascadeOnDelete();

            builder.Entity<ApplicationRole>().ToTable("AppRoles");
        }
    }
}
