using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.NavigationTree.ViewModels
{
    public class NavigationTree : BindableBase, System.IDisposable
    {
        // Tree View は user が操作できないので read only で良い。
        public ReadOnlyReactiveCollection<TreeViewItem> TreeNodes { get; }

        public ReactiveCommand<RoutedPropertyChangedEventArgs<object>> SelectedItemChanged { get; }

        public NavigationTree(Models.AppData appData)
        {
            // DI container からAppDataを受け取る
            this._appData = appData;
            this._rootNode = this._convert(this._appData); // TreeView に渡せる形式に変換する

            var col = new System.Collections.ObjectModel.ObservableCollection<TreeViewItem>
            {
                this._rootNode
            };
            this.TreeNodes = col.ToReadOnlyReactiveCollection().AddTo(this._disposables);

            this.SelectedItemChanged = new ReactiveCommand<RoutedPropertyChangedEventArgs<object>>().WithSubscribe(e => { }).AddTo(this._disposables);
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

        private Models.AppData _appData = null;
        private TreeViewItem _rootNode = null;
        private readonly System.Reactive.Disposables.CompositeDisposable _disposables
            = new System.Reactive.Disposables.CompositeDisposable();
    }
}
