using System;
using System.Reactive.Linq;
using Reactive.Bindings;

namespace Sample1.Models
{
    [System.Runtime.Serialization.DataContract]
    public class PhysicalInformation : Prism.Mvvm.BindableBase
    {
        /// <summary>身体測定データのIDを取得・設定します。</summary>
        public int Id { get; } = 0;

        /// <summary>測定日を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<DateTime?> MeasurementDate { get; }

        /// <summary>身長を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<double> Height { get; }

        /// <summary>体重を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<double> Weight { get; }

        /// <summary>BMIを取得します。</summary>
        public ReadOnlyReactivePropertySlim<double> Bmi { get; }

        public PhysicalInformation(int id)
        {
            this.Id = id;
            this.MeasurementDate = new ReactivePropertySlim<DateTime?>(null);
            this.Height = new ReactivePropertySlim<double>(0.0);
            this.Weight = new ReactivePropertySlim<double>(0.0);

            // BMIを計算する
            this.Bmi = this.Height
                .CombineLatest(this.Weight, (height, weight) =>
                  height == 0 ? 0
                  : Math.Round(weight / Math.Pow(height / 100, 2), 1, MidpointRounding.AwayFromZero))
                .ToReadOnlyReactivePropertySlim();
        }
    }
}
