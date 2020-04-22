using System.Windows.Controls;

namespace Sample1.NavigationTree.Views
{
    /// <summary>
    /// Interaction logic for NavigationTree.xaml
    /// </summary>
    public partial class NavigationTree : UserControl
    {
        public NavigationTree()
            => this.InitializeComponent();

        /// <summary>TreeViewItemのPreviewMouseRightButtonDownイベントハンドラ。</summary>
        /// <param name="sender">イベントのソース。</param>
        /// <param name="e">イベントデータを格納しているMouseButtonEventArgs。</param>
        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!(sender is TreeViewItem item))
            {
                return;
            }

            item.IsSelected = true;
        }
    }
}
