using System;
using System.Reactive.Linq;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace NavigationTree.ViewModels
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
        public ReactivePropertySlim<System.Windows.Media.ImageSource> ItemImage { get; }

        /// <summary>コンストラクタ</summary>
        /// <param name="treeItem">TreeViewItem の元データを表すobject。</param>
        public TreeViewItem(object treeItem)
        {
            this.Children = new ReactiveCollection<TreeViewItem>().AddTo(this._disposables);

            this.SourceData = treeItem;
            var imageFileName = string.Empty;

            // read only reactive propertiesは
            // 1. ObserveProperty
            // 2. ToReadOnlyReactiveProperty[Slim]
            // という手順を踏んでソースを登録する
            switch (this.SourceData)
            {
                case Sample1.Model.PersonalInformation p:
                    this.ItemText = p.ObserveProperty(x => x.Name).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    imageFileName = "standing-man.png";
                    break;
                case Sample1.Model.PhysicalInformation phy:
                    ItemText = phy.ObserveProperty(x => x.MeasurementDate).Select(d => d.HasValue ? d.Value.ToString("yyy年MM月dd日") : "新しい測定").ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    imageFileName = "hearts.png";
                    break;
                case Sample1.Model.TestPointInformation test:
                    ItemText = test.ObserveProperty(x => x.TestDate).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    imageFileName = "test.png";
                    break;
                case string s:
                    // このブロックだけ、thisをソースとして用いている。
                    this.ItemText = this.ObserveProperty(x => x.SourceData).Select(v => v as string).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    imageFileName = s == "身体測定" ? "user-folder.png" : "test-folder.png";
                    break;
            }

            var image = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/" + imageFileName, UriKind.Absolute));
            this.ItemImage = new ReactivePropertySlim<System.Windows.Media.ImageSource>(image).AddTo(this._disposables);
        }

        /// <summary>オブジェクトを破棄します。</summary>
        void IDisposable.Dispose() => this._disposables.Dispose();

        /// <summary>ReactivePropertyのDispose用リスト</summary>
        private System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
