using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Reactive.Bindings.ObjectExtensions;

namespace Sample7.Services
{
    /// <summary>サンプルアプリ用のサービスを表します。</summary>
    public class Service : IService
    {
        public async Task CreateTaskAsync(string content)
        {

        }
        public void ShowAllTasks() => throw new System.NotImplementedException();
    }
}
