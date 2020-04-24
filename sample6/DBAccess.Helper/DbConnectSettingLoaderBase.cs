using System.IO;

namespace Sample6.DBAccess.Connection
{
    public class DbConnectSettingLoaderBase
    {
        #region プロパティ

        public string FolderName { get; set; } = System.String.Empty;

        public string SettingFileName { get; set; } = System.String.Empty;

        #endregion

        /// <summary>接続設定を読み込みます。</summary>
        /// <returns>接続設定を表すDbConnectionSetting。</returns>
        public DbConnectionSetting Load() 
            => Utilities.SerializeUtility.DeserializeFromFile<DbConnectionSetting>(this.getSettingFilePath());

        /// <summary>接続設定ファイルのパスを取得します。</summary>
        /// <returns>接続設定ファイルのパスを表す文字列。</returns>
        protected virtual string getSettingFilePath()
        {
            string execPath = Utilities.AssemblyUtility.GetExecutingPath();

            return System.String.IsNullOrEmpty(this.FolderName)
                ? Path.Combine(execPath, this.SettingFileName)
                : Path.Combine(execPath, this.FolderName, this.SettingFileName);
        }

        #region コンストラクタ

        public DbConnectSettingLoaderBase(string folderName, string settingFileName)
        {
            this.FolderName = folderName;
            this.SettingFileName = settingFileName;
        }

        public DbConnectSettingLoaderBase() : base() { }

        #endregion
    }
}
