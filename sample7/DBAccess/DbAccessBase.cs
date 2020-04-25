using System;
using System.Data.Common;
using Takker.DBAccess.Connection;

namespace Takker.DBAccess
{
    /// <summary>DBアクセスのベースクラスを表します。</summary>
    public abstract class DbAccessBase : IDbAccess, IDisposable
    {
        /// <summary>データベースのコネクションを表します。</summary>
        protected DbConnection Connection { get; private set; } = null;

        /// <summary>トランザクションを開始します。</summary>
        /// <returns>DBのトランザクションを表すDbTransaction。</returns>
        public DbTransaction BeginTransaction()
            => helper.GetTransaction(this.Connection);

        #region IDisposable Support

        /// <summary>このクラスを破棄します。</summary>
        /// <param name="disposing">破棄中かを表すbool。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed)
            {
                return;
            }
            if (disposing)
            {
                this.Connection?.Dispose();
            }
            this._isDisposed = true;
        }

        public void Dispose()
            => this.Dispose(true);

        #endregion

        #region コンストラクタ

        /// <summary>デフォルトコンストラクタ。</summary>
        public DbAccessBase(DbConnectSettingLoaderBase loader) : base()
            => this.initializeConnection(loader);

        private bool _isDisposed = false; // 重複する呼び出しを検出するには
        /// <summary>キャッシュした接続文字列を表します。</summary>
        private static DbConnectionSetting connectionSetting = null;
        /// <summary>AbstractFactoryを表すIDbAccessHelper。</summary>
        private static IDbAccessHelper helper = null;

        /// <summary>
        /// 接続を初期化します。
        /// </summary>
        private void initializeConnection(DbConnectSettingLoaderBase loader)
        {
            if (connectionSetting == null)
            {
                DbConnectSettingLoaderBase settingLoader = loader ?? new DbConnectSettingLoaderBase();

                // キャッシュされていない場合は接続設定ファイルを読み込む
                connectionSetting = settingLoader.Load() ??
                    throw new Exception("DBの接続設定ファイルがLoadできません。");
            }

            // 接続する設定ファイル番号を取得
            int? num = this.getConnectionNumber();

            if (!num.HasValue)
                helper ??= DbAccessHelperFactory.CreateHelper(connectionSetting, num);

            this.Connection = helper.GetConnection();
        }

        /// <summary>接続対象の設定ファイル番号を取得します。</summary>
        /// <returns>接続対象の設定ファイル番号を表すint?。</returns>
        protected virtual int? getConnectionNumber()
            => null;

        #endregion
    }
}
