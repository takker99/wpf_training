using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.DataAccess
{
    /// <summary>
    /// Task用repositpry
    /// </summary>
    public interface ITaskRepository : Takker.DBAccess.IDbAccess, IDisposable
    {
        /// <summary>
        /// 新しいTaskを追加する (非同期)
        /// </summary>
        /// <param name="task">追加するTask</param>
        /// <returns>生成したTaskのID</returns>
        public Task<int> AddAsync(Entities.Task task);

        /// <summary>
        /// idで指定したTaskを更新する
        /// </summary>
        /// <param name="id">更新するTaskのid</param>
        /// <param name="task">新しいTaskの値</param>
        /// <returns>非同期処理の結果</returns>
        public Task UpdateAsync(int id, Entities.Task task);

        /// <summary>
        /// IDを指定してProjectを削除する(非同期)
        /// </summary>
        /// <param name="id">削除するTaskのID</param>
        /// <returns>非同期処理の結果</returns>
        public Task RemoveAsync(int id);

        /// <summary>
        /// tagからTaskを検索する
        /// </summary>
        /// <param name="tags">検索に使うtagのlist</param>
        /// <returns>指定されたtagを持つTaskとidのdictionary</returns>
        public Task<IDictionary<int, Entities.Task>> FindAsync(IEnumerable<string> tags);

        // tagの追加・削除、taskの追加・削除もUpdateAsyncで行う。

    }
}
