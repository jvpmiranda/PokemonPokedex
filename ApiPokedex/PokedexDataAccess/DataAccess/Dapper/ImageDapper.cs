using Dapper;
using PokedexDataAccess.DataAccess.Dapper.Abstract;
using PokedexDataAccess.Factory;
using PokedexDataAccess.Interfaces;
using PokedexDataAccess.Interfaces.Infrastructure;
using PokedexModels.Model;
using System.Data;

namespace PokedexDataAccess.DataAccess.Dapper;

public class ImageDapper : ConnectionDapper, IImageDataAccess
{
    public ImageDapper(IFactoryDbConnection factory) : base(factory) { }

    public async Task Delete(int pokemonId)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeleteImage", new { pokemonId }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteInTransaction(int pokemonId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_DeleteImage", new { pokemonId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task<ImageModel> GetImage(int pokemonId)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QuerySingleAsync<ImageModel>("sp_pokedex_GetImage", new { pokemonId }, commandType: CommandType.StoredProcedure);
    }

    public async Task<ImageModel> GetImageInTransaction(int pokemonId, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        return await conn.QuerySingleAsync<ImageModel>("sp_pokedex_GetImage", new { pokemonId }, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Insert(ImageModel image)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_InsertImage", image, commandType: CommandType.StoredProcedure);
    }

    public async Task InsertInTransaction(ImageModel image, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_InsertImage", image, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }

    public async Task Update(ImageModel image)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpdateImage", image, commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateInTransaction(ImageModel image, IDataAccessTransaction tran)
    {
        using IDbConnection conn = GetDbConnection();
        await conn.ExecuteAsync("sp_pokedex_UpdateImage", image, commandType: CommandType.StoredProcedure, transaction: GetDbTransaction(tran));
    }
}
