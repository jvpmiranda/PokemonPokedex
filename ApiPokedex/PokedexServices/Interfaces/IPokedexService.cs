﻿using PokedexModels.Model;

namespace PokedexServices.Interfaces;

public interface IPokedexService
{
    void Delete(int pokemonId);
    IEnumerable<PokemonModel> GetPokemon(string pokemonKey);
    PokemonModel GetPokemon(int pokemonId, int versionId);
    PokemonModelFull GetPokemonFullInfo(int pokemonId, int versionId);
    void Insert(PokemonModel pokemon);
    void Update(PokemonModel pokemon);
}