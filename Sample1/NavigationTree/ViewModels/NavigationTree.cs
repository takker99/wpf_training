using System.Windows;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.NavigationTree.ViewModels
{
    public class NavigationTree : BindableBase, System.IDisposable
    {
        // Tree View は user が操作できないので read only で良い。
        public ReadOnlyReactiveCollection<TreeViewItem> TreeNodes { get; }

        public ReactiveCommand<RoutedPropertyChangedEventArgs<object>> SelectedItemChanged { get; }

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

            this.SelectedItemChanged = new ReactiveCommand<RoutedPropertyChangedEventArgs<object>>()
                .WithSubscribe(e =>
                {
                    var viewName = string.Empty;
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

                    this._regionManager.RequestNavigate("EditorArea", viewName);
                })
                .AddTo(this._disposables);
        }

        // AppData を TreeViewItem の形式に変換する
        private TreeViewItem _convert(Models.AppData appData)
        {
            var rootNode = new TreeViewItem(appData.Student);

            // 身体測定データの tree を作る
            var physicalClass = new TreeViewItem("身体測定");
            rootNode.Children.Add(physicalClass);

            foreach (var item in appData.Physicals)
            {
                var child = new TreeViewItem(item);
                physicalClass.Children.Add(child);
            }

            // 試験結果データの tree を作る
            var testPointClass = new TreeViewItem("試験結果");
            rootNode.Children.Add(testPointClass);

            foreach (var item in appData.TestPoints)
            {
                var child = new TreeViewItem(item);
                testPointClass.Children.Add(child);
            }

            return rootNode;
        }

        void System.IDisposable.Dispose() => this._disposables.Dispose();

        private readonly Models.AppData _appData = null;
        private readonly TreeViewItem _rootNode = null;
        private IRegionManager _regionManager = null;
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
