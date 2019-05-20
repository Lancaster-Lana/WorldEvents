using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;

namespace WorldEvents.EntityFramework
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule))]
    public class WorldEventsEntityFrameworkDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Init DB with secure information
            Configuration.DefaultNameOrConnectionString = CONSTS.DBDataConnectionString;

            //Init 2-data DB
            //Database.SetInitializer(new SatteliteDBInitializer());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
