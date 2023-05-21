using PokedexServices.Interfaces;
using PokedexServices.Model;
using SqlServerADOConnection.SQLConnection;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace PokedexServices.Services;

public class PokedexADOSqlServer : IPokedexService
{
    private readonly ISqlServerQuery _sql;

    public PokedexADOSqlServer(ISqlServerQuery sql)
    {
        _sql = sql;
    }

    public IEnumerable<PokemonModel> GetPokemon()
    {
        DataSet ds = _sql.ExecuteQuery("select top 10 * from pokemon");

        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return Enumerable.Empty<PokemonModel>();

        var ls = new List<PokemonModel>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            ls.Add(ReadDataSet(ds.Tables[0].Rows[i]));

        return ls;
    }

    public PokemonModel GetPokemon(int pokemonId)
    {
        DataSet ds = _sql.ExecuteQuery($"select * from pokemon where id = {pokemonId}");

        return ReadDataSet(ds.Tables[0].Rows[0]);
    }

    public void Insert(PokemonModel pokemon)
    {
        _sql.ExecuteNonQuery($"insert into pokemon (id, identifier) values ('{pokemon.Id}', '{pokemon.Identifier}')");
    }

    public void Update(PokemonModel pokemon)
    {
        _sql.ExecuteNonQuery($"update pokemon set identifier = '{pokemon.Identifier}' id = {pokemon.Id}");
    }

    public void Delete(int pokemonId)
    {
        _sql.ExecuteNonQuery($"Delete from pokemon id = {pokemonId}");
    }

    private PokemonModel ReadDataSet(DataRow ds)
    {
        return new PokemonModel
        {
            Id = (int)ds["id"],
            Identifier = ds["identifier"].ToString()
        };
    }
}
