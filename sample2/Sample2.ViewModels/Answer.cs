using Prism.Events;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample2.ViewModels
{
    public class Answer : BindableBase,System.IDisposable
    {
        public ReactivePropertySlim<string> Text { get; }

        public Answer(IEventAggregator eventAggregator)
        {
            this.Text = new ReactivePropertySlim<string>("4").AddTo(this._disposables);

            eventAggregator.
               GetEvent<PubSubEvent<double>>()
               .Subscribe(operand =>
               {
                   this.Text.Value = (operand * operand).ToString();
               },true)
               .AddTo(this._disposables);
        }

        void System.IDisposable.Dispose() => this._disposables.Dispose();

        // ÉSÉ~î†
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
