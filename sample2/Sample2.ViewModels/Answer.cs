using System.Windows.Forms;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sample2.Extensions;

namespace Sample2.ViewModels
{
    public class Answer : BindableBase,System.IDisposable
    {
        public ReactivePropertySlim<string> Text { get; }
        public ReactiveCommand<object> ShowDialogCommand { get; }

        public Answer(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this.Text = new ReactivePropertySlim<string>("4").AddTo(this._disposables);
            this._dialogService = dialogService;
            this.ShowDialogCommand
                = new ReactiveCommand()
                .WithSubscribe(() => this._dialogService.ShowConfirmationMessage($"N^2 = {this.Text.Value}"));

            eventAggregator.
               GetEvent<PubSubEvent<double>>()
               .Subscribe(operand =>
               {
                   this.Text.Value = (operand * operand).ToString();
               },true)
               .AddTo(this._disposables);
        }

        void System.IDisposable.Dispose() => this._disposables.Dispose();

        private IDialogService _dialogService;
        // ゴミ箱
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
