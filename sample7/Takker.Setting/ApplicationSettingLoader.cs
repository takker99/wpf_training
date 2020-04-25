using Microsoft.Extensions.Configuration;

namespace Takker.Setting
{
    public class ApplicationSettingLoader : IApplicationSettingLoader
    {
        public ApplicationSettingLoader() 
            => this._configuration ??= new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile(this._settingPath, optional: true, reloadOnChange: true)
                .Build();

        /// <summary>
        /// Applicationの設定情報を取得する
        /// </summary>
        /// <param name="path">設定情報への階層パス</param>
        /// <returns></returns>
        public string GetValue(string path)
            => this._configuration[path];


        /// <summary>
        /// 設定情報
        /// </summary>
        private IConfigurationRoot _configuration{ get; } = null;

        /// <summary>
        /// 設定ファイルのCurrent Directoryからの相対パス
        /// </summary>
        private readonly string _settingPath = @"appsettings.json";
    }
}
