using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using Prism.Ioc;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Takker.Utilities;

namespace Sample7.TaskListPanel.ViewModels
{
    public class TaskListPanel : ViewModelBase
    {
        /// <summary>
        /// TaskListPanelに表示するTaskのlist
        /// </summary>
        public ReactiveCollection<TaskListItem> TaskList { get; }

        /// <summary>
        /// 新規作成するTaskの内容
        /// </summary>
        public ReactivePropertySlim<string> Content { get; }

        /// <summary>
        /// 選択しているTask
        /// </summary>
        public ReactiveProperty<TaskListItem> SelectedItem { get; set; }

        /// <summary>
        /// 新規Task作成command
        /// </summary>
        public AsyncReactiveCommand AddTaskCommand { get; }


        public TaskListPanel()
        {
            this.AddTaskCommand
                = new AsyncReactiveCommand()
                .WithSubscribe(async () =>
                {
                    // Taskを作成する。TextBoxはresetする
                    this.Content.Value = System.String.Empty;
                    await this._getIService().CreateTaskAsync(this.Content.Value);
                })
                .AddTo(this._disposable);

        }

        /// <summary>
        /// DI containerから任意のタイミングでserviceを取得する
        /// </summary>
        /// <returns>取得したservice</returns>
        private Services.IService _getIService()
            // 初期化時にserviceを受け取るだけなら、constructor injectionを使えば良い。
            => (System.Windows.Application.Current as Prism.Unity.PrismApplication)?
            .Container.Resolve<Services.IService>();
    }
}
