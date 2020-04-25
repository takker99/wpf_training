using System;

using MahApps.Metro.Controls;

using Reactive.Bindings;

namespace Sample7.Setting
{
    public class ApplicationSetting : IApplicationSetting
    {
        public ReactivePropertySlim<TransitionType> ContentControlTransition { get; set; }
        public ReactivePropertySlim<SplitViewDisplayMode> HamburgerMenuDisplayMode { get; set; }
        public ReactivePropertySlim<bool> IsHamburgerMenuPanelOpened { get; set; }

        public ApplicationSetting()
        {
            this.ContentControlTransition
                = new ReactivePropertySlim<TransitionType>(Enum.Parse<TransitionType>(this._loader.GetValue("HamburgerMenu:Transition")));
            this.HamburgerMenuDisplayMode
                = new ReactivePropertySlim<SplitViewDisplayMode>(Enum.Parse<SplitViewDisplayMode>(this._loader.GetValue("HamburgerMenu:DisplayMode")));
            this.IsHamburgerMenuPanelOpened
                = new ReactivePropertySlim<bool>(Boolean.Parse(this._loader.GetValue("HamburgerMenu:IsPaneOpen")));
        }


        private Takker.Setting.ApplicationSettingLoader _loader = new Takker.Setting.ApplicationSettingLoader();
    }
}
