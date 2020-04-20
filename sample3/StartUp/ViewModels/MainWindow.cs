using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample3.StartUp.ViewModels
{
    public class MainWindow : BindableBase
    {
        public ReactivePropertySlim<string> Text;
        public ReactiveCommand SaveTextCommand;

        public MainWindow()
        {
            this.Text = new ReactivePropertySlim<string>("Write any text here!")
                .AddTo(this._disposable);
        }

        private System.Reactive.Disposables.CompositeDisposable _disposable 
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
