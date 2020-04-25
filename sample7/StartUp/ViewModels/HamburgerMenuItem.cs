using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using MaterialDesignThemes.Wpf;
using Takker.Utilities;

namespace Sample7.StartUp.ViewModels
{
	public class HamburgerMenuItem : ViewModelBase
	{
		#region プロパティ

		public ReactivePropertySlim<PackIconKind> IconKind { get; }

		public ReactivePropertySlim<string> MenuText { get; }

		public string NavigationPanel { get; }

		#endregion

		public HamburgerMenuItem(PackIconKind kind, string text, string navigationPanelName)
		{
			this.IconKind = new ReactivePropertySlim<PackIconKind>(kind)
				.AddTo(this._disposable);
			this.MenuText = new ReactivePropertySlim<string>(text)
				.AddTo(this._disposable);

			this.NavigationPanel = navigationPanelName;
		}
	}
}
