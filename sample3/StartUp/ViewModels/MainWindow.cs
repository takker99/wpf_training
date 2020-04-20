using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sample3.StartUp.ViewModels
{
    public class MainWindow : BindableBase
    {
        // null合体代入演算子を用いた以下の記法については次を参照：
        // - https://qiita.com/okazuki/items/2f0832133eac4427b72c

        public ReactivePropertySlim<string> Text { get; }
        public AsyncReactiveCommand<object> SaveTextCommand { get; }

        public MainWindow(Models.IStreamManager streamManager)
        {
            // DI container から modelを受け取る
            this._streamManager = streamManager;

            this.Text = new ReactivePropertySlim<string>("Write any text here!")
            .AddTo(this._disposable);
            //this.Text=new ReactivePropertySlim<string>(await this._streamManager.ReadTextAsync())

            this.SaveTextCommand = new AsyncReactiveCommand()
                .WithSubscribe(async _ => await this._streamManager.WriteTextAsync(this.Text.Value).ConfigureAwait(false))
                .AddTo(this._disposable);

        }

        private System.Reactive.Disposables.CompositeDisposable _disposable 
            = new System.Reactive.Disposables.CompositeDisposable();

        private Models.IStreamManager _streamManager;
    }
}
