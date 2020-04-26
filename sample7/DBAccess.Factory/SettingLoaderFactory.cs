using System.IO;

namespace Takker.DBAccess.Factory
{
    /// <summary>
    /// ISettingLoaderのfactory
    /// </summary>
    public static class SettingLoaderFactory
    {
        public static Connection.ISettingLoader Create()
        {
            // 接続情報file
            string settingFile = @"DbConnectSetting.json";
            // 実行fileの場所
            string execPath = Utilities.AssemblyUtility.GetExecutingPath();

            return new Connection.SettingLoader(Path.Combine(execPath, settingFile));
        }
    }
}
