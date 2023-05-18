using ApiPokedex.Interfaces;
using SqlServerDataBaseConnection.Interface;
using SqlServerDataBaseConnection.Model;
using SqlServerDataBaseConnection.SQLConnection;
using System.Data;

namespace ApiPokedex.Services;

public class PokedexSqlServer : SqlServerQuery, IPokedexService
{
    public PokedexSqlServer(ISqlConnection sqlConnection) : base(sqlConnection) { }

    public IEnumerable<PokemonPokedex> GetPokemon()
    {
        DataSet ds = ExecuteQuery($"select top 10 * from pokemon");

        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return Enumerable.Empty<PokemonPokedex>();

        var ls = new List<PokemonPokedex>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            ls.Add(ReadDataSet(ds.Tables[0].Rows[i]));

        return ls;
    }

    public PokemonPokedex GetPokemon(int pokemonId)
    {
        DataSet ds = ExecuteQuery($"select * from pokemon where id = {pokemonId}");

        return ReadDataSet(ds.Tables[0].Rows[0]);
    }

    public void Insert(PokemonPokedex pokemon)
    {
        Execute($"insert into pokemon (id, identifier) values ('{pokemon.Id}', '{pokemon.Name}')");
    }

    public void Update(PokemonPokedex pokemon)
    {
        Execute($"update pokemon set identifier = '{pokemon.Name}' where 1 = 2 and id = {pokemon.Id}");
    }

    public void Delete(int pokemonId)
    {
        Execute($"Delete from pokemon where 1 = 2 and id = {pokemonId}");
    }

    private PokemonPokedex ReadDataSet(DataRow ds)
    {
        return new PokemonPokedex
        {
            Id = (int)ds["id"],
            Name = ds["identifier"].ToString()
        };
    }
}
