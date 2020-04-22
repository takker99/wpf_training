using Prism.Ioc;
using Prism.Modularity;

namespace Sample1.EditorView
{
    public class Module : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider) { }

        // DI containerにViewをRigion Navigationとして登録する
        public void RegisterTypes(IContainerRegistry containerRegistry) 
        {
            containerRegistry.RegisterForNavigation<ViewModels.PersonalEditor>();
            containerRegistry.RegisterForNavigation<ViewModels.PhysicalEditor>();
            containerRegistry.RegisterForNavigation<ViewModels.TestPointEditor>();
            containerRegistry.RegisterForNavigation<ViewModels.CategoryPanel>();
        }
    }
}
