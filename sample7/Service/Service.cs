using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Reactive.Bindings.ObjectExtensions;
using Sample7.Entities;

namespace Sample7.Services
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

            // データを作成する
            // cf.https://teratail.com/questions/82476
            var result = new DataTable("NewTable from Service");// newで新たにテーブルごと作り直すと、Viewに変更通知が出される。
            result.Columns.Add("ID",typeof(long));
            result.Columns.Add("名前",typeof(string));
            result.Columns.Add("かな",typeof(string));
            result.Columns.Add("誕生日",typeof(string));
            result.Columns.Add("所属",typeof(string));
            result.Columns.Add("斬魄刀",typeof(string));

			foreach (dynamic character in characters)
			{
                _ = result.Rows.Add(character.ID, character.CHARACTER_NAME, character.KANA,
                                                character.BIRTHDAY, character.ORGANIZATION_NAME,
                                                character.ZANPAKUTOU_NAME);
			}

            console.Data.Value = result;
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

            // データを作成する
            var result = new DataTable("NewTable from Service");// newで新たにテーブルごと作り直すと、Viewに変更通知が出される。
            result.Columns.Add("ID",typeof(long));
            result.Columns.Add("名前",typeof(string));
            result.Columns.Add("かな",typeof(string));
            result.Columns.Add("誕生日",typeof(string));
            result.Columns.Add("所属",typeof(string));
            result.Columns.Add("斬魄刀",typeof(string));

			foreach (var character in characters)
			{
                _ = result.Rows.Add(character.Id.Value, character.Name.Value, character.Furigana.Value,
                                                character.Birthday.Value, character.OrganizationName.Value,
                                                character.ZanpakutouName.Value);
            }

            console.Data.Value = result;
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

            // データを作成する

            var result = new DataTable("NewTable from Service");// newで新たにテーブルごと作り直すと、Viewに変更通知が出される。
            result.Columns.Add("隊ID", typeof(long));
            result.Columns.Add("隊名",typeof(string));
            result.Columns.Add("ID", typeof(long));
            result.Columns.Add("名前",typeof(string));
            result.Columns.Add("斬魄刀",typeof(string));
            result.Columns.Add("卍解",typeof(string));

			foreach (SoulSocietyParty party in parties)
			{
				foreach (BleachCharacter character in party.PartyMembers)
				{
                    _ = result.Rows.Add(party.PartyId, party.PartyName, character.Id.Value, character.Name.Value,
                                                    character.ZanpakutouName.Value, character.BankaiName.Value);
				}
			}

            console.Data.Value = result;
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

            // データを作成する

            var result = new DataTable("NewTable from Service");// newで新たにテーブルごと作り直すと、Viewに変更通知が出される。
            result.Columns.Add("ID",typeof(long));
            result.Columns.Add("名前",typeof(string));
            result.Columns.Add("ふりかな",typeof(string));
            result.Columns.Add("所属",typeof(string));

			foreach (var character in newCharacters)
			{
                _ = result.Rows.Add(character.Id.Value, character.Name.Value, character.Furigana.Value,
                                                character.OrganizationName.Value);
			}

            console.Data.Value = result;
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
