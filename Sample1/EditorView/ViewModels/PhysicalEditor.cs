using System;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.EditorView.ViewModels
{
    public class PhysicalEditor : BindableBase, IDisposable, INavigationAware, IConfirmNavigationRequest
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
        [RegularExpression(@"^\d{1,3}(\.\d{1,2})?$", ErrorMessage = "身長は整数3桁 少数2桁の範囲で入力してください。")]
        public ReactiveProperty<double> Height { get; private set; }

        /// <summary>体重を取得・設定します。</summary>
        [RegularExpression(@"^\d{1,3}(\.\d{1,2})?$", ErrorMessage = "体重は整数3桁 少数2桁の範囲で入力してください。")]
        public ReactiveProperty<double> Weight { get; private set; }

        /// <summary>BMIを取得します。</summary>
        public ReadOnlyReactivePropertySlim<double> Bmi { get; private set; }



        public PhysicalEditor(Models.AppData appData)
            // DI container からmodelsを受け取る
            => this._appData = appData;

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
                //     ignoreValidationErrorValue:true
                // を設定することで、error時に値がmodelに反映されないようになる
                .ToReactivePropertyAsSynchronized(x => x.Value
                // 初期値ではerrorが出ないようにする
                , mode: ReactivePropertyMode.Default | ReactivePropertyMode.IgnoreInitialValidationError
                , ignoreValidationErrorValue: true)
                // 複数propertyが絡むvalidationでは、DataAnnotationが使えない
                .SetValidateNotifyError(value =>
                {
                    if (!value.HasValue)
                    {
                        return "必須入力です。";
                    }
                    else if (this._appData.HasPhysicalKey(value, this._physicInfo))
                    {
                        return "既に同一の測定日が存在するため、別の日付を設定してください。";
                    }
                    else
                    {
                        // 正常の場合はnullを返す
                        return null;
                    }
                })
                .AddTo(this._disposables);
            this.Height = this._physicInfo.Height
                .ToReactivePropertyAsSynchronized(x => x.Value, ignoreValidationErrorValue: true)
                .SetValidateAttribute(() => this.Height)
                .AddTo(this._disposables);
            this.Weight = this._physicInfo.Weight
                .ToReactivePropertyAsSynchronized(x => x.Value, ignoreValidationErrorValue: true)
                .SetValidateAttribute(() => this.Weight)
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

        /// <summary>他 View への遷移を確認します。</summary>
        /// <param name="navigationContext">遷移先の情報を表すNavigationContext</param>
        /// <param name="continuationCallback">遷移を続行するかを判定するcallback</param>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            // 初期値のvalidationは無効にしてあるので、
            // ForceValidate()で強制実行する
            this.MeasurementDate.ForceValidate();
            this.Height.ForceValidate();
            this.Weight.ForceValidate();

            // 入力欄のerrorが一つもなかったら他のViewへ遷移できる
            continuationCallback(!(this.MeasurementDate.HasErrors | this.Height.HasErrors | this.Weight.HasErrors));
        }

        // このViewModelとbindしているModel
        private Models.PhysicalInformation _physicInfo = null;
        private Models.AppData _appData = null;
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
