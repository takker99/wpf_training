using MahApps.Metro.Controls;
using Reactive.Bindings;

namespace Sample7.HamburgerMenuService
{
    /// <summary>
    /// HamburgerMenuのpropertyを
    /// 異なるAssembly間で共有するserviceのinterface
    /// </summary>
    public interface IService
    { 
         /// <summary>TransitioningContentControlのTransitionを取得・設定します。</summary>
		public ReactivePropertySlim<TransitionType> ContentControlTransition { get; set; }

		/// <summary>HamburgerMenuのDisplayModeを取得・設定します。</summary>
		public ReactivePropertySlim<SplitViewDisplayMode> HamburgerMenuDisplayMode { get; set; }

		/// <summary>HamburgerMenuのIsPaneOpenを取得・設定します。</summary>
		public ReactivePropertySlim<bool> IsHamburgerMenuPanelOpened { get; set; }
    }
}
