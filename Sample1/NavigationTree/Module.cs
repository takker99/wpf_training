using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Sample1.NavigationTree
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) 
            => containerProvider.Resolve<IRegionManager>()
            .RegisterViewWithRegion("NaviTree", typeof(Views.NavigationTree));

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
