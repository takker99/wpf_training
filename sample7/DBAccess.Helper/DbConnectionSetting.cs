using System.Collections.Generic;

namespace Takker.DBAccess.Connection
{
	/// <summary>
	/// DBの接続設定を表します。
	/// </summary>
	public class DbConnectionSetting
	{
		/// <summary>
		/// 接続するDBの番号を取得・設定します。
		/// </summary>
		public int TargetNumber { get; set; } = 0;

		/// <summary>
		/// DBの接続設定を取得・設定します。
		/// </summary>
		public List<DbConnectInformation> ConnectInformations { get; set; } = new List<DbConnectInformation>();
	}
}
