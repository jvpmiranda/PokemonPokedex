using Dapper;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using PokedexDataAccess.DataAccess.Dapper.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.Abstract;
using PokedexDataAccess.DataAccess.MongoDb.CollectionNames;
using PokedexDataAccess.Extensions.Dapper;
using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexDataAccess.Model.MongoDb;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class PokemonDapper : ConnectionDapper, IPokemonDataAccess
{
    public PokemonDapper(FactoryDbConnection factory) : base(factory) { }

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemon(int? pokemonId)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<PokemonModel>("sp_pokedex_GetBasicInfoPokemon", new { pokemonId }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PokemonModel>> GetBasicPokemonInTransaction(int? pokemonId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<PokemonModel>("sp_pokedex_GetBasicInfoPokemon", new { pokemonId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<IEnumerable<PokemonModel>> GetPagedBasicPokemon(int start, int quantity)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<PokemonModel>("sp_pokedex_GetPagedBasicPokemon", new { start, quantity }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<PokemonModel>> GetPagedBasicPokemonInTransaction(int start, int quantity, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QueryAsync<PokemonModel>("sp_pokedex_GetPagedBasicPokemon", new { start, quantity }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<long> GetNumberOfPokemons()
    {
        using IDbConnection conn = GetDbConnection();
        var result = await conn.QueryAsync<int>("sp_pokedex_GetNumberOfPokemons", new { }, commandType: CommandType.StoredProcedure);
        return result.First();
    }

    public async Task<long> GetNumberOfPokemonsInTransaction(IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        var result = await conn.QueryAsync<int>("sp_pokedex_GetNumberOfPokemons", new { }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
        return result.First();
    }

    public async Task<PokemonModel> GetPokemon(int pokemonId, int versionId)
    {
        using IDbConnection conn = GetDbConnection();
        var result = await conn.QueryMultipleAsync("sp_pokedex_GetPokemon", new { pokemonId, versionId }, commandType: CommandType.StoredProcedure);
        return ReadResultGetPokemon(result);
    }

    public async Task<PokemonModel> GetPokemonInTransaction(int pokemonId, int versionId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        var result = await conn.QueryMultipleAsync("sp_pokedex_GetPokemon", new { pokemonId, versionId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
        return ReadResultGetPokemon(result);
    }

    public async Task Insert(PokemonModel pokemon)
    {
        using IDbConnection conn = GetDbConnection();
        DynamicParameters parameters = CreateParametersPokemonModel(pokemon);
        await conn.ExecuteAsync("sp_pokedex_InsertPokemon", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task InsertInTransaction(PokemonModel pokemon, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        DynamicParameters parameters = CreateParametersPokemonModel(pokemon);
        await conn.ExecuteAsync("sp_pokedex_InsertPokemon", parameters, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Update(PokemonModel pokemon)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpdatePokemon", pokemon, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateInTransaction(PokemonModel pokemon, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpdatePokemon", pokemon, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Delete(int pokemonId)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeletePokemon", new { Id = pokemonId }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteInTransaction(int pokemonId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeletePokemon", new { Id = pokemonId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    private DynamicParameters CreateParametersPokemonModel(PokemonModel pokemon)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", pokemon.Id);
        parameters.Add("@Name", pokemon.Name);
        parameters.Add("@Height", pokemon.Height);
        parameters.Add("@Weight", pokemon.Weight);
        parameters.Add("@GenerationNumber", pokemon.GenerationNumber);
        parameters.Add("@EvolvesFrom", pokemon.EvolvesFrom?.Id ?? 0);
        parameters.Add("@Types", pokemon.Types.AsTableValuedParameter("tpTypeOfPokemon"));
        parameters.Add("@Id", pokemon.Versions);
        return parameters;
    }

    private PokemonModel ReadResultGetPokemon(SqlMapper.GridReader result)
    {
        var pokemon = result.Read<PokemonModel>().FirstOrDefault();

        if (pokemon == null)
            return new PokemonModel();

        var versions = result.Read<PokemonVersionModel, PokemonVersionGroupModel, PokemonVersionModel>(
            (vers, group) =>
            {
                vers.VersionGroup = group;
                return vers;
            }, splitOn: "versionId, versionGroupId");


        pokemon.Versions = versions;
        pokemon.Types = result.Read<TypeModel>();
        pokemon.EvolvesFrom = result.Read<PokemonModel>().FirstOrDefault();
        pokemon.EvolvesTo = result.Read<PokemonModel>();

        return pokemon;
    }
}
