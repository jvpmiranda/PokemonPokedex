using PokedexServices.Interfaces;
using PokedexServices.Model;
using SqlServerADOConnection.SQLConnection;
using System.Data;

namespace PokedexServices.Services;

public class PokedexADOSqlServer : SqlServerQuery, IPokedexService
{
    public PokedexADOSqlServer(string connectionString) : base(connectionString) { }

    public IEnumerable<PokemonModel> GetPokemon()
    {
        DataSet ds = ExecuteQuery($"select top 10 * from pokemon");

        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return Enumerable.Empty<PokemonModel>();

        var ls = new List<PokemonModel>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            ls.Add(ReadDataSet(ds.Tables[0].Rows[i]));

        return ls;
    }

    public PokemonModel GetPokemon(int pokemonId)
    {
        DataSet ds = ExecuteQuery($"select * from pokemon where id = {pokemonId}");

        return ReadDataSet(ds.Tables[0].Rows[0]);
    }

    public void Insert(PokemonModel pokemon)
    {
        Execute($"insert into pokemon (id, identifier) values ('{pokemon.Id}', '{pokemon.Identifier}')");
    }

    public void Update(PokemonModel pokemon)
    {
        Execute($"update pokemon set identifier = '{pokemon.Identifier}' where 1 = 2 and id = {pokemon.Id}");
    }

    public void Delete(int pokemonId)
    {
        Execute($"Delete from pokemon where 1 = 2 and id = {pokemonId}");
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
