using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Dnx.Runtime;
using Topicomb.Forum.Models;
using Topicomb.Forum.Exceptions;

namespace Topicomb.Forum
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }
        
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {  
            var appEnv = services.BuildServiceProvider().GetRequiredService<IApplicationEnvironment>(); 

            switch(Configuration["Database.Mode"])
            {
                case "SQLite":
                    services.AddEntityFramework()
                        .AddSqlite()
                        .AddDbContext<ForumContext> (x => x.UseSqlite(Configuration["Database.ConnectionString"].Replace("{AppRoot}", appEnv.ApplicationBasePath)));
                    break;
                case "SQLServer":
                    services.AddEntityFramework()
                        .AddSqlServer()
                        .AddDbContext<ForumContext> (x => x.UseSqlServer(Configuration["Database.ConnectionString"].Replace("{AppRoot}", appEnv.ApplicationBasePath)));
                    break;
                default:
                    throw new DatabaseNotSupportedException(Configuration["Database.Mode"]);
            }
            
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ForumContext>()
                .AddDefaultTokenProviders();
                
            services.AddMvc()
                .AddTemplate();
                
            services.AddCurrentUser<long, User>();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            
            app.UseMvc(router =>
            {
                router.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            await SampleData.InitDB(app.ApplicationServices);
        }
    }
}
