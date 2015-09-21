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
        
        public void ConfigureServices(IServiceCollection services)
        {  
            var _services = services.BuildServiceProvider();
            var appEnv = _services.GetRequiredService<IApplicationEnvironment>(); 
            var env = _services.GetRequiredService<IHostingEnvironment>();
            
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
            Configuration = builder.Build();
            
            switch(Configuration["Database:Mode"])
            {
                case "SQLite":
                    services.AddEntityFramework()
                        .AddSqlite()
                        .AddDbContext<ForumContext> (x => x.UseSqlite(Configuration["Database:ConnectionString"].Replace("{AppRoot}", appEnv.ApplicationBasePath)));
                    break;
                case "SQLServer":
                    services.AddEntityFramework()
                        .AddSqlServer()
                        .AddDbContext<ForumContext> (x => x.UseSqlServer(Configuration["Database:ConnectionString"].Replace("{AppRoot}", appEnv.ApplicationBasePath)));
                    break;
                case "InMemory":
                    services.AddEntityFramework()
                        .AddInMemoryDatabase()
                        .AddDbContext<ForumContext> (x => x.UseInMemoryDatabase());
                        break;
                default:
                    throw new DatabaseNotSupportedException(Configuration["Database:Mode"]);
            }
            
            services.AddIdentity<User, IdentityRole<long>>(x => 
            {
                x.Password.RequiredLength = Convert.ToInt32(Configuration["Password:RequiredLength"]);
                x.Password.RequireDigit = Convert.ToBoolean(Configuration["Password:RequireDigit"]);
                x.Password.RequireLowercase = Convert.ToBoolean(Configuration["Password:RequireLowercase"]);
                x.Password.RequireNonLetterOrDigit = Convert.ToBoolean(Configuration["Password:RequireNonLetterOrDigit"]);
                x.Password.RequireUppercase = Convert.ToBoolean(Configuration["Password:RequireUppercase"]);
                if (!Convert.ToBoolean(Configuration["Password:AllowedUserNameCharacters"]))
                    x.User.AllowedUserNameCharacters = null;
            })
                .AddEntityFrameworkStores<ForumContext, long>()
                .AddDefaultTokenProviders();
                
            services.AddMvc()
                .AddTemplate();
                
            services.AddCurrentUser<long, User>();
            services.AddCodeCombLocalizationJsonDictionary();
        }

        public async void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseIdentity();
            app.UseStaticFiles();
            
            app.UseMvc(router =>
            {
                router.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCodeCombLocalization("/assets/shared/localization.js");

            await SampleData.InitDB(app.ApplicationServices);
        }
    }
}
