using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;

using WorldEvents.DBModel;
using WorldEvents.Messaging;
using WorldEvents.Entities;

namespace WorldEvents
{
    public class Startup
    {
        string _testSecret = null;

        public IConfiguration Configuration { get; }
        IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();   // http://go.microsoft.com/fwlink/?LinkID=532709

                // This will push telemetry data through Application 
                // Insights pipeline faster, allowing you to view results 
                // immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //var options = new Authentication();
            //config.GetSection("Authentication").Bind(options);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//, IDistributedCache cache)
        {
            //Security settings
            _testSecret = Configuration["MySecret"];

            //INIT DB
            var identityConnection = Configuration.GetConnectionString("IdentityConnection"); //Configuration["Data:Identity:ConnectionString"];
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(identityConnection));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();

            string contentDBconnection = Configuration.GetConnectionString("DataConnection");//Configuration["Data:Sattelite:ConnectionString"];
            services.AddDbContext<SatteliteDbContext>(options => options.UseSqlServer(contentDBconnection)
             //.UseInMemoryDatabase//UseMemoryCache//UseLoggerFactory//UseSqlite
             );

            //INIT AUTHENTICATION with social networks
            services.AddAuthentication()

                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                })
                .AddTwitter(twOptions =>
                {
                    twOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                    twOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });

            services.Configure<FacebookOptions>(Configuration.GetSection("Facebook"));
            services.Configure<GoogleOptions>(Configuration.GetSection("Google"));

            services.AddMvc();

            //INIT Application Services
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //INIT Session
            services.AddSession();

            // If you want to tweak Identity cookies, they're no longer part of IdentityOptions.
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            //Configuration RedisCash
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });
            //var serverStartTimeString = DateTime.Now.ToString();
            //byte[] val = Encoding.UTF8.GetBytes(serverStartTimeString);
            //var cacheEntryOptions = new DistributedCacheEntryOptions()
            //    .SetSlidingExpiration(TimeSpan.FromSeconds(30));
            //cache.Set("lastServerStartTime", val, cacheEntryOptions);


        }
        /// </summary>
        /// Use SQL Server Cache in Production
        /// <ItemGroup>
        ///   <DotNetCliToolReference Include = "Microsoft.Extensions.Caching.SqlConfig.Tools" Version="1.0.0-msbuild3-final" />
        ///</ItemGroup>
        /// </summary>
        /// <param name="services"></param>
        /*public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = @"Data Source=/;Initial Catalog=DistCache;Integrated Security=True;";
                options.SchemaName = "dbo";
                options.TableName = "TestCache";
            });
        }*/

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //var result = string.IsNullOrEmpty(_testSecret) ? "Null" : "Not Null";
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Secret is {result}");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Role}/{action=Index}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
