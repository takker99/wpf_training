namespace Sample1
{
    // Service 層に位置するクラス
    public static class DataLoader
    {
        /// <summary>データをロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>ロードしたデータを表すAppData。</returns>
        //public static Model.AppData Load(string dataFilePath) => dataFilePath == string.Empty ? DataLoader._createNewTestData() : DataLoader._loadFromFile(dataFilePath);
        public static Model.AppData Load(string dataFilePath) => DataLoader._createNewTestData();

        /// <summary>新規テストデータを作成します。</summary>
        /// <returns>新規テストデータを表すAppData。</returns>
        private static Model.AppData _createNewTestData()
        {
            var appData = new Model.AppData();
            appData.Student.Name = "新しい生徒";
            appData.Student.ClassNumber = "所属クラス";
            appData.Student.Sex = "女";

            appData.Physicals.Add(new Model.PhysicalInformation { Id = 1 });
            appData.TestPoints.Add(new Model.TestPointInformation
            {
                Id = 1,
                TestDate = "新しい試験日",
            });

            return appData;
        }

        /// <summary>データファイルからロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>データファイルからロードしたAppData。</returns>
        private static Model.AppData _loadFromFile(string dataFilePath) => new Model.AppData();
    }
}
