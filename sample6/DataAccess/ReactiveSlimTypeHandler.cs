using System.Data;
using Reactive.Bindings;
using static Dapper.SqlMapper;

namespace Sample6.DataAccess
{
	/// <summary>ReactivePropertySlim<T>型用ハンドラを表します。</summary>
	public class ReactiveSlimTypeHandler<T> : TypeHandler<ReactivePropertySlim<T>>
	{
		/// <summary>DBから取得した値からプロパティに設定する値を取得します。</summary>
		/// <param name="value">DBから取得した値を表すobject。</param>
		/// <returns>プロパティに設定する値を表すReactivePropertySlim<T>。</returns>
		public override ReactivePropertySlim<T> Parse(object value)
			=> new ReactivePropertySlim<T>((T)value);

		/// <summary>バインド変数にマッピングします。</summary>
		/// <param name="parameter">設定するDBパラメータを表すIDbDataParameter。</param>
		/// <param name="value">バインド変数にマッピングするプロパティを表すReactivePropertySlim<T>。</param>
		public override void SetValue(IDbDataParameter parameter, ReactivePropertySlim<T> value)
		{
            parameter.DbType = default(T) is long ? DbType.Int64 :
                default(T) is int ? DbType.Int32 : DbType.String;
			parameter.Value = value.Value;
		}
	}
}
