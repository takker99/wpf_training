namespace Sample1.Services
{
    // Service 層に位置するクラス
    public static class DataLoader
    {
        /// <summary>データをロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>ロードしたデータを表すAppData。</returns>
        //public static Models.AppData Load(string dataFilePath) => dataFilePath == string.Empty ? DataLoader._createNewTestData() : DataLoader._loadFromFile(dataFilePath);
        public static Models.AppData Load(string dataFilePath) => DataLoader._createNewTestData();

        /// <summary>新規テストデータを作成します。</summary>
        /// <returns>新規テストデータを表すAppData。</returns>
        private static Models.AppData _createNewTestData()
        {
            var appData = new Models.AppData();
            appData.Student.Name.Value = "新しい生徒";
            appData.Student.ClassNumber.Value = "所属クラス";
            appData.Student.Sex.Value = "女";
            appData.Create<Models.PhysicalInformation>();
			appData.Create<Models.TestPointInformation>();

            return appData;
        }

        /// <summary>データファイルからロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>データファイルからロードしたAppData。</returns>
        private static Models.AppData _loadFromFile(string dataFilePath) => new Models.AppData();
    }
}
