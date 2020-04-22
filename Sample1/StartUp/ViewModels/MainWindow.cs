using Prism.Mvvm;
using Reactive.Bindings;

namespace Sample1.StartUp.ViewModels
{
    public class MainWindow : BindableBase
    {
        public ReactivePropertySlim<string> Title { get; }
        public MainWindow()
        {
            this.Title = new ReactivePropertySlim<string>("Prism Application");
        }
    }
}
