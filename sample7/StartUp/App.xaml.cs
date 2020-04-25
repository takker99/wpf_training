using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;
using Takker.Services;

namespace Sample7.StartUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => this.Container.Resolve<Views.MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
            => containerRegistry.Register<IService, Service>();

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) 
            => _ = moduleCatalog.AddModule<Takker.Services.Module>();

        // "...Views.hogehoge.xaml" という View の View Model を "...ViewModels.hogehoge.cs" に自動で設定する
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.
                SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                // "...Views.MainWindow" を "...ViewModels.MainWindow" に置き換える
                string viewName = viewType.FullName.Replace(".Views.",".ViewModels.");
                string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                string viewModelName = $"{viewName}, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }
    }
}
