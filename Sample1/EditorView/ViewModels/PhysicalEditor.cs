using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.EditorView.ViewModels
{
    public class PhysicalEditor : BindableBase, System.IDisposable, INavigationAware
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

        /// <summary>測定日を取得・設定します。</summary>
        public ReactiveProperty<System.DateTime?> MeasurementDate { get; private set; }

        /// <summary>身長を取得・設定します。</summary>
        public ReactiveProperty<double> Height { get; private set; }

        /// <summary>体重を取得・設定します。</summary>
        public ReactiveProperty<double> Weight { get; private set; }

        /// <summary>BMIを取得します。</summary>
        public ReadOnlyReactivePropertySlim<double> Bmi { get; private set; }



        public PhysicalEditor() { }

        /// <summary>Viewを表示した後呼び出されます。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            // 既にViewが作成されている場合は何もしない
            // (Viewを切り替える度に初期化する羽目になる)
            if (this._physicInfo != null)
            {
                return;
            }

            // 呼び出し元から渡されたparameterを取り出す
            this._physicInfo = navigationContext.Get<Models.PhysicalInformation>();

            // ViewModel <=> Modelを双方向bindする
            this.MeasurementDate = this._physicInfo.MeasurementDate
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.Height = this._physicInfo.Height
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.Weight = this._physicInfo.Weight
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            // BMIは読み取り専用なので片方向のみ
            this.Bmi = this._physicInfo.Bmi
                .AddTo(this._disposables);

            // 変更通知を出して値を更新する
            // - constructor以外でReactivePropertyを初期化するのに必要
            this.RaisePropertyChanged(null);
        }

        /// <summary>表示するViewを判別します。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        /// <returns>表示するViewかどうかを表すbool。</returns>
        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
            // 与えられた身体測定dataと同じものを持っているViewなら再利用する
            => this._physicInfo.Id == navigationContext.Get<Models.PhysicalInformation>().Id;

        /// <summary>別のViewに切り替わる前に呼び出されます。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) { }

        // このViewModelとbindしているModel
        private Models.PhysicalInformation _physicInfo = null;
        void System.IDisposable.Dispose() => this._disposables.Dispose();
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }

    static class NavigationContextExtensions
    {
        /// <summary>
        /// Navigation requestから編集画面用dataを取得する
        /// </summary>
        /// <typeparam name="T">取り出したいdataの型</typeparam>
        /// <param name="navigationContext">Navigation requestから取得したdata</param>
        /// <returns></returns>
        public static T Get<T>(this NavigationContext navigationContext)
            where T : class
            => navigationContext.Parameters["TargetData"] as T;
    }
}
