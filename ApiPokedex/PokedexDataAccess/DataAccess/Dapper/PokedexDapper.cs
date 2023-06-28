using Dapper;
using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using System.Data;
using System.Data.SqlClient;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokedexDapper : IPokedexDataAccessService
{
    private readonly string _connectionString;

    public PokedexDapper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        return await conn.QueryAsync<PokemonModel>("sp_pokedex_GetBasicInfoPokemon", new { pokemonId }, commandType: CommandType.StoredProcedure);
    }

    public async Task<PokemonModel> GetPokemon(int pokemonId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        var pokemon = await conn.QueryAsync<PokemonModel, PokemonVersionModel, TypeModel, int?, PokemonModel>(
            "sp_pokedex_GetPokemon",
            (pok, version, type, evol) =>
            {
                if (evol.HasValue)
                    pok.EvolvesTo = new List<int>() { evol.Value };

                pok.Types = new List<TypeModel>() { type };
                pok.Version = new List<PokemonVersionModel>() { version };
                return pok;
            },
            new { pokemonId }, splitOn: "versionId, typeId, evolvesTo", commandType: CommandType.StoredProcedure
        );

        if (pokemon.Count() == 0)
            return new PokemonModel();

        var result = PrepareModel(pokemon);
        return result;
    }

    public async Task<PokemonLineModel> GetPokemonLine(int pokemonId, int versionId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        var pokemon = await conn.QueryAsync<PokemonLineModel, PokemonVersionModel, TypeModel, int?, PokemonLineModel>(
            "sp_pokedex_GetPokemonLine",
            (pok, version, type, evol) =>
            {
                if (evol.HasValue)
                    pok.EvolvesTo = new List<int>() { evol.Value };

                pok.Types = new List<TypeModel>() { type };
                pok.Version = new List<PokemonVersionModel>() { version };
                return pok;
            },
            new { pokemonId, versionId }, splitOn: "versionId, typeId, evolvesTo", commandType: CommandType.StoredProcedure
        );

        if (pokemon.Count() == 0)
            return new PokemonLineModel();

        var result = PrepareModel(pokemon);
        return result as PokemonLineModel;
    }

    public async Task Insert(PokemonModel pokemon)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync("sp_pokedex_InsertPokemon", pokemon, commandType: CommandType.StoredProcedure);
    }

    public async Task Update(PokemonModel pokemon)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync("sp_pokedex_UpdatePokemon", pokemon, commandType: CommandType.StoredProcedure);
    }

    public async Task Delete(int pokemonId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        await conn.ExecuteAsync("sp_pokedex_DeletePokemon", new { Id = pokemonId }, commandType: CommandType.StoredProcedure);
    }

    private PokemonModel PrepareModel(IEnumerable<PokemonModel> pokemon)
    {
        var result = pokemon.GroupBy(p => p.Id).Select(g =>
        {
            var pok = g.First();
            pok.Types = g.Select(p => p.Types.Single()).ToList();
            if (pok.EvolvesTo.Count() > 0)
                pok.EvolvesTo = g.Select(p => p.EvolvesTo.Single()).ToList();
            pok.Version = g.Select(p => p.Version.Single()).ToList();
            return pok;
        }).First();

        result.Types = result.Types.GroupBy(t => t.Id).Select(t => t.First()).ToList();
        result.EvolvesTo = result.EvolvesTo.GroupBy(e => e).Select(e => e.First()).ToList();
        result.Version = result.Version.GroupBy(v => v.VersionId).Select(v => v.First()).ToList();
        return result;
    }
}
