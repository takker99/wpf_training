using Prism.Ioc;
using Prism.Modularity;
using Sample7.DataAccess;

namespace Sample7.Services
{
	/// <summary>ApplicationLayerモジュールを表します。</summary>
	public class Module : IModule
	{
		/// <summary>モジュールを初期化します。</summary>
		/// <param name="containerProvider"></param>
		public void OnInitialized(IContainerProvider containerProvider)
			=> DataAccessBase.InitializedSqlMapper();

        /// <summary>DIコンテナへ型を登録します。</summary>
        /// <param name="containerRegistry">登録用のDIコンテナを表すIContainerRegistry。</param>
        public void RegisterTypes(IContainerRegistry containerRegistry) { }
	}
}
