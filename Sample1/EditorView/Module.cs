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
            containerRegistry.RegisterForNavigation<Views.PersonalEditor>();
            containerRegistry.RegisterForNavigation<Views.PhysicalEditor>();
            containerRegistry.RegisterForNavigation<Views.TestPointEditor>();
            containerRegistry.RegisterForNavigation<Views.CategoryPanel>();
        }
    }
}
