using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample4.StartUp.Models
{
    /// <summary>
    /// 非常に重たい処理 (目安は50ms)を別のthreadで行うためのinterface
    /// </summary>
    public class HeavyWorker : IHeavyWorker
    {
        /// <summary>
        /// 非常に重たい処理を実行する
        /// </summary>
        /// <param name="progressInfo">処理の通知先</param>
        /// <returns></returns>
        public async Task HeavyWork(IProgress<ProgressInfo> progress)
        {
            using (this._CancellationTokenSource = new CancellationTokenSource())
            {
                try
                {
                    await Task.Run(() =>
                    {
                        foreach (var v in Enumerable.Range(1, 100))
                        {
                            // キャンセル処理
                            this._CancellationTokenSource.Token.ThrowIfCancellationRequested();

                            // 重たい処理
                            Task.Delay(100).Wait();

                            // 状況通知
                            progress.Report(new ProgressInfo(v, $"処理中{v}/{100}"));
                        }
                    }, this._CancellationTokenSource.Token);

                    // 作業終了後の処理
                    progress.Report(new ProgressInfo(0, "作業終了"));
                }
                catch (OperationCanceledException)
                {
                    // キャンセルされた場合
                    progress.Report(new ProgressInfo(0, "キャンセルされました"));
                    return;
                }
            }
        }

        /// <summary>
        /// 処理を中断する
        /// </summary>
        public void Cancel() 
            => this._CancellationTokenSource.Cancel();

        private CancellationTokenSource _CancellationTokenSource;
    }
}
