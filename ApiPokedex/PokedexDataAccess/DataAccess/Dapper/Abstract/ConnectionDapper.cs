using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces.Infrastructure;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper.Abstract;

public abstract class ConnectionDapper : IDataAccessTransaction
{
    private IDbTransaction _transaction;
    private IDbConnection _connection;

    protected FactoryDbConnection Factory { get; private set; }

    public ConnectionDapper(FactoryDbConnection factory)
    {
        Factory = factory;
    }

    public async Task UseTransaction(Func<IDataAccessTransaction, bool> func)
    {
        using (_connection = GetDbConnection())
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            try
            {
                if (func.Invoke(this))
                    _transaction.Commit();
                else
                    _transaction.Rollback();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }

    protected IDbConnection GetDbConnection() => Factory.CreateConnection();

    protected IDbConnection GetDbConnection(IDataAccessTransaction conn) => ((ConnectionDapper)conn)._connection;

    protected IDbTransaction GetDbTransaction(IDataAccessTransaction tran) => ((ConnectionDapper)tran)._transaction;
}
