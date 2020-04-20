
using Prism.Services.Dialogs;

namespace Sample2.Extensions
{
    public static class DialogServiceExtensions
    {
        public static ButtonResult ShowConfirmationMessage(this IDialogService dialogService, string message)
        {
            var param = new DialogParameters($"Message={message}");
            var tempRet = ButtonResult.Cancel;

            dialogService.ShowDialog(nameof(Views.MessageBox), param, r => tempRet = r.Result);

            return tempRet;
        }
    }
}
