using NavigationTree;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace NavigationTree
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("NaviTree", typeof(Views.NavigationTree));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
