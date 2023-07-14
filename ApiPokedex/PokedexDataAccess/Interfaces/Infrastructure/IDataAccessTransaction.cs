namespace PokedexDataAccess.Interfaces.Infrastructure;

public interface IDataAccessTransaction
{
    Task UseTransaction(Func<IDataAccessTransaction, bool> func);
}

