using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PA.Migrations;
using PA.Models.Entities;
using PA.Services.Configuration;
using PA.Services.MessageServices;

namespace PA.Models
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {        
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>();//(options =>options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            //.AddJsonOptions(options =>
            //{
            //    options.SerializerSettings.ContractResolver =
            //    new CamelCasePropertyNamesContractResolver();
            //});
            
            // Add application services.         
            services.AddTransient<IConfigurationRoot, AppSettings>();
            services.AddTransient<ApplicationDbContextSeeder>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IConfigurationRoot appSetting, ApplicationDbContextSeeder seeder)
        {
            loggerFactory.AddConsole(appSetting.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();
            
            app.UseDefaultFiles();

            app.UseStaticFiles();

            //TODO AUTH
            //app.UseIdentity();

            app.UseMvc();
            
            seeder.EnsureSeedData().Wait();            
        }


        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
