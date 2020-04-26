using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using Reactive.Bindings;
using Takker.Utilities;

namespace Sample7.Entities
{
    public class Project: BindableModelBase
    {
        /// <summary>
        /// Projectの名前
        /// </summary>
        public ReactivePropertySlim<string> Name { get; }
        /// <summary>
        /// Projectが参照しているfileへの相対パス
        /// </summary>
        public ObservableCollection<string> References { get; }
        /// <summary>
        /// Projectに紐付けられているtagの名前
        /// </summary>
        public ObservableCollection<string> Tags { get; }
        /// <summary>
        /// Projectに紐付けられているtask
        /// </summary>
        public ObservableCollection<int> Tasks { get; }
        /// <summary>
        /// Projectの開始時刻
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset?> Begin { get; }
        /// <summary>
        /// Project全体の見積もり時間
        /// </summary>
        public ReactivePropertySlim<TimeSpan?> Length { get; }
        /// <summary>
        /// ProjectのStatus
        /// </summary>
        public ReactivePropertySlim<ActionStatus> Status { get; }
        /// <summary>
        /// Projectの作成日時
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset> UpdatedAt { get; }
        /// <summary>
        /// Projectの更新日時
        /// </summary>
        public ReactivePropertySlim<DateTimeOffset> CreatedAt { get; }

        public Project(string name, IEnumerable<string> references, DateTimeOffset? begin, TimeSpan? length, ActionStatus status,DateTimeOffset createdAt,DateTimeOffset updatedAt)
        {
            this.Name = new ReactivePropertySlim<string>(name);
            this.References = new ObservableCollection<string>(references);
            this.Tags = new ObservableCollection<string>();
            this.Tasks = new ObservableCollection<int>();
            this.Begin = new ReactivePropertySlim<DateTimeOffset?>(begin);
            this.Length = new ReactivePropertySlim<TimeSpan?>(length);
            this.Status = new ReactivePropertySlim<ActionStatus>(status);
            this.CreatedAt = new ReactivePropertySlim<DateTimeOffset>(createdAt);
            this.UpdatedAt = new ReactivePropertySlim<DateTimeOffset>(updatedAt);
        }
    }
}
