using Dapper;
using PokedexDataAccess.DataAccess.Dapper.Abstract;
using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;
using PokedexServices.Interfaces;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokemonDescriptionsDapper : ConnectionDapper, IPokemonDescriptionsDataAccess
{
    public PokemonDescriptionsDapper(FactoryDbConnection factory) : base(factory) { }

    public async Task Delete(int pokemonId, int versionId)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeletePokemonDescriptions", new { pokemonId, versionId }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteInTransaction(int pokemonId, int versionId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeletePokemonDescriptions", new { pokemonId, versionId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<IEnumerable<PokemonVersionModel>> Get(int pokemonId, int? versionId)
    {
        using IDbConnection conn = GetDbConnection();

        var result = await conn.QueryAsync<PokemonVersionModel, PokemonVersionGroupModel, PokemonVersionModel>(
            "sp_pokedex_GetPokemonDescriptions",
            (vers, group) =>
            {
                vers.VersionGroup = group;
                return vers;
            },
            param: new { pokemonId, versionId }, 
            splitOn: "versionId, versionGroupId",
            commandType: CommandType.StoredProcedure);
        return result;
    }

    public async Task<IEnumerable<PokemonVersionModel>> GetInTransaction(int pokemonId, int? versionId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();

        var result = await conn.QueryAsync<PokemonVersionModel, PokemonVersionGroupModel, PokemonVersionModel>(
            "sp_pokedex_GetPokemonDescriptions",
            (vers, group) =>
            {
                vers.VersionGroup = group;
                return vers;
            },
            param: new { pokemonId, versionId },
            splitOn: "versionId, versionGroupId",
            commandType: CommandType.StoredProcedure,
            transaction: GetDbTransaction(tran));
        return result;
    }

    public async Task Upsert(int pokemonId, PokemonVersionModel version)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpsertPokemonDescriptions", new { pokemonId, version.VersionId, version.Description }, commandType: CommandType.StoredProcedure);
    }

    public async Task UpsertInTransaction(int pokemonId, PokemonVersionModel version, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpsertPokemonDescriptions", new { pokemonId, version.VersionId, version.Description }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }
}
