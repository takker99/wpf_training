using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;

namespace Sample2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => this.Container.Resolve<Views.MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

        // 手動でViewとViewModel を関連付ける
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<Views.MainWindow, ViewModels.MainWindow>();
            ViewModelLocationProvider.Register<Views.Operand, ViewModels.Operand>();
            ViewModelLocationProvider.Register<Views.Answer, ViewModels.Answer>();
        }
    }
}
