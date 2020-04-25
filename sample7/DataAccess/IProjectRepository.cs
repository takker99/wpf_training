using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.DataAccess
{
    // repositoryはentity系classのみを返却するようにする
    // これによりDataAccess layerの影響がPresentation層に出ることを防ぐことができる


    /// <summary>
    /// Project用repository
    /// </summary>
    public interface IProjectRepository : Takker.DBAccess.IDbAccess, IDisposable
    {
        /// <summary>
        /// 新しいProjectを追加する (非同期)
        /// </summary>
        /// <param name="project">追加するProject</param>
        /// <returns>生成したProjectのID</returns>
        public Task<int> AddAsync(Entities.Project project);

        /// <summary>
        /// idで指定したProjectを更新する
        /// </summary>
        /// <param name="id">更新するProjectのid</param>
        /// <param name="project">新しいProjectの値</param>
        /// <returns>非同期処理の結果</returns>
        public Task UpdateAsync(int id, Entities.Project project);

        /// <summary>
        /// IDを指定してProjectを削除する(非同期)
        /// </summary>
        /// <param name="id">削除するProjectのID</param>
        /// <returns>非同期処理の結果</returns>
        public Task RemoveAsync(int id);

        /// <summary>
        /// tagからProjectを検索する
        /// </summary>
        /// <param name="tags">検索に使うtagのlist</param>
        /// <returns>指定されたtagを持つProjectとidのdictionary</returns>
        public Task<IDictionary<int, Entities.Project>> FindAsync(IEnumerable<string> tags);

        // tagの追加・削除、taskの追加・削除もUpdateAsyncで行う。
    }
}
