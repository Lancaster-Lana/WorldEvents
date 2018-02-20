using Microsoft.EntityFrameworkCore;
//using Abp.Zero.EntityFramework;
//using System.Data.Entity;
using WorldEvents.Entities;

namespace WorldEvents.DBModel
{
    /// <summary>
    /// Data context (without secure data that in Identity Config)
    /// </summary>
    public class SatteliteDbContext : DbContext //AbpZeroDbContext<Tenant, Role, User>
    {
        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsContent> NewsContents { get; set; }

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectContent> ProjectContents { get; set; }
        public virtual DbSet<ProjectRole> ProjectRoles { get; set; }
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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