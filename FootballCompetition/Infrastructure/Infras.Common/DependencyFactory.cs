using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Infras.Common
{
    public class DependencyFactory
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/IoC.config") };
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("IoC");
            container.LoadConfiguration(unitySection);
            return container;
        }
    }
}
