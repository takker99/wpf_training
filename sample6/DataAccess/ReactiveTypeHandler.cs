using System.Data;
using Reactive.Bindings;
using static Dapper.SqlMapper;

namespace Sample6.DataAccess
{
	public class ReactiveTypeHandler<T> : TypeHandler<ReactiveProperty<T>>
        where T: struct
	{
        public override ReactiveProperty<T> Parse(object value) => new ReactiveProperty<T>((T)value);

        public override void SetValue(IDbDataParameter parameter, ReactiveProperty<T> value)
		{
            parameter.DbType = default(T) is long ? DbType.Int64 :
                default(T) is int ? DbType.Int32 : DbType.String;
			parameter.Value = value.Value;
		}
	}
}