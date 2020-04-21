using System;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample4.StartUp.ViewModels
{
    public class MainWindow : BindableBase
    {
        public ReactivePropertySlim<double> ProgressValue { get; }
        public ReactivePropertySlim<string> Message { get; }
        /// <summary>
        /// true:  処理を実行可能
        /// false: 処理を実行中
        /// </summary>
        public ReactivePropertySlim<bool> CanHeavyWork { get; }
        public AsyncReactiveCommand RunHeavyWorkCommand { get; }
        public ReactiveCommand CancelHeavyWorkCommand { get; }
        public MainWindow(Models.IHeavyWorker heavyWorker)
        {
            // DI containerからmodelを受け取る
            this._heavyWorker = heavyWorker;

            // ReactivePropertyの初期化
            this.ProgressValue = new ReactivePropertySlim<double>(0.0).AddTo(this._disposables);
            this.Message = new ReactivePropertySlim<string>("").AddTo(this._disposables);
            this.CanHeavyWork = new ReactivePropertySlim<bool>(true).AddTo(this._disposables); // 初期状態では実行可能

            this.RunHeavyWorkCommand
                = this.CanHeavyWork // 処理が実行可能ならば
                .ToAsyncReactiveCommand() // 処理を非同期で開始する
                .WithSubscribe(async () => await this._heavyWorker.HeavyWork(this._progressInfo))
                .AddTo(this._disposables);

            this.CancelHeavyWorkCommand
                = this.CanHeavyWork
                .Inverse() // 処理を実行中であれば
                .ToReactiveCommand() // 処理を中断する
                .WithSubscribe(() => this._heavyWorker.Cancel())
                .AddTo(this._disposables);

            this._progressInfo = new Progress<Models.ProgressInfo>(e =>
              {
                  this.ProgressValue.Value = e.Value;
                  this.Message.Value = e.Message;
              });
        }

        private readonly Models.IHeavyWorker _heavyWorker;
        private readonly Progress<Models.ProgressInfo> _progressInfo;
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable(); // ゴミ箱
    }
}
