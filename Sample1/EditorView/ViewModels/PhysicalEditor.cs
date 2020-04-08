using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace EditorView.ViewModels
{
    public class PhysicalEditor : BindableBase, System.IDisposable
    {
        public PhysicalEditor() { }

        void System.IDisposable.Dispose() => this._disposables.Dispose();
        private System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
