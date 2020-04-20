using System;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;

namespace Sample2.ViewModels
{
    public class MessageBox : BindableBase, IDialogAware
    {
        public string Title => "Message box";
        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;
        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters) { }

    }
}
