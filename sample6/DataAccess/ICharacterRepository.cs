using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample6.DataAccess
{
	/// <summary>キャラクター用リポジトリを表します。</summary>
	public interface ICharacterRepository : DBAccess.IDbAccess, IDisposable
	{
		/// <summary>護廷十三隊別にキャラクターを取得します。</summary>
		/// <returns>取得した護廷十三隊を表すList<SoulSocietyParty>。</returns>
		public List<Entities.SoulSocietyParty> GetCharactersByParty();

		/// <summary>フリガナ昇順のトップ10キャラクターを取得します。</summary>
		/// <returns>取得したキャラクターを表すList<BleachCharacter>。</returns>
		public List<Entities.BleachCharacter> GetTop10Furigana();

		/// <summary>ID順のトップ10キャラクターを取得します。</summary>
		/// <returns>取得したキャラクターを表すIEnumerable<dynamic>。</returns>
		public IEnumerable<dynamic> GetTop10Id();

		/// <summary>指定した数値より大きいIDのキャラクターを取得します。</summary>
		/// <param name="minId">このパラメータに指定したより大きいIDのキャラクターを取得します。</param>
		/// <returns>指定した数値より大きいIDのキャラクターを表すTask<IEnumerable<BleachCharacter>>。</returns>
		public Task<IEnumerable<Entities.BleachCharacter>> GetCharactersByIdOrverAsync(long minId);

		/// <summary>CHARACTERSテーブルの最大オートナンバーを取得します。</summary>
		/// <returns>CHARACTERSテーブルの最大オートナンバーを表すlong。</returns>
		public long GetCharacterSeq();

		/// <summary>キャラクターを登録します。</summary>
		/// <param name="character">登録するキャラクターを表すBleachCharacter。</param>
		/// <returns>登録件数を表すint。</returns>
		public int RegistCharacter(Entities.BleachCharacter character);

		/// <summary>キャラクターを登録します。（非同期）</summary>
		/// <param name="character">登録するキャラクターを表すBleachCharacter。</param>
		/// <returns>登録件数を表すint。</returns>
		public Task<int> RegistCharacterAsync(Entities.BleachCharacter character);

		/// <summary>キャラクターを登録します。（非同期）</summary>
		/// <param name="characters">登録するキャラクターを表すList<BleachCharacter>。</param>
		/// <returns>登録件数を表すint。</returns>
		public Task<int> RegistCharactersAsync(List<Entities.BleachCharacter> characters);

		/// <summary>ふりがなを指定してキャラクターを削除します。（非同期）</summary>
		/// <param name="characters">削除するキャラクターを表すList<BleachCharacter>。</param>
		/// <returns>削除件数を表すint。</returns>
		public Task<int> DeleteCharactersByKanaAsync(List<Entities.BleachCharacter> characters);
	}
}
