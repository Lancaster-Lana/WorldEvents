using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorldEvents.Entities;

namespace WorldEvents.DBModel
{
    /// <summary>
    /// Data context (without secure data that in Identity Config)
    /// </summary>
    //[AutoRepositoryTypes(
    //typeof(IRepository<>),
    //typeof(IRepository<,>),
    //typeof(Repository<>),typeof(Repository<,>))]
    public class SatteliteDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> //AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventRegistration> EventRegistrations { get; set; }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsContent> NewsContents { get; set; }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectContent> ProjectContents { get; set; }
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

        #region Identity part
        /// <summary>
        /// Site application roles
        /// </summary>
        public DbSet<ApplicationRole> AppRoles { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        //public virtual DbSet<UsersInRoles> UsersInRoles { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        public virtual DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Project roles
        /// </summary>
        public virtual DbSet<CategoryPermission> CategoryPermissions { get; set; }
        public virtual DbSet<CategorySubscription> Subscriptions { get; set; }

        #endregion

        static SatteliteDbContext()
        {
            //System.Data.Entity.Database.SetInitializer(new SatteliteDBInitializer());// CreateDatabaseIfNotExists<SatteliteDbContext>());
            //Database.SetInitializer<SatteliteDBContext>(new Configuration<SatteliteDbContext>());
        }

        //public SatteliteDbContext() : this(CONSTS.DBDataConnectionString)
        //{
        //}

        public SatteliteDbContext(string connectionString) : this(GetOptions(connectionString))
        { 
            //    this.Configuration.LazyLoadingEnabled = true;
            //    this.Configuration.ProxyCreationEnabled = false;
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        //If using Microsoft.EntityFrameworkCore
        public SatteliteDbContext(DbContextOptions options)
            : base(options)
        {
            //options.FindExtension.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //TO BE MOVED to class UserMapping : EntityMappingBase<User>
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(e => e.UserProfile).HasForeignKey<ApplicationUser>(f => f.UserId)//Optional - to be created\filled up later
            //    .WithOne(e => e.User)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<IdentityUser>().ToTable("AppUsers");
            modelBuilder.Entity<ApplicationUser>().ToTable("AppUsers");
            modelBuilder.Entity<ApplicationRole>().ToTable("AppRoles");

            //   modelBuilder.ApplyConfiguration<Category>(new CategoryMapping()); //for Microsoft.EntityFrameworkCore

            //   modelBuilder.Configurations.Configurations.Add(new CategoryMapping());

            //    modelBuilder.Configurations.Add(new RoleMapping());
            //    modelBuilder.Configurations.Add(new CategoryPermissionMapping());

            //    modelBuilder.Configurations.Add(new UserMapping());
            //    modelBuilder.Configurations.Add(new CategorySubscriptionMapping());
            //    //modelBuilder.Configurations.Add(new UsersInRolesMapping());

            //    modelBuilder.Configurations.Add(new NewsMapping());
            //    modelBuilder.Configurations.Add(new NewsContentMapping());

            //    modelBuilder.Configurations.Add(new ProjectMapping());
            //    modelBuilder.Configurations.Add(new ProjectContentMapping());
            //    modelBuilder.Configurations.Add(new ProjectMemberMapping());
        }
    }
}