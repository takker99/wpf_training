using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sample3.Models
{
    /// <summary>
    /// fileへの非同期書き込みを管理するclass
    /// </summary>
    public class FileManager : IStreamManager
    {
        public FileManager(string filePath)
            => this._filePath = filePath;

        public async Task WriteTextAsync(string text)
        {
            // 非同期に書き込むには、byte列に変換する必要がある
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (var sourceStream = new FileStream(this._filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }

        public async Task<string> ReadTextAsync()
        {
            using var sourceStream = new FileStream(this._filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true);
            var sb = new StringBuilder();

            byte[] buffer = new byte[0x1000];
            int numRead;
            while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                sb.Append(text);
            }

            return sb.ToString();
        }

        private readonly string _filePath = "";
    }
}
