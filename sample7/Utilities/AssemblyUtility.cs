using System.IO;
using System.Reflection;

namespace Takker.Utilities
{
    public static class AssemblyUtility
	{
        /// <summary>
        /// 実行fileのあるdirectoryのpathを取得する
        /// </summary>
        /// <returns>取得したdirectoryのpath</returns>
		public static string GetExecutingPath() 
            => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
