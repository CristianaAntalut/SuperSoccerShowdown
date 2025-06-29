using PokeApiNet;
using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.Poke;

public static class PokemonMapper
{
    public static PlayerDto MapToPlayer(this Pokemon pokemon)
    {
        var player = new PlayerDto
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Height = pokemon.Height,
            Weight = pokemon.Weight
        };
        return player;
    }

}
