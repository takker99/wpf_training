using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Reactive.Bindings;

namespace Sample7.Entities
{
    public class Record
    {
        /// <summary>
        /// 記録対象のTaskを完了できたかどうか
        /// </summary>
        public ReactivePropertySlim<bool> IsCompleted { get; }
        /// <summary>
        /// commit message
        /// タスクの実行の具合や、その時の気分、感想を書く
        /// </summary>
        public ReactivePropertySlim<string> CommitMessage { get; }
        /// <summary>
        /// 記録対象のTaskのID
        /// </summary>
        public ReactivePropertySlim<int> TaskId { get; }
        /// <summary>
        /// Recordに紐付けられているtagの名前
        /// </summary>
        public ObservableCollection<string> Tags { get; }
        /// <summary>
        /// 実行時間
        /// </summary>
        public ReactivePropertySlim<TimeSpan?> Length { get; }
        /// <summary>
        /// Taskの実行終了時刻
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset?> End { get; }
        /// <summary>
        /// Taskを実行した場所
        /// </summary>
        public ReactivePropertySlim<string?> Location { get; }
        /// <summary>
        /// Taskの作成日時
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset> UpdatedAt { get; }
        /// <summary>
        /// Taskの更新日時
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset> CreatedAt { get; }

        public Record(bool isCompleted, string commitMessage, int taskId, IEnumerable<string> tags, TimeSpan? length, string? location,DateTimeOffset createdAt,DateTimeOffset updatedAt)
        {
            this.IsCompleted = new ReactivePropertySlim<bool>(isCompleted);
            this.CommitMessage = new ReactivePropertySlim<string>(commitMessage);
            this.TaskId = new ReactivePropertySlim<int>(taskId);
            this.Tags = new ObservableCollection<string>(tags);
            this.Length = new ReactivePropertySlim<TimeSpan?>(length);
            this.Location = new ReactivePropertySlim<string?>(location);
            this.CreatedAt = new ReactivePropertySlim<DateTimeOffset>(createdAt);
            this.UpdatedAt = new ReactivePropertySlim<DateTimeOffset>(updatedAt);
        }

    }
}
