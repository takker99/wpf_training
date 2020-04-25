using Prism.Mvvm;
using Prism.Ioc;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows;
using Prism.Unity;
using Sample7.Services;
using System.Threading.Tasks;
using Sample7.Entities;
using Takker.Utilities;
using System.Windows.Navigation;
using System.Data;

namespace Sample7.StartUp.ViewModels
{
    public class MainWindow : ViewModelBase
    {
        /// <summary>コンソールの出力内容を取得します。</summary>
        public ReadOnlyReactivePropertySlim<DataTable> Console { get; }

        /// <summary>Dynamic型でキャラクターを取得します。</summary>
        public ReactiveCommand GetDynamic { get; }

        /// <summary>BleachCharacter型でキャラクターを取得します。</summary>
        public ReactiveCommand GetBleachCharacter { get; }

        /// <summary>護廷十三隊別にキャラクターを取得します。</summary>
        public ReactiveCommand GetCharacterByParty { get; }

        /// <summary>キャラクターを登録します。</summary>
        public AsyncReactiveCommand InsertCharacter { get; }

        /// <summary>コンソールをクリアします。</summary>
        public ReactiveCommand ClearConsole { get; }


        /// <summary>コンストラクタ。</summary>
        public MainWindow()
        {
            //全てのcommand実装をIServiceとConsoleBufferに委譲している

            this.GetDynamic = new ReactiveCommand()
                //Dynamic型で取得ボタンコマンドを実行します。
                .WithSubscribe(()
                    => this._getIService()?
                    .ShowTopIdCharacters(this._buffer))
                .AddTo(this._disposable);
            this.GetBleachCharacter = new ReactiveCommand()
                // フリガナ順でトップ10のキャラクターをコンソールに表示します。
                .WithSubscribe(()
                    => this._getIService()?
                    .ShowTopFuriganaCharacters(this._buffer))
                .AddTo(this._disposable);
            this.GetCharacterByParty = new ReactiveCommand()
                // 護廷十三隊別にキャラクターをコンソールに表示します。
                .WithSubscribe(()
                    => this._getIService()?
                    .ShowCharactersByParty(this._buffer))
                .AddTo(this._disposable);
            this.InsertCharacter = new AsyncReactiveCommand()
                // キャラクターを登録します。
                .WithSubscribe(async ()
                    => await this._getIService()?
                    .ShowInsertCharacterAsync(this._buffer))
                .AddTo(this._disposable);
            this.ClearConsole = new ReactiveCommand()
                .WithSubscribe(() => this._buffer.Clear())
                .AddTo(this._disposable);

            this.Console = this._buffer.Data
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this._disposable);
        }

        /// <summary>
        /// DI containerから任意のタイミングでserviceを取得する
        /// </summary>
        /// <returns>取得したservice</returns>
        private IService _getIService()
            // 初期化時にserviceを受け取るだけなら、constructor injectionを使えば良い。
            => (Application.Current as PrismApplication)?.Container.Resolve<IService>();

        /// <summary>コンソールのbufferを表します。</summary>
        private readonly ConsoleBuffer _buffer = new ConsoleBuffer();
    }
}
