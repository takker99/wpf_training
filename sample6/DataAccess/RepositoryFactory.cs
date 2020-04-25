namespace Sample6.DataAccess
{
	public class RepositoryFactory : IRepositoryFactory
	{
        public ICharacterRepository Create() 
            => new CharacterRepository();
    }
}