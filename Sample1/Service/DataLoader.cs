using Sample1.Model
namespace Sample1
{
    // Service 層に位置するクラス
    public static class DataLoader
    {
        /// <summary>新規テストデータを作成します。</summary>
        /// <returns>新規テストデータを表すAppData。</returns>
        private static AppData _createNewTestData()
        {
            var appData = new AppData();
            appData.Student.Name = "新しい生徒";
            appData.Student.ClassNumber = "所属クラス";
            appData.Student.Sex = "女";

            appData.Physicals.Add(new PhysicalInformation { Id = 1 });
            appData.TestPoints.Add(new TestPointInformation
            {
                Id = 1,
                TestDate = "新しい試験日",
            });

            return appData;
        }

        /// <summary>データファイルからロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>データファイルからロードしたAppData。</returns>
        private static AppData loadFromFile(string dataFilePath) => new AppData();

        /// <summary>データをロードします。</summary>
        /// <param name="dataFilePath">データファイルのフルパスを表す文字列。</param>
        /// <returns>ロードしたデータを表すAppData。</returns>
        public static AppData Load(string dataFilePath)
        {
            if (dataFilePath == string.Empty) { return DataLoader.createNewTestData(); }
            else { return DataLoader.loadFromFile(dataFilePath); }
        }
    }
}
