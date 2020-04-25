using System;
using System.Reactive.Disposables;
using Prism.Mvvm;

namespace Sample6.Utilities
{
    public abstract class BindableModelBase : BindableBase, IDisposable
    {
        protected CompositeDisposable _disposable { get; } = new CompositeDisposable();



        protected virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    this._disposable.Dispose();
                }
                this._isDisposed = true;
            }
        }
        public void Dispose()
            => this.Dispose(true);

        private bool _isDisposed = false; // 重複する呼び出しを検出するには
    }
}
