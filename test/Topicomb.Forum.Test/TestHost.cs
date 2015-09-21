using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNet.Mvc.Actions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.TestHost;
using Microsoft.AspNet.Testing;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Testing;
using CodeComb.HtmlAgilityPack;
using Newtonsoft.Json;

namespace Topicomb.Forum.Test
{
    public class TestHost
    {
        public TestServer server { get; set; }
        public HttpClient client { get; set; }
        public WebSocketClient wsclient { get; set; }
        public TestHost()
        {
            var applicationServices = CallContextServiceLocator.Locator.ServiceProvider;
            var libraryManager = applicationServices.GetRequiredService<ILibraryManager>();

            var applicationEnvironment = applicationServices.GetRequiredService<IApplicationEnvironment>();
            
            var startupInstance = new Startup();
            var startupTypeInfo = startupInstance.GetType().GetTypeInfo();
            var configureApplication = (Action<IApplicationBuilder>)startupTypeInfo
                .DeclaredMethods
                .FirstOrDefault(m => m.Name == "Configure" && m.GetParameters().Length == 1)
                ?.CreateDelegate(typeof(Action<IApplicationBuilder>), startupInstance);
            if (configureApplication == null)
            {
                var configureWithLogger = (Action<IApplicationBuilder, ILoggerFactory>)startupTypeInfo
                    .DeclaredMethods
                    .FirstOrDefault(m => m.Name == "Configure" && m.GetParameters().Length == 2)
                    ?.CreateDelegate(typeof(Action<IApplicationBuilder, ILoggerFactory>), startupInstance);
                // Debug.Assert(configureWithLogger != null);

                configureApplication = application => configureWithLogger(application, NullLoggerFactory.Instance);
            }

            var buildServices = (Func<IServiceCollection, IServiceProvider>)startupTypeInfo
                .DeclaredMethods
                .FirstOrDefault(m => m.Name == "ConfigureServices" && m.ReturnType == typeof(IServiceProvider))
                ?.CreateDelegate(typeof(Func<IServiceCollection, IServiceProvider>), startupInstance);
            if (buildServices == null)
            {
                var configureServices = (Action<IServiceCollection>)startupTypeInfo
                    .DeclaredMethods
                    .FirstOrDefault(m => m.Name == "ConfigureServices" && m.ReturnType == typeof(void))
                    ?.CreateDelegate(typeof(Action<IServiceCollection>), startupInstance);
                // Debug.Assert(configureServices != null);

                buildServices = services =>
                {
                    configureServices(services);
                    return services.BuildServiceProvider();
                };
            }

            // RequestLocalizationOptions saves the current culture when constructed, potentially changing response
            // localization i.e. RequestLocalizationMiddleware behavior. Ensure the saved culture
            // (DefaultRequestCulture) is consistent regardless of system configuration or personal preferences.
            using (new CultureReplacer())
            {
                server = TestServer.Create(
                    CallContextServiceLocator.Locator.ServiceProvider,
                    configureApplication,
                    configureServices: InitializeServices(startupTypeInfo.Assembly, buildServices));
            }
            
            client = server.CreateClient();
            wsclient = server.CreateWebSocketClient();
        }
        
        protected HtmlDocument GetHtml(string source)
        {
            HtmlDocument html = new HtmlDocument();
            html.OptionFixNestedTags = true;
            html.OptionAutoCloseOnEnd = true;
            html.OptionDefaultStreamEncoding = Encoding.UTF8;
            html.LoadHtml(source ?? "");
            return html;
        }

        protected StringContent PostData(object @object)
        {
            return new StringContent(JsonConvert.SerializeObject(@object), Encoding.UTF8, "application/json");
        }
        
        protected virtual void AddAdditionalServices(IServiceCollection services)
        {
        }

        private Func<IServiceCollection, IServiceProvider> InitializeServices(
            Assembly startupAssembly,
            Func<IServiceCollection, IServiceProvider> buildServices)
        {
            var applicationServices = CallContextServiceLocator.Locator.ServiceProvider;
            var libraryManager = applicationServices.GetRequiredService<ILibraryManager>();

            // When an application executes in a regular context, the application base path points to the root
            // directory where the application is located, for example .../samples/MvcSample.Web. However, when
            // executing an application as part of a test, the ApplicationBasePath of the IApplicationEnvironment
            // points to the root folder of the test project.
            // To compensate, we need to calculate the correct project path and override the application
            // environment value so that components like the view engine work properly in the context of the test.
            var applicationName = startupAssembly.GetName().Name;
            var library = libraryManager.GetLibrary(applicationName);
            var applicationRoot = Path.GetDirectoryName(library.Path);

            var applicationEnvironment = applicationServices.GetRequiredService<IApplicationEnvironment>();

            return (services) =>
            {
                services.AddInstance<IApplicationEnvironment>(
                    new TestApplicationEnvironment(applicationEnvironment, applicationName, applicationRoot));

                var hostingEnvironment = new HostingEnvironment();
                hostingEnvironment.Initialize(applicationRoot, "testing");
                services.AddInstance<IHostingEnvironment>(hostingEnvironment);
                
                // Inject a custom assembly provider. Overrides AddMvc() because that uses TryAdd().
                var assemblyProvider = new StaticAssemblyProvider();
                assemblyProvider.CandidateAssemblies.Add(startupAssembly);
                services.AddInstance<IAssemblyProvider>(assemblyProvider);

                AddAdditionalServices(services);

                return buildServices(services);
            };
        }
    }    
}
