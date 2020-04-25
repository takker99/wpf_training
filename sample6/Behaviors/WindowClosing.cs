using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.Xaml.Behaviors;
using Sample6.Win32Api;

namespace Sample6.Behaviors
{
	public class WindowClosingBehavior : Behavior<Window>
	{
		public bool CanClose
        {
            get => (bool)this.GetValue(CanCloseProperty);
            set => this.SetValue(CanCloseProperty, value);
        }

        // Using a DependencyProperty as the backing store for CanClose.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanCloseProperty =
			DependencyProperty.Register("CanClose",
							   typeof(bool),
							   typeof(WindowClosingBehavior),
							   new PropertyMetadata(true));

		public ICommand CloseCanceledCommand
        {
            get => (ICommand)this.GetValue(CloseCanceledCommandProperty);
            set => this.SetValue(CloseCanceledCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for CloseCanceledCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseCanceledCommandProperty =
			DependencyProperty.Register("CloseCanceledCommand",
				typeof(ICommand),
				typeof(WindowClosingBehavior),
				new PropertyMetadata(null));

		protected override void OnAttached()
		{
            if (this.AssociatedObject == null)
            {
                throw new InvalidOperationException();
            }

            this.AssociatedObject.SourceInitialized += this.onSourceInitialized;
			base.OnAttached();

			this.AssociatedObject.Closing += (sender, e) =>
			{
                if (this.CanClose)
                {
                    return;
                }

                if ((this.CloseCanceledCommand != null) && this.CloseCanceledCommand.CanExecute(null))
				{
					this.CloseCanceledCommand.Execute(null);
				}

				e.Cancel = !this.CanClose;
				this.CanClose = true;
			};
		}

		protected override void OnDetaching()
		{
			var source = (HwndSource)PresentationSource.FromVisual(this.AssociatedObject);
			source.RemoveHook(this.wndProc);
			this.AssociatedObject.SourceInitialized -= this.onSourceInitialized;

			base.OnDetaching();
		}

		private void onSourceInitialized(object sender, EventArgs e)
		{
            var source = (HwndSource)PresentationSource.FromVisual(this.AssociatedObject);
            source.AddHook(this.wndProc);
		}

		private IntPtr wndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == Constant.WM_SYSCOMMAND &&
				(wParam.ToInt64() & 0xFFF0L) == Constant.SC_CLOSE)
			{
				handled = !this.CanClose;
			}

			return IntPtr.Zero;
		}
	}
}
