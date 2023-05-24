using DapperConnection.DataAccess;
using PokedexServices.Interfaces;
using PokedexServices.Model;

namespace PokedexServices.Services;

public class PokedexDapper : IPokedexService
{
    private readonly ISqlDapperDataAccess _sql;

    public PokedexDapper(ISqlDapperDataAccess sql)
    {
        _sql = sql;
    }

    public IEnumerable<PokemonModel> GetPokemon()
    {
        //return _sql.ExecuteQuery<PokemonModel, dynamic>("select top 10 * from pokemon", new { }).Result;
        return _sql.ExecuteQueryStoredProcedure<PokemonModel, dynamic>("sp_pokedex_GetAllPokemon", new { }).Result;
    }

    public PokemonModel GetPokemon(int pokemonId)
    {
        //var result = _sql.ExecuteQuery<PokemonModel, dynamic>("select * from pokemon where id = @id", new { id = pokemonId }).Result;
        var result = _sql.ExecuteQueryStoredProcedure<PokemonModel, dynamic>("sp_pokedex_GetPokemon", new { id = pokemonId }).Result;
        return result.FirstOrDefault();
    }

    public void Insert(PokemonModel pokemon)
    {
        //_sql.ExecuteNonQuery("insert into pokemon (identifier) values (@Identifier)", pokemon);
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_InsertPokemon", pokemon);
    }

    public void Update(PokemonModel pokemon)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_UpdatePokemon", pokemon);
    }

    public void Delete(int pokemonId)
    {
        _sql.ExecuteNonQueryStoredProcedure("sp_pokedex_DeletePokemon", new { Id = pokemonId });
    }
}
