using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;

namespace Sample1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    // 起動 parameter から読み込むファイルパスを取り出す
        //    if (e.Args.Length == 1) { this._dataFilePath = e.Args[0]; }

        //    base.OnStartup(e);
        //}

        protected override Window CreateShell()
            => this.Container.Resolve<StartUp.Views.MainWindow>();

        // データをDI container に登録する
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
            => containerRegistry.RegisterInstance(Services.DataLoader.Load(this._dataFilePath));

        // "...Views.hogehoge.xaml" という View の View Model を "...ViewModels.hogehoge.cs" に自動で設定する
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                // "...Main.Views.MainWiw" を "...Main.ViewModels.MainWindow" に置き換える
                var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = $"{viewName}, {viewAssemblyName}";
                return Type.GetType(viewModelName);
            });
        }

        // moduleを追加する
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<NavigationTree.Module>(nameof(NavigationTree)+nameof(NavigationTree.Module),InitializationMode.WhenAvailable);
            moduleCatalog.AddModule<EditorView.Module>(nameof(EditorView)+nameof(NavigationTree.Module),InitializationMode.WhenAvailable);
        }

        private string _dataFilePath = String.Empty;
    }
}
