using System.Collections.Generic;
using System.Threading.Tasks;
using Sample6.Entities;

namespace Sample6.Services
{
	/// <summary>サンプルアプリ用のサービスを表します。</summary>
	public class Service : IService
	{
		/// <summary>ID順でトップ10のキャラクターをコンソールに表示します。</summary>
		/// <param name="console">表示するコンソールを表すConsoleBuffer。</param>
		public void ShowTopIdCharacters(ConsoleBuffer console)
		{
            // bufferに残っている文字を全て削除
			console.Clear();

			IEnumerable<dynamic> characters = null;

			using (var repository = this._factory.Create())
			{
				characters = repository.GetTop10Id();
			}

			foreach (dynamic character in characters)
			{
				console.AppendLineToBuffer($"ID: {character.ID}  名前: {character.CHARACTER_NAME}  かな: {character.KANA}  " +
					$"誕生日: {character.BIRTHDAY}  所属: {character.ORGANIZATION_NAME}  斬魄刀: {character.ZANPAKUTOU_NAME}");
			}
		}

		/// <summary>フリガナ順でトップ10のキャラクターをコンソールに表示します。</summary>
		/// <param name="console">表示するコンソールを表すConsoleBuffer。</param>
		public void ShowTopFuriganaCharacters(ConsoleBuffer console)
		{
			console.Clear();

			List<BleachCharacter> characters = null;

			using (var repository = this._factory.Create())
			{
				characters = repository.GetTop10Furigana();
			}

			foreach (BleachCharacter chara in characters)
			{
				console.AppendLineToBuffer($"ID: {chara.Id}  名前: {chara.Name}  かな: {chara.Furigana}  " +
					$"誕生日: {chara.Birthday}  所属: {chara.OrganizationName}  斬魄刀: {chara.ZanpakutouName}");


			}
		}

		/// <summary>護廷十三隊別にキャラクターをコンソールに表示します。</summary>
		/// <param name="console">表示するコンソールを表すConsoleBuffer。</param>
		public void ShowCharactersByParty(ConsoleBuffer console)
		{
			console.Clear();

			List<SoulSocietyParty> parties = null;

			using (var repository = this._factory.Create())
			{
				parties = repository.GetCharactersByParty();
			}

			foreach (SoulSocietyParty party in parties)
			{
				console.AppendLineToBuffer($"隊ID: {party.PartyId}　隊名: {party.PartyName}");

				foreach (var chara in party.PartyMembers)
				{
					console.AppendLineToBuffer($"\tID: {chara.Id}  名前: {chara.Name}　斬魄刀: {chara.ZanpakutouName}　卍解: {chara.BankaiName}");
				}
			}
		}

		/// <summary>Insertしたキャラクターを表示します。</summary>
		/// <param name="console">表示するコンソールを表すConsoleBuffer。</param>
		/// <returns>非同期処理の結果を表すTask。</returns>
		public async Task ShowInsertCharacterAsync(ConsoleBuffer console)
		{
			console.Clear();

			IEnumerable<BleachCharacter> newCharacters = null;

			using (var repository = this._factory.Create())
			{
                long seq = repository.GetCharacterSeq();

				using (var tran = repository.BeginTransaction())
				{
					var characters = this.createSavesCharacters();

					try
					{
						await repository.DeleteCharactersByKanaAsync(characters)
							.ContinueWith(c => repository.RegistCharactersAsync(characters))
							.ContinueWith(c => tran.CommitAsync());
					}
					catch (System.Exception)
					{
						tran.Rollback();
						throw;
					}
				}

				newCharacters = await repository.GetCharactersByIdOrverAsync(seq);
			}

			foreach (var chara in newCharacters)
			{
				console.AppendLineToBuffer($"ID: {chara.Id}  名前: {chara.Name}　ふりがな: {chara.Furigana}　所属: {chara.OrganizationName}");
			}
		}

        /// <summary>登録用のキャラクターリストを生成します。</summary>
        /// <returns>生成した登録用のキャラクターリストを表すList<BleachCharacter>。</returns>
        private List<BleachCharacter> createSavesCharacters() 
            => new List<BleachCharacter>()
            {
                new BleachCharacter("麒麟寺 天示郎", "きりんじ てんじろう", 14),
                new BleachCharacter("曳舟 桐生", "ひきふね きりお", 14),
                new BleachCharacter("二枚屋 王悦", "にまいや おうえつ", 14),
                new BleachCharacter("修多羅 千手丸", "しゅたら せんじゅまる", 14),
                new BleachCharacter("兵主部 一兵衛", "ひょうすべ いちべえ", 14)
            };

        /// <summary>リポジトリのファクトリを表します。</summary>
        private readonly DataAccess.IRepositoryFactory _factory = null;

        /// <summary>コンストラクタ。</summary>
        /// <param name="repositoryFactory"></param>
        public Service(DataAccess.IRepositoryFactory repositoryFactory) 
            => this._factory = repositoryFactory;
    }
}
