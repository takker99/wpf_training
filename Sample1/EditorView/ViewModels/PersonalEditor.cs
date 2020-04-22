using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.EditorView.ViewModels
{
    // Viewの呼び出し元からのparameterを受け取るために、INavigationAwareを継承する
    public class PersonalEditor : BindableBase, System.IDisposable, INavigationAware
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

        /// <summary>生徒氏名</summary>
        public ReactiveProperty<string> Name { get; private set; }
        /// <summary>所属class</summary>
        public ReactiveProperty<string> ClassNumber { get; private set; }
        /// <summary>性別</summary>
        public ReactiveProperty<string> Sex { get; private set; }

        public PersonalEditor() { }

        /// <summary>Viewを表示した後呼び出されます。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            // 既にViewが作成されている場合は何もしない
            // (Viewを切り替える度に初期化する羽目になる)
            if (this._personInfo != null)
            {
                return;
            }

            // 呼び出し元から渡されたparameterを取り出す
            this._personInfo = navigationContext.Get<Models.PersonalInformation>();

            // ViewModel <=> Modelを双方向bindする
            this.Name = this._personInfo.Name
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.ClassNumber = this._personInfo.ClassNumber
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);
            this.Sex = this._personInfo.Sex
                .ToReactivePropertyAsSynchronized(x => x.Value)
                .AddTo(this._disposables);

            // 変更通知を出して値を更新する
            // - constructor以外でReactivePropertyを初期化するのに必要
            this.RaisePropertyChanged(null);
        }

        /// <summary>表示するViewを判別します。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        /// <returns>表示するViewかどうかを表すbool。</returns>
        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext) => true;// 常に同じmodelを用いた画面を表示する

        /// <summary>別のViewに切り替わる前に呼び出されます。</summary>
        /// <param name="navigationContext">Navigation Requestの情報を表すNavigationContext。</param>
        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) { } 

        void System.IDisposable.Dispose() => this._disposables.Dispose();

        // このViewModelとbindしているModel
        private Models.PersonalInformation _personInfo = null;
        private System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
