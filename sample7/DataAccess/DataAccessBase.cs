using Dapper;

namespace Takker.DataAccess
{
	/// <summary>DapperSampleサンプルアプリのDataAccess用親クラスを表します。</summary>
	public abstract class DataAccessBase : DBAccess.DbAccessBase
	{
		/// <summary>Dapperのマッピング設定を初期化します。</summary>
		public static void InitializedSqlMapper()
		{
            // ReactiveProperySlimに関する変換方法を登録する
			SqlMapper.AddTypeHandler(new ReactiveSlimTypeHandler<long>());
			SqlMapper.AddTypeHandler(new ReactiveSlimTypeHandler<string>());
		}

		/// <summary>コンストラクタ。</summary>
		public DataAccessBase()
			: base(new DBAccess.Connection.DbConnectSettingLoaderBase(System.String.Empty, "DbConnectSetting.json")) { }
	}
}