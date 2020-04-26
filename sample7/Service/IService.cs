using System;
using System.Threading.Tasks;

namespace Sample7.Services
{
    // Model layerのうち、UIのVMと直接やり取りし合う部分。
    // VMはこのinterfaceを通じてModelと通信する。
    // 具体的に函数を実装したServiceはVMから隠蔽する。

    /// <summary>サンプルアプリ用のサービスインタフェースを表します。</summary>
    public interface IService
    {
        // TaskをProjectに変換する
        //
        // 仕様：
        // 1. taskは消さない
        // 2. Task.Content->Project.Name
        // 3. task.Description->fileを新規作成してProject.Referenceへ
        //   - fileは予め指定できる
        //   - 指定しないで削除することもできる
        // 4. task.Location-> ## 場所\ntask.Locationでfileに書き込み
        // 5. 
        //public void ConvertTaskToProject();

        /// <summary>
        /// 簡易的にTaskを作成する。
        ///
        /// - 期間なし、詳細なし
        /// - defaultのProject(INBOXまたは設定ファイルで設定したProject)に放り込む
        /// </summary>
        /// <param name="content"></param>
        public Task CreateTaskAsync(string content);

        public void ShowAllTasks();

    }
}
