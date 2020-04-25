using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Reactive.Bindings.ObjectExtensions;
using Sample7.Entities;

namespace Sample7.Services
{
    /// <summary>サンプルアプリ用のサービスを表します。</summary>
    public class Service : IService
    {
        public void CreateTaskAsync(string content) => throw new System.NotImplementedException();
        public void ShowAllTasks() => throw new System.NotImplementedException();
    }
}
