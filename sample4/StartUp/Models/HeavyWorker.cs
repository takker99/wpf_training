using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample4.Models
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
            using (this._cancellationTokenSource = new CancellationTokenSource())
            {
                await Task.Run(() =>
                {
                    try
                    {
                        foreach (int v in Enumerable.Range(1, 100))
                        {
                                // キャンセル処理
                                this._cancellationTokenSource.Token.ThrowIfCancellationRequested();

                                // 重たい処理
                                Task.Delay(100).Wait();

                                // 状況通知
                                progress.Report(new ProgressInfo(v, $"処理中{v}/{100}"));
                        }
                    }
                    catch (OperationCanceledException)
                    {
                            // キャンセルされた場合
                            progress.Report(new ProgressInfo(0, "キャンセルされました"));
                        return;
                    }
                }, this._cancellationTokenSource.Token);

                // 作業終了後の処理
                progress.Report(new ProgressInfo(0, "作業終了"));
            }
        }

        /// <summary>
        /// 処理を中断する
        /// </summary>
        public void Cancel()
            => this._cancellationTokenSource.Cancel();

        private CancellationTokenSource _cancellationTokenSource;
    }
}
