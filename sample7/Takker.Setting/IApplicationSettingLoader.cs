using Microsoft.Extensions.Configuration;

namespace Takker.Setting
{
    /// <summary>
    /// Applicationの設定情報を読み込むinterface
    /// </summary>
    public interface IApplicationSettingLoader
    {
        /// <summary>
        /// Applicationの設定情報を取得する
        /// </summary>
        /// <param name="path">設定情報への階層パス</param>
        /// <returns></returns>
        public string GetValue(string path);
    }
}
