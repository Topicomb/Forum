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
        public void ConfigureServices(IServiceCollection services)
        {  
            var _services = services.BuildServiceProvider();
            var appEnv = _services.GetRequiredService<IApplicationEnvironment>();
            IConfiguration Configuration;
            services.AddConfiguration(out Configuration);
            
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
                x.Password.RequiredLength = Convert.ToInt32(Configuration["Security:RequiredLength"]);
                x.Password.RequireDigit = Convert.ToBoolean(Configuration["Security:RequireDigit"]);
                x.Password.RequireLowercase = Convert.ToBoolean(Configuration["Security:RequireLowercase"]);
                x.Password.RequireNonLetterOrDigit = Convert.ToBoolean(Configuration["Security:RequireNonLetterOrDigit"]);
                x.Password.RequireUppercase = Convert.ToBoolean(Configuration["Security:RequireUppercase"]);
                x.User.RequireUniqueEmail = Convert.ToBoolean(Configuration["Security:RequireUniqueEmail"]);
                if (!Convert.ToBoolean(Configuration["Security:AllowedUserNameCharacters"]))
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

            app.UseIISPlatformHandler();
            app.UseIdentity();
            app.UseStaticFiles();
            
            app.UseMvc(router =>
            {
                router.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCodeCombLocalization("/assets/shared/scripts/localization.js");

            await SampleData.InitDB(app.ApplicationServices);
        }
    }
}
