using Dapper;
using PokedexDataAccess.Interfaces;
using PokedexModels.Model;
using System.Data;
using System.Data.SqlClient;

namespace PokedexDataAccess.DataAccess.Dapper;

public class ImageDapper : IImageDataAccessService
{
    private readonly string _connectionString;

    public ImageDapper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<ImageModel> GetImage(int pokemonId)
    {
        using IDbConnection conn = new SqlConnection(_connectionString);
        return await conn.QuerySingleAsync<ImageModel>("sp_pokedex_GetImage", new { pokemonId }, commandType: CommandType.StoredProcedure);
    }
}
