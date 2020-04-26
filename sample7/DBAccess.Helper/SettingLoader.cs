using System.IO;

namespace Takker.DBAccess.Connection
{
    public class SettingLoader : ISettingLoader
    {
        /// <summary>接続設定を読み込みます。</summary>
        /// <returns>接続設定を表すDbConnectionSetting。</returns>
        public DbConnectionSetting Load() 
            => Utilities.SerializeUtility.Deserialize<DbConnectionSetting>(this._settingFilePath);


        public SettingLoader(string settingFilePath) 
            => this._settingFilePath = settingFilePath;



        private readonly string _settingFilePath = default;
    }
}
