namespace Takker.DataAccess
{
	public interface IRepositoryFactory
	{
		public ICharacterRepository Create();
	}
}