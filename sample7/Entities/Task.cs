using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Reactive.Bindings;

namespace Sample7.Entities
{
    public class Task
    {
        /// <summary>
        /// Taskの内容
        /// </summary>
        public ReactivePropertySlim<string> Content { get; }
        /// <summary>
        /// タスクの詳しい説明
        /// </summary>
        public ReactivePropertySlim<string?> Description { get; }
        /// <summary>
        /// Taskの所属先のProjectのID
        /// > 複数のProjectに所属可
        /// </summary>
        public ObservableCollection<int> ProjectIds { get; }
        /// <summary>
        /// Taskの記録のID
        /// </summary>
        public ObservableCollection<int> RecordIds { get; }
        /// <summary>
        /// Taskに紐付けられているtagの名前
        /// </summary>
        public ObservableCollection<string> Tags { get; }
        /// <summary>
        /// Taskの期日
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset?> Deadline { get; }
        /// <summary>
        /// Taskの見積もり時間
        /// </summary>
        public ReactivePropertySlim<TimeSpan?> Length { get; }
        /// <summary>
        /// Taskの進行度
        /// - 0.0: 未着手
        /// - 1.0: 完了
        /// </summary>
        public ReactivePropertySlim<double> IsCompleted { get; }
        /// <summary>
        /// Taskのstatus
        /// </summary>
        public ReactivePropertySlim<ActionStatus> Status { get; }
        /// <summary>
        /// Taskの作成日時
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset> UpdatedAt { get; }
        /// <summary>
        /// Taskの更新日時
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset> CreatedAt { get; }

        public Task(string content, string? description, IEnumerable<int> projectIds, IEnumerable<int> recordIds, IEnumerable<string> tags, DateTimeOffset? deadline, TimeSpan? length, double isCompleted, ActionStatus status,DateTimeOffset createdAt,DateTimeOffset updatedAt)
        {
            this.Content = new ReactivePropertySlim<string>(content);
            this.Description = new ReactivePropertySlim<string?>(description);
            this.ProjectIds = new ObservableCollection<int>(projectIds);
            this.RecordIds = new ObservableCollection<int>(recordIds);
            this.Tags = new ObservableCollection<string>(tags);
            this.Deadline = new ReactivePropertySlim<DateTimeOffset?>(deadline);
            this.Length = new ReactivePropertySlim<TimeSpan?>(length);
            this.IsCompleted = new ReactivePropertySlim<double>(isCompleted);
            this.Status = new ReactivePropertySlim<ActionStatus>(status);
            this.CreatedAt = new ReactivePropertySlim<DateTimeOffset>(createdAt);
            this.UpdatedAt = new ReactivePropertySlim<DateTimeOffset>(updatedAt);
        }
    }
}
