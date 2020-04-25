using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sample7.DataAccess
{
    /// <summary>
    /// Tag用repository
    /// </summary>
    public interface ITagRepository : Takker.DBAccess.IDbAccess, IDisposable
    {
        /// <summary>
        /// 新しいTagを追加する (非同期)
        /// </summary>
        /// <param name="tagName">追加するTask</param>
        /// <returns>非同期処理の結果</returns>
        public Task AddAsync(string tagName);

        /// <summary>
        /// 指定した名前のTagを消す
        /// </summary>
        /// <param name="tagName">削除するTagの名前</param>
        /// <returns>非同期処理の結果</returns>
        public Task RemoveAsync(string tagName);

        /// <summary>
        /// Tagの名前を変更する
        /// </summary>
        /// <param name="oldName">更新するTagの名前</param>
        /// <param name="newName">新しいTagの名前</param>
        /// <returns>非同期処理の結果</returns>
        public Task RenameAsync(string oldName, string newName);

        /// <summary>
        /// 2つのTagを一つにまとめる
        /// </summary>
        /// <param name="source">合体するTagのうち、消滅する方の名前</param>
        /// <param name="destination">合体するTagのうち、名前が残る方の名前</param>
        /// <returns>非同期処理の結果</returns>
        public Task CombineAsync(string source, string destination);

    }
}
