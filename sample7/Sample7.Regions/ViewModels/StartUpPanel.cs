using MahApps.Metro.Controls;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Takker.Utilities;

namespace Sample7.Regions.ViewModels
{
    public class StartUpPanel : ViewModelBase
    {
		/// <summary>TransitioningContentControlのTransitionを取得・設定します。</summary>
		public ReactivePropertySlim<TransitionType> ContentControlTransition { get; set; }

		/// <summary>HamburgerMenuのIsPaneOpenを取得・設定します。</summary>
		public ReactivePropertySlim<bool> IsHamburgerMenuPanelOpened { get; set; }

		/// <summary>HamburgerMenuのDisplayModeを取得・設定します。</summary>
		public ReactivePropertySlim<SplitViewDisplayMode> HamburgerMenuDisplayMode { get; set; }




		/// <summary>コンストラクタ。</summary>
		/// <param name="winService"></param>
		public StartUpPanel(Setting.IApplicationSetting menuService)
		{
			this._menuService = menuService;

			this.HamburgerMenuDisplayMode = this._menuService.HamburgerMenuDisplayMode
				.AddTo(this._disposable);
			this.IsHamburgerMenuPanelOpened = this._menuService.IsHamburgerMenuPanelOpened
				.AddTo(this._disposable);
			this.ContentControlTransition = this._menuService.ContentControlTransition
				.AddTo(this._disposable);
		}

		/// <summary>
        /// HamburgerMenu serviceを表す。
        /// このserviceから、HamburgerMenuのpropertyと接続する
        /// </summary>
		private Setting.IApplicationSetting _menuService = null;
    }
}
