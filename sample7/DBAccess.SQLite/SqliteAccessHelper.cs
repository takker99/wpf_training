using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using Takker.DBAccess.Connection;

namespace Takker.DBAccess.SQlite
{
	/// <summary>SQLiteデータベースアクセスヘルパを表します。</summary>
	public class SqliteAccessHelper : IDbAccessHelper
    {
		/// <summary>DBのConnectionを取得します。</summary>
		/// <returns>DBのConnectionを表すDbConnection。</returns>
		public DbConnection GetConnection()
		{
            var con = new SQLiteConnection(builder.ToString());

			return con.OpenAndReturn();
		}

        /// <summary>DBのトランザクションを取得します。</summary>
        /// <param name="connection">トランザクションを取得するDbConnection。</param>
        /// <returns>DBのトランザクションを表すDbTransaction。</returns>
        public DbTransaction GetTransaction(DbConnection connection) 
            => !(connection is null)
                ? (connection is SQLiteConnection con)
                ? con.State == System.Data.ConnectionState.Open
                ? con.BeginTransaction() : null : null : null;

        #region コンストラクタ

        /// <summary>
        /// DBへの接続情報を表します。
        /// </summary>
        private static DbConnectInformation connectInfo = null;
		/// <summary>
		/// DBへの接続文字列を表します。
		/// </summary>
		private static SQLiteConnectionStringBuilder builder = null;

		/// <summary>
		/// コンストラクタ。
		/// </summary>
		/// <param name="connectInformation">DBへの接続情報を表すDbConnectInformation。</param>
		public SqliteAccessHelper(DbConnectInformation connectInformation)
		{
			if (connectInfo != null)
				return;

            connectInfo = connectInformation;

            string sqLitePath = this.createDbFilePath();
            if (!File.Exists(sqLitePath))
            {
                throw new FileNotFoundException("データベースファイルが存在しません。", sqLitePath);
            }

            builder = new SQLiteConnectionStringBuilder() { DataSource = sqLitePath };
		}

		/// <summary>
		/// データベースファイルへのパスを生成します。
		/// </summary>
		/// <returns>データベースファイルへのパスを表す文字列。</returns>
		private string createDbFilePath()
		{
            if (Regex.IsMatch(connectInfo.DataSource, "^{exePath}."))
			{

                string execPath = Utilities.AssemblyUtility.GetExecutingPath();

                return Regex.Replace(connectInfo.DataSource, "{exePath}", execPath);
			}
			else
			{
				return connectInfo.DataSource;
			}
		}

		#endregion
	}
}
