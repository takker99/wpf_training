using Reactive.Bindings;

namespace Sample6.Entities
{
    /// <summary>BLEACHのキャラクターを表します。</summary>
    public class BleachCharacter
    {
        /// <summary>キャラクターIDを取得・設定します。</summary>
        public ReactivePropertySlim<long> Id
        {
            get => this._id;
            set
            {
                this._id?.Dispose();
                this._id = value;
            }
        }

        /// <summary>キャラクター名を取得・設定します。</summary>
        public ReactivePropertySlim<string> Name
        {
            get => this._name;
            set
            {
                this._name?.Dispose();
                this._name = value;
            }
        }

        public ReactivePropertySlim<string> Furigana { get; set; } = new ReactivePropertySlim<string>(System.String.Empty);

        public ReactivePropertySlim<string> Birthday { get; set; } = new ReactivePropertySlim<string>(System.String.Empty);

        public ReactivePropertySlim<long> OrganizationId { get; set; } = new ReactivePropertySlim<long>(0);

        public ReactivePropertySlim<string> OrganizationName { get; set; } = new ReactivePropertySlim<string>(System.String.Empty);

        public ReactivePropertySlim<long> ZanpakutouId { get; set; } = new ReactivePropertySlim<long>(0);

        public ReactivePropertySlim<string> ZanpakutouName { get; set; } = new ReactivePropertySlim<string>(System.String.Empty);

        public ReactivePropertySlim<string> BankaiName { get; set; } = new ReactivePropertySlim<string>(System.String.Empty);

        /// <summary>コンストラクタ</summary>
        public BleachCharacter()
        {
            this._id = new ReactivePropertySlim<long>(0);
            this._name = new ReactivePropertySlim<string>(System.String.Empty);
        }

        /// <summary>コンストラクタ</summary>
        /// <param name="characterName">キャラクター名を表す文字列</param>
        /// <param name="kana">キャラクター名のフリガナを表す文字列</param>
        /// <param name="orgId">所属する組織IDを表すlong。</param>
        public BleachCharacter(string characterName, string kana, long orgId) : this()
        {
            this._name.Value = characterName;
            this.Furigana.Value = kana;
            this.OrganizationId.Value = orgId;
        }

        private ReactivePropertySlim<long> _id;
        private ReactivePropertySlim<string> _name;
    }
}
