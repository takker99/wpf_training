using MahApps.Metro.Controls;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Takker.Utilities;
using Takker.Setting;

namespace Sample7.HamburgerMenuService
{
    /// <summary>
    /// HamburgerMenuのpropertyを
    /// 異なるAssembly間で共有するservice
    /// </summary>
    public class Service : BindableModelBase, IService
    {
         /// <summary>TransitioningContentControlのTransitionを取得・設定します。</summary>
		public ReactivePropertySlim<TransitionType> ContentControlTransition { get; set; }

		/// <summary>HamburgerMenuのDisplayModeを取得・設定します。</summary>
		public ReactivePropertySlim<SplitViewDisplayMode> HamburgerMenuDisplayMode { get; set; }

		/// <summary>HamburgerMenuのIsPaneOpenを取得・設定します。</summary>
		public ReactivePropertySlim<bool> IsHamburgerMenuPanelOpened { get; set; }

        public Service(IApplicationSettingLoader loader)
        {
            this.HamburgerMenuDisplayMode = new ReactivePropertySlim<SplitViewDisplayMode>()
				.AddTo(this._disposable);
			this.IsHamburgerMenuPanelOpened = new ReactivePropertySlim<bool>(false)
				.AddTo(this._disposable);
			this.ContentControlTransition = new ReactivePropertySlim<TransitionType>(TransitionType.Default)
				.AddTo(this._disposable);
        }
    }
}
