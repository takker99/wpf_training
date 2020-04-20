using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;

using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sample2.Extensions;

namespace Sample2.ViewModels
{
    public class Operand : BindableBase, System.IDisposable
    {
        // Validation機能を使うので、ReactivePropertySlimは使えない
        // Viewから値を変更できるようにするので、ReadOnlyReactivePropertyも使わない
        [Required, Range(-10000,10000)]
        public ReactiveProperty<string> Text { get; }
        public ReactiveCommand<object> ShowDialogCommand { get; }

        public Operand(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            // SetValidateAttribute:
            //
            // エラー検出を可能にする。検出条件式は
            // System.ComponentModel.DataAnnotations属性で指定する
            this.Text = new ReactiveProperty<string>("2")
                .SetValidateAttribute(() => this.Text)
                .AddTo(this._disposables);
            this._dialogService = dialogService;
            this.ShowDialogCommand
                = new ReactiveCommand()
                .WithSubscribe(() => this._dialogService.ShowConfirmationMessage($"N = {this.Text.Value}"));

            // ViewModel間でデータをやり取りするためにEventAggregatorを使う
            Observable
                .WithLatestFrom
                (
                    // 入力されたテキストと、直前に発行された入力値の
                    // エラー検出結果を一つにしてストリームに流す
                    this.Text,
                    this.Text.ObserveHasErrors,
                    (text, error) => (text, error)
                )
                // 入力値が正常なもののみ通す
                .Where(z => !z.error)
                // 数値に変換して、EventAggregatorで発行する。
                .Subscribe(z =>
                {
                    eventAggregator.GetEvent<PubSubEvent<double>>()
                                   .Publish(Double.Parse(z.text));
                })
                // ゴミ箱を指定
                .AddTo(this._disposables);
        }

        void System.IDisposable.Dispose() => this._disposables.Dispose();

        private IDialogService _dialogService;
        // observableのゴミ箱
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
