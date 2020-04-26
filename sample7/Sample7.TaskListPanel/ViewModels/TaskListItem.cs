using System;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Takker.Utilities;

namespace Sample7.TaskListPanel.ViewModels
{
    public class TaskListItem : ViewModelBase
    {
        /// <summary>
        /// Taskの内容
        /// </summary>
        public ReactiveProperty<string> Content { get; }
        /// <summary>
        /// Taskの期日
        /// </summary>
        public ReactiveProperty<DateTimeOffset?> Deadline { get; }
        /// <summary>
        /// Taskの見積もり時間
        /// </summary>
        public ReactiveProperty<TimeSpan?> Length { get; }
        /// <summary>
        /// Taskの進行度
        /// - 0.0: 未着手
        /// - 1.0: 完了
        /// </summary>
        public ReadOnlyReactivePropertySlim<double> IsCompleted { get; }
        /// <summary>
        /// Taskのstatus
        /// </summary>
        public ReactiveProperty<Entities.ActionStatus> Status { get; }
        /// <summary>
        /// Taskの作成日時
        /// </summary>
        public ReadOnlyReactivePropertySlim<DateTimeOffset> UpdatedAt { get; }
        /// <summary>
        /// Taskの更新日時
        /// </summary>
        public ReadOnlyReactivePropertySlim<DateTimeOffset> CreatedAt { get; }
        public TaskListItem()
        {

        }
    }
}
