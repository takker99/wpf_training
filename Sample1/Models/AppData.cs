using System.Collections.ObjectModel;
using System.Linq;

namespace Sample1.Models
{
    [System.Runtime.Serialization.DataContract]
    public class AppData
    {
        /// <summary>生徒の情報を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public PersonalInformation Student { get; set; } = new PersonalInformation();

        /// <summary>身体測定データを取得します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ObservableCollection<PhysicalInformation> Physicals { get; private set; }
            = new ObservableCollection<PhysicalInformation>();

        /// <summary>試験結果データを取得します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ObservableCollection<TestPointInformation> TestPoints { get; private set; }
            = new ObservableCollection<TestPointInformation>();


        /// <summary>
        /// 新規dataを作成・登録する
        /// </summary>
        /// <typeparam name="T">作成するdataの型</typeparam>
        /// <returns>作成した新規data</returns>
        public T Create<T>() where T : class
        {
            if (typeof(T) == typeof(PhysicalInformation))
            {
                var temp = new PhysicalInformation(this._publishPhysicalId());
                this.Physicals.Add(temp);
                return temp as T;
            }
            else if (typeof(T) == typeof(TestPointInformation))
            {
                var temp = new TestPointInformation(this._publishTestPointId(), "新しい試験日");
                this.TestPoints.Add(temp);
                return temp as T;
            }
            else
            {
                return null;
            }
        }

        /// <summary>指定した日付と同日の身体測定データ（自分自身は除く）が
        /// 存在するかを返します。</summary>
        /// <param name="value">存在をチェックする日付を表すDateTime?。</param>
        /// <param name="target">チェックで除外する身体測定データを表すPhysicalInformation。</param>
        /// <returns>指定した日付と同日の身体測定データが存在するかを表すbool。</returns>
        public bool HasPhysicalKey(System.DateTime? value, PhysicalInformation target)
        {
            return value.HasValue
                ? this.Physicals
                    .Where(p => p.MeasurementDate.Value.HasValue)
                    .FirstOrDefault((p) => p.MeasurementDate.Value.Value.Date == value.Value.Date && p.Id != target.Id) != null
                : this.Physicals.FirstOrDefault(p => !p.MeasurementDate.Value.HasValue) != null;
        }

        /// <summary>
        /// 身体測定dataのIDを発行する
        /// </summary>
        /// <returns>身体測定データのID</returns>
        private int _publishPhysicalId()
            => this.Physicals.Count() == 0 ? 0 : this.Physicals.Max(x => x.Id) + 1;
        /// <summary>
        /// 試験結果dataのIDを発行する
        /// </summary>
        /// <returns>試験結果dataのIDを発行する</returns>
        private int _publishTestPointId()
            => this.TestPoints.Count() == 0 ? 0 : this.TestPoints.Max(x => x.Id) + 1;
    }
}
