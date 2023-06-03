using DapperConnection.DataAccess;
using PokedexServices.Interfaces;
using PokedexServices.Model;

namespace PokedexServices.Services.Dapper;

public class PokedexDapperV2 : PokedexDapper, IPokedexServiceV2
{
    public PokedexDapperV2(ISqlDapperDataAccess sql) : base(sql) { }

    public PokemonModel GetPokemon(int pokemonId, int versionId)
    {
        PokemonModel pokemon;
        using (var queryResult = _sql.ExecuteQueryStoredProcedureMultiple<dynamic>("sp_pokedex_GetPokemon", new { pokemonId, versionId }).Result)
        {
            pokemon = queryResult.Read<PokemonModel>().ToList().First();
            pokemon.Types = queryResult.Read<PokemonTypeModel>().ToList();
            pokemon.Description = queryResult.Read<PokemonDescription>().ToList();
        }

        return pokemon;
    }

}
