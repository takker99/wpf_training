using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

namespace Sample2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
            => this.Container.Resolve<Views.MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry) 
            => containerRegistry.RegisterDialog<Views.MessageBox, ViewModels.MessageBox>();

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
