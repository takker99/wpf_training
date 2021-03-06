using System.Text.Json;
using System.Text;
using static System.Console;

namespace Sample6.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello, takker!");

            // System.Text.Jsonのテストコード

            var testData = new DBAccess.Connection.DbConnectionSetting()
            {
                TargetNumber = 0,
                ConnectInformations = new System.Collections.Generic.List<DBAccess.Connection.DbConnectInformation>()
                {
                    new DBAccess.Connection.DbConnectInformation()
                    {
                        DataSource = @"{exePath}\SampleDb.sqlite3",
                        DbType = DBAccess.Connection.DatabaseType.SQLite,
                        Number = 0,
                    }
                },
            };

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };
            // JSONでobjectをserializeする
            byte[] output = JsonSerializer.SerializeToUtf8Bytes(testData, jsonOptions);
            WriteLine($"The result of serialization:\n{Encoding.UTF8.GetString(output)}");

            // fileに保存する
            string path = @"e:\json_test.json";
            System.IO.File.WriteAllBytes(path, output);
            WriteLine("Successfully serialized!");

            // fileから読み込む
            var utf8Reader = new Utf8JsonReader(System.IO.File.ReadAllBytes(path));
            var testData2 = JsonSerializer.Deserialize<DBAccess.Connection.DbConnectInformation>(ref utf8Reader, jsonOptions);

            WriteLine("Successfully Desirialized!");
            WriteLine("Result:");
            WriteLine($"DataSource = {testData2.DataSource}");
            WriteLine($"DbType = {testData2.DbType}");
            WriteLine($"Number = {testData2.Number}");

            WriteLine("Successfully finished!");
        }
    }
}
