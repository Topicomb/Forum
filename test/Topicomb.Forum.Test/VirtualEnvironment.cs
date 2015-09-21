using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Framework.Caching.Memory;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Identity.EntityFramework;
using Topicomb.Forum.Models;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Http;
using Xunit;

namespace Topicomb.Forum.Test
{
    public class VirtualEnvironment
    {
        protected IServiceProvider services;
        
        public VirtualEnvironment()
        {
            var services = new ServiceCollection();

            services.AddLogging();

            services.AddEntityFramework()
               .AddInMemoryDatabase()
               .AddDbContext<ForumContext> (x => x.UseInMemoryDatabase());
            
            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<ForumContext, long>()
                .AddDefaultTokenProviders();
                
            services.AddMvc()
                .AddTemplate();
                
            services.AddCodeCombLocalizationJsonDictionary();
            
            this.services = services.BuildServiceProvider();
            
            SampleData.InitDB(this.services).Wait();
        }
    }
}
