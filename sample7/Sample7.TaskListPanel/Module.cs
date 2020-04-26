using Prism.Ioc;
using Prism.Modularity;

namespace Sample7.TaskListPanel
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Navigation Regionを登録する
            containerRegistry.RegisterForNavigation<Views.TaskListPanel>();
        }
    }
}