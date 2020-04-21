using System;
using System.Threading.Tasks;

namespace Sample4.StartUp.Models
{
    /// <summary>
    /// 非常に重たい処理 (目安は50ms)を別のthreadで行うためのinterface
    /// </summary>
    public interface IHeavyWorker
    {
        /// <summary>
        /// 非常に重たい処理を実行する
        /// </summary>
        /// <param name="progressInfo">処理の通知先</param>
        /// <returns></returns>
        public Task HeavyWork(IProgress<ProgressInfo> progressInfo);

        /// <summary>
        /// 処理を中断する
        /// </summary>
        public void Cancel();
    }
}
