using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace Takker.Utilities
{
    /// <summary>
    /// シリアライズユーティリティを表します。
    /// </summary>
    public static class SerializeUtility
    {
        #region メソッド

        /// <summary>
        /// ファイルへシリアライズします。
        /// </summary>
        /// <typeparam name="T">シリアライズするオブジェクトの型を表します。</typeparam>
        /// <param name="jsonFilePath">シリアライズするJSONファイルのパスを表します。</param>
        /// <param name="data">シリアライズするオブジェクトを表すT。</param>
        public static void Serialize<T>(string jsonFilePath, T data) where T : class
        {
            // file pathの検証
            string dirPath = Path.GetDirectoryName(jsonFilePath);
            if (!Directory.Exists(dirPath))
            {
                return;
            }

            // ファイルに出力する
            File.WriteAllBytes(jsonFilePath, JsonSerializer.SerializeToUtf8Bytes(data, _options));
        }

        /// <summary>
        /// JSONファイルからデシリアライズします。
        /// </summary>
        /// <typeparam name="T">デシリアライズするオブジェクトの型を表します。</typeparam>
        /// <param name="jsonFilePath">デシリアライズするJSONファイルのパスを表します。</param>
        /// <returns>XMLファイルからデシリアライズするT。</returns>
        public static T Deserialize<T>(string jsonFilePath) where T : class
        {
            // file pathの検証
            if (!File.Exists(jsonFilePath))
            {
                return null;
            }

            // ファイルを読み込んで、deserializeする
            var utf8Reader = new Utf8JsonReader(File.ReadAllBytes(jsonFilePath));
            return JsonSerializer.Deserialize<T>(ref utf8Reader, _options);
        }

        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };

        // 以下の函数は、上の函数とやりたいことがほぼ同じ。
        // どこからの参照もなく、存在意義が不明なのでcomment outした。

        //public static void Serialize<T>(string xmlFilePath, T data) where T : class
        //{
        //	var dirPath = Path.GetDirectoryName(xmlFilePath);
        //	if (!Directory.Exists(dirPath))
        //		return;

        //	using (var stream = File.Open(xmlFilePath, FileMode.Create))
        //	{
        //		using (var writer = XmlDictionaryWriter.CreateBinaryWriter(stream))
        //		{
        //			var writerSettings = new XmlWriterSettings()
        //			{
        //				Encoding = new UTF8Encoding(false),
        //				Indent = true
        //			}; 
        //			var serializer = new DataContractSerializer(typeof(T));
        //			serializer.WriteObject(writer, data);
        //		}
        //	}
        //}

        //public static T Deserialize<T>(string xmlFilePath) where T : class
        //{
        //	using (var stream = File.OpenRead(xmlFilePath))
        //	{
        //		using (var reader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max))
        //		{
        //			var serializer = new DataContractSerializer(typeof(T));

        //			return serializer.ReadObject(reader) as T;
        //		}
        //	}

        //}

        #endregion
    }
}
