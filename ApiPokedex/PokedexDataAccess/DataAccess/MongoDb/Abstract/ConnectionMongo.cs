using MongoDB.Driver;
using PokedexDataAccess.Interfaces.Infrastructure;

namespace PokedexDataAccess.DataAccess.MongoDb.Abstract;

public abstract class ConnectionMongo : IDataAccessTransaction
{
    protected readonly IMongoDatabase _conn;

    public IClientSessionHandle Session { get; set; }

    public ConnectionMongo(IMongoDatabase conn)
    {
        _conn = conn;
    }

    public async Task UseTransaction(Func<IDataAccessTransaction, bool> func)
    {
        using (Session = await _conn.Client.StartSessionAsync())
        {
            Session.StartTransaction();
            try
            {
                if (func(this))
                    await Session.CommitTransactionAsync();
                else
                    await Session.AbortTransactionAsync();
            }
            catch (Exception)
            {
                Session.AbortTransaction();
                throw;
            }

        }
    }
}
