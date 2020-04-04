using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationTree.ViewModels
{
    public class NavigationTree : BindableBase, System.IDisposable
    {
        // Tree View は user が操作できないので read only で良い。
        public ReadOnlyReactiveCollection<TreeViewItemViewModel> TreeNodes { get; }

        public NavigationTree(Sample1.Model.AppData appData)
        {
            // DI container からAppDataを受け取る
            this._appData = appData;
            this._rootNode = this._convert(this._appData); // TreeView に渡せる形式に変換する

            var col = new System.Collections.ObjectModel.ObservableCollection<TreeViewItemViewModel>();
            col.Add(this._rootNode);
            this.TreeNodes = col.ToReadOnlyReactiveCollection().AddTo(this._disposables);
        }

        // AppData を TreeViewItemViewModel の形式に変換する
        private TreeViewItemViewModel _convert(Sample1.Model.AppData appData)
        {
            var rootNode = new TreeViewItemViewModel(appData.Student);

            // 身体測定データの tree を作る
            var physicalClass = new TreeViewItemViewModel("身体測定");
            rootNode.Children.Add(physicalClass);

            foreach (var item in appData.Physicals)
            {
                var child = new TreeViewItemViewModel(item);
                physicalClass.Children.Add(child);
            }

            // 試験結果データの tree を作る
            var testPointClass = new TreeViewItemViewModel("試験結果");
            rootNode.Children.Add(testPointClass);

            foreach (var item in appData.TestPoints)
            {
                var child = new TreeViewItemViewModel(item);
                testPointClass.Children.Add(child);
            }

            return rootNode;
        }

        private Sample1.Model.AppData _appData = null;
        private TreeViewItemViewModel _rootNode = null;
        private var _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
