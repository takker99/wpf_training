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
            appData.Student.Name = "新しい生徒";
            appData.Student.ClassNumber = "所属クラス";
            appData.Student.Sex = "女";

            appData.Physicals.Add(new Models.PhysicalInformation { Id = 1 });
            appData.TestPoints.Add(new Models.TestPointInformation
            {
                Id = 1,
                TestDate = "新しい試験日",
            });

            return appData;
        }

        /// <summary>データファイルからロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>データファイルからロードしたAppData。</returns>
        private static Models.AppData _loadFromFile(string dataFilePath) => new Models.AppData();
    }
}
