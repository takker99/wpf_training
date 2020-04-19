using Prism.Mvvm;
using Reactive.Bindings;

namespace Sample2.ViewModels
{
    public class MainWindow : BindableBase
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>("Hello, Prism!");
    }
}
