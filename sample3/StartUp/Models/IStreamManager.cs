using System.Threading.Tasks;

namespace Sample3.Models
{
    /// <summary>
    /// streamへの非同期I/Oを管理するinterface
    /// 今の所、FileStreamにしか対応していない
    /// </summary>
    public interface IStreamManager
    {
        Task<string> ReadTextAsync();
        Task WriteTextAsync(string text);
    }
}
