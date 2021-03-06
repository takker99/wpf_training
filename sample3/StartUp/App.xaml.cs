using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;

namespace Sample3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => this.Container.Resolve<StartUp.Views.MainWindow>();

        // Modelを登録する
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
            => containerRegistry.RegisterInstance<Models.IStreamManager>(new Models.FileManager(this._filePath));

        // "...Views.hogehoge.xaml" という View の View Model を "...ViewModels.hogehoge.cs" に自動で設定する
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.
                SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                // "...StartUp.Views.MainWindow" を "...StartUp.ViewModels.MainWindow" に置き換える
                string viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
                string viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                string viewModelName = $"{viewName}, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }

        private readonly string _filePath = @"temp.txt";
    }
}
