namespace Takker.DataAccess
{
	public class RepositoryFactory : IRepositoryFactory
	{
        public ICharacterRepository Create() 
            => new CharacterRepository();
    }
}