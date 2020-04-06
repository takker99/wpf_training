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

        /// <summary>コンストラクタ</summary>
        /// <param name="treeItem">TreeViewItem の元データを表すobject。</param>
        public TreeViewItem(object treeItem)
        {
            this.Children = new ReactiveCollection<TreeViewItem>().AddTo(this._disposables);

            this.SourceData = treeItem;

            // read only reactive propertiesは
            // 1. ObserveProperty
            // 2. ToReadOnlyReactiveProperty[Slim]
            // という手順を踏んでソースを登録する
            switch (this.SourceData)
            {
                case Sample1.Model.PersonalInformation p:
                    this.ItemText = p.ObserveProperty(x => x.Name).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    break;
                case Sample1.Model.PhysicalInformation phy:
                    this.ItemText = phy.ObserveProperty(x => x.MeasurementDate).Select(d => d.HasValue ? d.Value.ToString("yyy年MM月dd日") : "新しい測定").ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    break;
                case Sample1.Model.TestPointInformation test:
                    this.ItemText = test.ObserveProperty(x => x.TestDate).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    break;
                case string s:
                    // このブロックだけ、thisをソースとして用いている。
                    this.ItemText = this.ObserveProperty(x => x.SourceData).Select(v => v as string).ToReadOnlyReactivePropertySlim().AddTo(this._disposables);
                    break;
            }
        }

        /// <summary>オブジェクトを破棄します。</summary>
        void IDisposable.Dispose() => this._disposables.Dispose();

        /// <summary>ReactivePropertyのDispose用リスト</summary>
        private System.Reactive.Disposables.CompositeDisposable _disposables = new System.Reactive.Disposables.CompositeDisposable();
    }
}
