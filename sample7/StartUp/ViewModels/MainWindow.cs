
using System;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;

using MaterialDesignThemes.Wpf;

using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using Sample7.Entities;
using Sample7.Services;

namespace Sample7.StartUp.ViewModels
{
    public class MainWindow : Takker.Utilities.ViewModelBase
    {
        /// <summary>TransitioningContentControlのTransitionを取得・設定します。</summary>
        public ReadOnlyReactivePropertySlim<TransitionType> ContentControlTransition { get; }

        /// <summary>HamburgerMenu.IsPaneOpenを取得・設定します。</summary>
        public ReactivePropertySlim<bool> IsHamburgerMenuPanelOpened { get; set; }

        /// <summary>HamburgerMenu.DisplayModeを取得・設定します。</summary>
        public ReadOnlyReactivePropertySlim<SplitViewDisplayMode> HamburgerMenuDisplayMode { get; }


        /// <summary>HamburgerMenuで選択しているメニュー項目のインデックスを取得・設定します。</summary>
        public ReactivePropertySlim<int> SelectedMenuIndex { get; set; }


        /// <summary>HamburgerMenuで選択しているオプションメニュー項目のインデックスを取得・設定します。</summary>
        public ReactivePropertySlim<int> SelectedOptionIndex { get; set; }



        /// <summary>HamburgerMenuで選択しているメニュー項目を取得・設定します。</summary>
        public ReactivePropertySlim<HamburgerMenuItem> SelectedMenu { get; set; }
        /// <summary>HamburgerMenuで選択しているオプションメニュー項目を取得・設定します。</summary>
        public ReactivePropertySlim<HamburgerMenuItem> SelectedOption { get; set; }

        /// <summary>HamburgerMenuのメニュー項目を取得します。</summary>
        public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new ObservableCollection<HamburgerMenuItem>();

        /// <summary>HamburgerMenuのオプションメニュー項目を取得します。</summary>
        public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new ObservableCollection<HamburgerMenuItem>();


        /// <summary>タイトルバー上のHomeボタンのCommand。</summary>
		public ReactiveCommand HomeCommand { get; }

		/// <summary>ContentRenderedイベントハンドラ。</summary>
		public ReactiveCommand ContentRendered { get; }


        /// <summary>コンストラクタ。</summary>
        public MainWindow(IRegionManager regionManager, Setting.IApplicationSetting setting)
            :base(regionManager)
        {
            // DI containerからserviceを受け取る
            this._menuService = setting;

            //全てのcommand実装をIServiceとConsoleBufferに委譲している


            this._initialilzeMenu();

            this.SelectedMenu = new ReactivePropertySlim<HamburgerMenuItem>(null)
                .AddTo(this._disposable);
            this.SelectedMenu.Subscribe(i => this._onSelectedMenu(i));

            this.SelectedMenuIndex = new ReactivePropertySlim<int>(-1)
                .AddTo(this._disposable);

            this.SelectedOption = new ReactivePropertySlim<HamburgerMenuItem>(null)
                .AddTo(this._disposable);
            this.SelectedOption.Subscribe(o => this._onSelectedMenu(o));

            this.SelectedOptionIndex = new ReactivePropertySlim<int>(-1)
                .AddTo(this._disposable);

            this.ContentControlTransition = this._menuService.ContentControlTransition
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this._disposable);
            this.HamburgerMenuDisplayMode = this._menuService.HamburgerMenuDisplayMode
                .ToReadOnlyReactivePropertySlim()
                .AddTo(this._disposable);
            this.IsHamburgerMenuPanelOpened = this._menuService.IsHamburgerMenuPanelOpened
                .AddTo(this._disposable);

            // 起動時はStart Panelを表示する。
            this.ContentRendered = new ReactiveCommand()
                .WithSubscribe(() => this.regionManager.RequestNavigate("ContentRegion", nameof(Regions.Views.StartUpPanel)))
                .AddTo(this._disposable);

            this.HomeCommand = new ReactiveCommand()
                .WithSubscribe(() =>
                {
                    // Start page に戻る
                    this.SelectedMenuIndex.Value = -1;
                    this.SelectedOptionIndex.Value = -1;
                    this._menuService.IsHamburgerMenuPanelOpened.Value = false;
                    this.regionManager.RequestNavigate("ContentRegion", nameof(Regions.Views.StartUpPanel));
                })
                .AddTo(this._disposable);

            this.SelectedMenu = new ReactivePropertySlim<HamburgerMenuItem>(null)
                .AddTo(this._disposable);
            this.SelectedOption = new ReactivePropertySlim<HamburgerMenuItem>(null)
                .AddTo(this._disposable);
        }

        /// <summary>
        /// DI containerから任意のタイミングでserviceを取得する
        /// </summary>
        /// <returns>取得したservice</returns>
        private IService _getIService()
            // 初期化時にserviceを受け取るだけなら、constructor injectionを使えば良い。
            => (System.Windows.Application.Current as PrismApplication)?.Container.Resolve<IService>();

        /// <summary>HamburgerMenuのメニュー項目を初期化します。</summary>
        private void _initialilzeMenu()
        {
            this.MenuItems.Add(new HamburgerMenuItem(PackIconKind.BugOutline, "バグ", "BugPanel"));
            this.MenuItems.Add(new HamburgerMenuItem(PackIconKind.UserOutline, "ユーザ", "UserPanel"));
            this.MenuItems.Add(new HamburgerMenuItem(PackIconKind.CoffeeOutline, "珈琲", "CoffeePanel"));
            this.MenuItems.Add(new HamburgerMenuItem(PackIconKind.FontAwesome, "サイコー！", "AwesomePanel"));

            this.OptionMenuItems.Add(new HamburgerMenuItem(PackIconKind.SettingsOutline, "設定", "SettingPanel"));
            this.OptionMenuItems.Add(new HamburgerMenuItem(PackIconKind.InfoCircleOutline, "このサンプルアプリについて", "AboutPanel"));
        }

        /// <summary>HamburgerMenuのメニュー項目選択通知イベントハンドラ。</summary>
		/// <param name="item">選択したメニュー項目を表すHamburgerMenuItemViewModel。</param>
		private void _onSelectedMenu(HamburgerMenuItem item)
		{
			if (item == null) return;
			if (System.String.IsNullOrEmpty(item.NavigationPanel)) return;

			this.regionManager.RequestNavigate("ContentRegion", item.NavigationPanel);
		}

        /// <summary>コンソールのbufferを表します。</summary>
        private readonly ConsoleBuffer _buffer = new ConsoleBuffer();
        private readonly Setting.IApplicationSetting _menuService = null;
    }
}
