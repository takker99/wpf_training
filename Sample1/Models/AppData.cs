using System.Collections.ObjectModel;

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
    }
}
