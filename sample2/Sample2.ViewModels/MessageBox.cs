using System;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;

namespace Sample2.ViewModels
{
    public class MessageBox : BindableBase, IDialogAware
    {
        public string Title => "Message box";
        public ReactivePropertySlim<string> Message { get; } 
            = new ReactivePropertySlim<string>(String.Empty);
        public ReactiveCommand YesCommand { get; } = new ReactiveCommand();
        public ReactiveCommand NoCommand { get; } = new ReactiveCommand();
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;
        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters)
            => this.Message.Value = parameters.GetValue<string>("Message");

        public MessageBox()
        {
            this.YesCommand.Subscribe(() => this.RequestClose?.Invoke(new DialogResult(ButtonResult.Yes)));
            this.NoCommand.Subscribe(() => this.RequestClose?.Invoke(new DialogResult(ButtonResult.No)));
        }

    }
}
