using PokedexServices.Interfaces;
using PokedexServices.Model;
using SqlServerADOConnection.SQLConnection;
using System.Data;

namespace PokedexServices.Services.ADO;

public class PokedexADOSqlServerV2 : PokedexADOSqlServer, IPokedexServiceV2
{
    public PokedexADOSqlServerV2(ISqlServerADOQuery sql) : base(sql) { }
    
    public PokemonModel GetPokemon(int pokemonId, int versionId)
    {
        DataSet ds = _sql.ExecuteQueryStoredProcedure("sp_pokedex_GetPokemon", new { pokemonId, versionId });

        return ReadDataSet(ds.Tables[0].Rows[0]);
    }
}
