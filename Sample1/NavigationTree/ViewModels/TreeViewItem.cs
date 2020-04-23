using System;
using System.Reactive.Linq;
using System.Windows.Documents;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.NavigationTree.ViewModels
{
    public class TreeViewItem : BindableBase, IDisposable
    {
        // Scheduler と Validationを使用していなければ、xxxReactiveyyySlimを使用できる
        /// <summary>TreeViewItemのテキストを取得します。</summary>
        public ReadOnlyReactivePropertySlim<string> ItemText { get; }

        /// <summary>子ノードを取得します。</summary>
        public ReactiveCollection<TreeViewItem> Children { get; }

        /// <summary>TreeViewItem の元データを取得します。</summary>
        public object SourceData { get; } = null;

        /// <summary>TreeViewItem のImageを取得します</summary>
        public ReactivePropertySlim<PackIconKind> IconImage { get; }

        /// <summary>TreeViewItemが展開されているかを取得・設定します。</summary>
        public ReactivePropertySlim<bool> IsExpanded { get; set; }
        /// <summary>
        /// TreeViewItemが選択されているかどうか
        /// </summary>
        public ReactivePropertySlim<bool> IsSelected { get; set; }
        /// <summary>
        /// このTreeViewItemが"category"であるかどうか
        /// </summary>
        public ReadOnlyReactivePropertySlim<bool> IsCategory { get; }

        public ReactiveCommand AddNewDataCommand { get; }



        /// <summary>コンストラクタ</summary>
        /// <param name="treeItem">TreeViewItem の元データを表すobject</param>
        /// <param name="parent">このViewModelの親を表すNavigationTree</param>
        public TreeViewItem(object treeItem, NavigationTree parent)
        {
            // TreeViewItemをdefaultで展開状態とする
            this.IsExpanded = new ReactivePropertySlim<bool>(true).AddTo(this._disposables);
            this.IsSelected = new ReactivePropertySlim<bool>(false).AddTo(this._disposables);
            this.Children = new ReactiveCollection<TreeViewItem>().AddTo(this._disposables);

            this.SourceData = treeItem;
            this.IsCategory = this.ObserveProperty(x => x.SourceData)// SourceDataを初期化してから出ないと正常にflagが立たないので注意！
                .Select(data => !(data is null) && data is string)
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this._disposables);

            this._parent = parent;


            PackIconKind? icon = null;

            // read only reactive propertiesは
            // - ToReadOnlyReactivePropertySlim
            // を使って初期化する。
            // IObservableを継承していないpropertiesは、
            // ObservePropertyを使ってObservableに変換する
            switch (this.SourceData)
            {
                case Models.PersonalInformation p:
                    this.ItemText = p.Name.ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    icon = PackIconKind.UserEdit;
                    break;
                case Models.PhysicalInformation phy:
                    this.ItemText = phy.MeasurementDate.Select(d => d.HasValue ? d.Value.ToString("yyy年MM月dd日") : "新しい測定").ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    icon = PackIconKind.HumanMaleHeightVariant;
                    break;
                case Models.TestPointInformation test:
                    this.ItemText = test.TestDate.ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    icon = PackIconKind.SquareRoot;
                    break;
                case string s:
                    // このブロックだけ、thisをソースとして用いている。
                    this.ItemText = this.ObserveProperty(x => x.SourceData).Select(v => v as string).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    icon = s == "身体測定" ? PackIconKind.FolderAccountOutline : PackIconKind.FolderEditOutline;
                    break;
            }

            this.IconImage = icon.HasValue ? new ReactivePropertySlim<PackIconKind>(icon.Value).AddTo(this._disposables) : null;

            // Commandの設定
            this.AddNewDataCommand = new System.Collections.Generic.List<IObservable<bool>>()
            {
                this.IsSelected,
                this.IsCategory
            }
            .CombineLatestValuesAreAllTrue()
            .ToReactiveCommand()
            // 新しいitemを追加する
            .WithSubscribe(() => this.Children.Add(this._parent.createNewChild(this._nodeCategory)))
            .AddTo(this._disposables);
        }

        /// <summary>コンストラクタ。</summary>
        /// <summary>コンストラクタ</summary>
        /// <param name="treeItem">TreeViewItem の元データを表すobject</param>
        /// <param name="parent">このViewModelの親を表すNavigationTree</param>
        /// <param name="categoryType">カテゴリの種類を表す列挙型の内の1つ</param>
        public TreeViewItem(string treeItem,
                                     NavigationTree parent,
                                     TreeNodeCategoryType categoryType)
            : this(treeItem, parent)
            => this._nodeCategory = categoryType;

        /// <summary>オブジェクトを破棄します。</summary>
        void IDisposable.Dispose() => this._disposables.Dispose();

        private readonly NavigationTree _parent = null;
        private readonly TreeNodeCategoryType _nodeCategory = TreeNodeCategoryType.NoCategory;
        /// <summary>ReactivePropertyのDispose用リスト</summary>
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
