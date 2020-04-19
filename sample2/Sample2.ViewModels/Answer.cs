using Prism.Mvvm;
using Reactive.Bindings;

namespace Sample2.ViewModels
{
    public class Answer : BindableBase
    {
        public ReactivePropertySlim<string> Text { get; }
    }
}
