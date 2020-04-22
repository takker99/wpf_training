using Reactive.Bindings;

namespace Sample1.Models
{
    [System.Runtime.Serialization.DataContract]
    public class PersonalInformation : Prism.Mvvm.BindableBase
    {
        /// <summary>個人名を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<string> Name { get; } = new ReactivePropertySlim<string>("");

        /// <summary>所属クラスを取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<string> ClassNumber { get; } = new ReactivePropertySlim<string>("");

        /// <summary>性別を取得・設定します。</summary>
        [System.Runtime.Serialization.DataMember]
        public ReactivePropertySlim<string> Sex { get; } = new ReactivePropertySlim<string>("");
    }
}
