using System;
using System.Linq;
using System.Reactive.Linq;
using Accessibility;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace Sample1.Models
{
    [System.Runtime.Serialization.DataContract]
    public class TestPointInformation : BindableBase
    {
        /// <summary>試験結果のIDを取得・設定します。</summary>
        public int Id { get; } = 0;

        /// <summary>試験日を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<string> TestDate { get; }

        /// <summary>国語の得点を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<int> JapaneseScore { get; } = new ReactivePropertySlim<int>(0);

        /// <summary>数学の得点を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<int> MathematicsScore { get; } = new ReactivePropertySlim<int>(0);

        /// <summary>英語の得点を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<int> EnglishScore { get; } = new ReactivePropertySlim<int>(0);

        /// <summary>平均点を取得します。</summary>
        public ReadOnlyReactivePropertySlim<double> Average { get; }

        public TestPointInformation(int id, string testDate)
        {
            this.Id = id;
            this.TestDate = new ReactivePropertySlim<string>(testDate);

            // 平均点を計算する
            this.Average = Observable.Merge( 
                this.JapaneseScore,
                this.MathematicsScore,
                this.EnglishScore
                )
                .Select(_=>(this.JapaneseScore.Value+this.MathematicsScore.Value+this.EnglishScore.Value)/3.0)
            .ToReadOnlyReactivePropertySlim();
        }
    }
}
