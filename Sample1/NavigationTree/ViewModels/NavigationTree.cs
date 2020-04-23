using System.Windows;
using System.Windows.Navigation;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.NavigationTree.ViewModels
{
    /// <summary>ツリーのカテゴリタイプを表す列挙型。</summary>
    public enum TreeNodeCategoryType
    {
        /// <summary>カテゴリなし</summary>
        NoCategory,
        /// <summary>身体測定を表します。</summary>
        Physical,
        /// <summary>試験結果を表します。</summary>
        TestPoint
    }

    public class NavigationTree : BindableBase, System.IDisposable, IDestructible
    {
        // Tree View は user が操作できないので read only で良い。
        public ReadOnlyReactiveCollection<TreeViewItem> TreeNodes { get; }

        public ReactiveCommand<RoutedPropertyChangedEventArgs<object>> SelectedItemChanged { get; }
        /// <summary>
        /// UserContorlのLoaded event handler
        /// event parameterは使用しない
        /// </summary>
        public ReactiveCommand Loaded { get; }



        /// <summary>パラメータで指定したカテゴリ配下のアイテムを新規作成します。</summary>
        /// <param name="categoryType">新規作成するカテゴリを表すTreeNodeCategoryType列挙型の内の1つ。</param>
        /// <returns>新規作成したアイテムをセットしたTreeViewItem。</returns>
        internal TreeViewItem createNewChild(TreeNodeCategoryType categoryType)
        {
            object newItem = null;
            switch (categoryType)
            {
                case TreeNodeCategoryType.Physical:
                    newItem = this._appData.Create<Models.PhysicalInformation>();
                    break;
                case TreeNodeCategoryType.TestPoint:
                    newItem = this._appData.Create<Models.TestPointInformation>();
                    break;
            }

            return new TreeViewItem(newItem, this);
        }

        public NavigationTree(Models.AppData appData, IRegionManager regionManager)
        {
            // DI container からmodelsを受け取る
            this._appData = appData;
            this._rootNode = this._convert(this._appData); // TreeView に渡せる形式に変換する
            this._regionManager = regionManager;

            var col = new System.Collections.ObjectModel.ObservableCollection<TreeViewItem>
            {
                this._rootNode
            };
            this.TreeNodes = col.ToReadOnlyReactiveCollection().AddTo(this._disposables);

            // ReactiveCommandの設定

            // 選択したアイテムに応じて編集画面を切り替える
            this.SelectedItemChanged = new ReactiveCommand<RoutedPropertyChangedEventArgs<object>>()
                .WithSubscribe(e =>
                {
                    string viewName = System.String.Empty;
                    var current = e.NewValue as TreeViewItem;

                    switch (current.SourceData)
                    {
                        case Models.PersonalInformation p:
                            viewName = "PersonalEditor";
                            break;
                        case Models.PhysicalInformation p:
                            viewName = "PhysicalEditor";
                            break;
                        case Models.TestPointInformation t:
                            viewName = "TestPointEditor";
                            break;
                        case string s:
                            viewName = "CategoryPanel";
                            break;
                    }


                    // 編集画面を表示する
                    this._regionManager.RequestNavigate("EditorArea", viewName
                        , new NavigationParameters
                    {
                        // 編集画面にわたすparameter
                        { "TargetData", current.SourceData }
                    });
                })
                .AddTo(this._disposables);

            // 起動直後はroot nodeを選択した状態にする
            this.Loaded = new ReactiveCommand()
                .WithSubscribe(() => this._rootNode.IsSelected.Value = true)
                .AddTo(this._disposables);
        }

        // AppData を TreeViewItem の形式に変換する
        private TreeViewItem _convert(Models.AppData appData)
        {
            var rootNode = new TreeViewItem(appData.Student, this);

            // 身体測定データの tree を作る
            var physicalClass = new TreeViewItem("身体測定", this, TreeNodeCategoryType.Physical);
            rootNode.Children.Add(physicalClass);

            foreach (var item in appData.Physicals)
            {
                var child = new TreeViewItem(item, this);
                physicalClass.Children.Add(child);
            }

            // 試験結果データの tree を作る
            var testPointClass = new TreeViewItem("試験結果", this, TreeNodeCategoryType.TestPoint);
            rootNode.Children.Add(testPointClass);

            foreach (var item in appData.TestPoints)
            {
                var child = new TreeViewItem(item, this);
                testPointClass.Children.Add(child);
            }

            return rootNode;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue)
            {
                if (disposing)
                {
                    this._disposables.Dispose();
                }
                this._disposedValue = true;
            }
        }
        public void Dispose()
            => this.Dispose(true);

        /// <summary>ViewModelを破棄します。</summary>
        public void Destroy()
            => this.Dispose();

        private bool _disposedValue = false; // 重複する呼び出しを検出するには
        private readonly Models.AppData _appData = null;
        private readonly TreeViewItem _rootNode = null;
        private readonly IRegionManager _regionManager = null;
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
