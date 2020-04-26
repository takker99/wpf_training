namespace Takker.DBAccess.Connection
{
    /// <summary>
    /// Database接続情報を読み込むinterface
    /// </summary>
    public interface ISettingLoader
    {
        /// <summary>接続設定を読み込みます。</summary>
        /// <returns>接続設定を表すDbConnectionSetting。</returns>
        public DbConnectionSetting Load();
    }
}
