using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Sample7.Regions
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }


        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Navigation Regionを登録する
            containerRegistry.RegisterForNavigation<Views.StartUpPanel>();
        }
    }
}