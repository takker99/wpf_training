using System.Windows;
using Prism.Regions;

namespace Sample2.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            this.InitializeComponent();

            // RegionにViewを登録する
            // 
            // ModuleなしでViewをRegionに登録するには、
            // code behindを使用する必要がある
            regionManager.RegisterViewWithRegion("OperandRegion", typeof(Operand));
            regionManager.RegisterViewWithRegion("AnswerRegion", typeof(Answer));
        }
    }
}
