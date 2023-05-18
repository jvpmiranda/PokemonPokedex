using ApiPokedex.Contract;
using ApiPokedex.Interfaces;
using SqlServerDataBaseConnection.Interface;
using SqlServerDataBaseConnection.SQLConnection;
using System.Data;

namespace ApiPokedex.Services;

public class PokedexSqlServer : SqlServerQuery, IPokedexService
{
    public PokedexSqlServer(ISqlConnection sqlConnection) : base(sqlConnection) { }

    public IEnumerable<Pokemon> GetPokemon()
    {
        DataSet ds = ExecuteQuery($"select top 10 * from pokemon");

        if (ds == null || ds.Tables[0].Rows.Count == 0)
            return Enumerable.Empty<Pokemon>();

        var ls = new List<Pokemon>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            var pokemon = new Pokemon
            {
                PokemonId = (int)ds.Tables[0].Rows[i]["id"],
                PokemonName = ds.Tables[0].Rows[i]["identifier"].ToString()
            };
            ls.Add(pokemon);
        }

        return ls;
    }

    public Pokemon GetPokemon(int pokemonId)
    {
        DataSet ds = ExecuteQuery($"select * from pokemon where id = {pokemonId}");
        var pokemon = new Pokemon
        {
            PokemonId = (int)ds.Tables[0].Rows[0]["id"],
            PokemonName = ds.Tables[0].Rows[0]["identifier"].ToString()
        };

        return pokemon;
    }

    public void Insert(Pokemon pokemon)
    {
        Execute($"insert into pokemon (id, identifier) values ('{pokemon.PokemonId}', '{pokemon.PokemonName}')");
    }

    public void Update(Pokemon pokemon)
    {
        Execute($"update pokemon set identifier = '{pokemon.PokemonName}' where 1 = 2 and id = {pokemon.PokemonId}");
    }

    public void Delete(int pokemonId)
    {
        Execute($"Delete from pokemon where 1 = 2 and id = {pokemonId}");
    }

}
