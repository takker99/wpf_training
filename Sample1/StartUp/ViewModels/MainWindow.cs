using Prism.Mvvm;
using Reactive.Bindings;

namespace Sample1.StartUp.ViewModels
{
    public class MainWindow : BindableBase, System.IDisposable
    {
        public ReactivePropertySlim<string> Title { get; }
        public MainWindow(Prism.Regions.IRegionManager regionManager)
        {
            // DI containerから、Region管理instanceを受け取る
            this._regionManager = regionManager;

            this.Title = new ReactivePropertySlim<string>("Prism Application");
        }

        // Windowを閉じるときに全てのViewModelsをDisposeする
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    this._disposables.Dispose();

                    foreach (var region in this._regionManager.Regions)
                    {
                        // 全てのRegionが持つViewを削除する
                        region.RemoveAll();
                    }
                }
                this._disposedValue = true;
            }
        }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            => this.Dispose(true);

        private bool _disposedValue = false; // 重複する呼び出しを検出するには
        private readonly Prism.Regions.IRegionManager _regionManager = null;
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
