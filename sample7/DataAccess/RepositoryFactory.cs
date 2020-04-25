namespace Sample7.DataAccess
{
	public class RepositoryFactory : IRepositoryFactory
	{
        public ICharacterRepository Create() 
            => new CharacterRepository();
    }
}