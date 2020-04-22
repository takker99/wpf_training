using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.EditorView.ViewModels
{
    public class CategoryPanel : BindableBase, System.IDisposable
    {
        public CategoryPanel() { }

        void System.IDisposable.Dispose() => this._disposables.Dispose();
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
