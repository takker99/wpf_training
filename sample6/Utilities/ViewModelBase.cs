using System;
using System.Reactive.Disposables;

using Prism.Mvvm;
using Prism.Regions;

namespace Sample6.Utilities
{
    /// <summary>
    /// ViewModelのDisposeの処理など、
    /// 全てのViewModelに共通する処理をまとめたクラス
    /// </summary>
    public class ViewModelBase : BindableBase, IDisposable
    {
        #region プロパティ

        // Trash box
        protected CompositeDisposable _disposable { get; } = new CompositeDisposable();

        public IRegionManager regionManager { get; } = null;

        #endregion

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this._disposable.Dispose();

                // Region上のViewを破棄
                if (this.regionManager is null)
                {
                    return;
                }
                foreach (var region in this.regionManager.Regions)
                {
                    region.RemoveAll();
                }
            }
            this._isDisposed = true;
        }

        public void Dispose()
            => this.Dispose(true);

        private bool _isDisposed = false; // 重複する呼び出しを検出
        #endregion

        public ViewModelBase() { }

        // Regionを使うときはこちらのconstructorを用いる
        public ViewModelBase(IRegionManager regionManager) : this()
            => this.regionManager = regionManager;

    }
}
