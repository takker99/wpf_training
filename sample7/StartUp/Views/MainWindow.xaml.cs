using CommonServiceLocator;
using MahApps.Metro.Controls;
using Prism.Regions;

namespace Sample7.StartUp.Views
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        { 
            this.InitializeComponent();

            // HamburgerMenu.Content 上に配置したコントロールは遅延作成される。
            // RegionManagerは遅延作成されたRegionを認識できないため、
            // 手動でRegionをRegionManagerに登録する必要がある。
            // cf.https://elf-mission.net/programming/wpf/ui-gallery/case01-01/#Prism_Module_VM
            RegionManager.SetRegionName(this.ContentRegion, "ContentRegion");
            RegionManager.SetRegionManager(this.ContentRegion, ServiceLocator.Current.GetInstance<IRegionManager>());
        }
    }
}
