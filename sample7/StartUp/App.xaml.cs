using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;

namespace Sample7.StartUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => this.Container.Resolve<Views.MainWindow>();

        /// <summary>
        /// DI containgerにobjectsを登録する
        /// </summary>
        /// <param name="containerRegistry">DI container</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<Setting.IApplicationSetting,Setting.ApplicationSetting>();
            containerRegistry.Register<Services.IService, Services.Service>();
        }

        /// <summary>
        /// Moduleを登録する
        /// </summary>
        /// <param name="moduleCatalog">登録先catalog interface</param>
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // 名前の重複を防ぐために、名前空間を付け加えている
            moduleCatalog.AddModule<Services.Module>(nameof(Services)+nameof(Services.Module),InitializationMode.WhenAvailable);
            moduleCatalog.AddModule<HamburgerMenuService.Module>(nameof(HamburgerMenuService)+nameof(HamburgerMenuService.Module),InitializationMode.WhenAvailable);

            // child windows
            moduleCatalog.AddModule<Regions.Module>(nameof(Regions)+nameof(Regions.Module),InitializationMode.WhenAvailable);
            //moduleCatalog.AddModule<TaskListPanel.Module>(nameof(TaskListPanel)+nameof(TaskListPanel.Module),InitializationMode.WhenAvailable);
        }

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
