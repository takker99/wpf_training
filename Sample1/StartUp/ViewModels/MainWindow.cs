using Prism.Mvvm;
using Reactive.Bindings;

namespace Sample1.StartUp.ViewModels
{
    public class MainWindow : BindableBase, System.IDisposable
    {
        public ReactivePropertySlim<string> Title { get; }
        public MainWindow()
        {
            this.Title = new ReactivePropertySlim<string>("Prism Application");
        }


        // Windowを閉じるときにViewModelsをDisposeする
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    this._disposables.Dispose();
                }
                this._disposedValue = true;
            }
        }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            => this.Dispose(true);

        private bool _disposedValue = false; // 重複する呼び出しを検出するには
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
