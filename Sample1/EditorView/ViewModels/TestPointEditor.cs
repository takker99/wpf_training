using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace EditorView.ViewModels
{
    public class TestPointEditor : BindableBase, System.IDisposable
    {
        public TestPointEditor() { }

        void System.IDisposable.Dispose() => this._disposables.Dispose();
        private System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
