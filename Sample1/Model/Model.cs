using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Model
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

        }

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
