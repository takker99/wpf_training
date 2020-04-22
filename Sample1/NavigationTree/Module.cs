using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Sample1.NavigationTree
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegionManager regionManager = containerProvider.Resolve<IRegionManager>();
            _ = regionManager.RegisterViewWithRegion("NaviTree", typeof(Views.NavigationTree));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
