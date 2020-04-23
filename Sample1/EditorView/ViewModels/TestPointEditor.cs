using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.EditorView.ViewModels
{
    public class TestPointEditor : BindableBase, System.IDisposable, INavigationAware
    {
        // 本来、ReactivePropertyはconstructorで初期化するので、
        // 読み取り専用propertiesで良い。
        // 
        // しかし今回はconstructor以外で初期化しているので、
        //     private set;
        // を追加する必要がある
        //
        // また双方向bindingを使用するため、ReactivePropertySlim
        // は使えない

        /// <summary>試験日を取得・設定します。</summary>
        public ReactiveProperty<string> TestDate { get; private set; }

        /// <summary>国語の得点を取得・設定します。</summary>
        public ReactiveProperty<int> JapaneseScore { get; private set; }

        /// <summary>数学の得点を取得・設定します。</summary>
        public ReactiveProperty<int> MathematicsScore { get; private set; }

        /// <summary>英語の得点を取得・設定します。</summary>
        public ReactiveProperty<int> EnglishScore { get; private set; }

        /// <summary>平均点を取得します。</summary>
        public ReadOnlyReactivePropertySlim<double> Average { get; private set; }



        /// <summary>Viewを表示した後呼び出されます。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            // 既にViewが作成されている場合は何もしない
            // (Viewを切り替える度に初期化する羽目になる)
            if (this._testPointInfo != null)
            {
                return;
            }

            // 呼び出し元から渡されたparameterを取り出す
            this._testPointInfo = navigationContext.Get<Models.TestPointInformation>();

            // ViewModel <=> Modelを双方向bindする
            this.TestDate = this._testPointInfo.TestDate
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.JapaneseScore = this._testPointInfo.JapaneseScore
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.MathematicsScore = this._testPointInfo.MathematicsScore
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.EnglishScore = this._testPointInfo.EnglishScore
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            // 平均点は読み取り専用なので片方向のみ
            this.Average = this._testPointInfo.Average
                .AddTo(this._disposables);

            // 変更通知を出して値を更新する
            // - constructor以外でReactivePropertyを初期化するのに必要
            this.RaisePropertyChanged(null);
        }

        /// <summary>表示するViewを判別します。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        /// <returns>表示するViewかどうかを表すbool。</returns>
        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
            // 与えられた試験結果dataと同じものを持っているViewなら再利用する
            => this._testPointInfo.Id == navigationContext.Get<Models.TestPointInformation>().Id;

        /// <summary>別のViewに切り替わる前に呼び出されます。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) { } 
        public TestPointEditor() { }

        void System.IDisposable.Dispose() => this._disposables.Dispose();

        // このViewModelとbindしているModel
        private Models.TestPointInformation _testPointInfo = null;
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
