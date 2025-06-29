using Newtonsoft.Json.Linq;
using SuperSoccerShowdown.Common.Dtos;

namespace SuperSoccerShowdown.PlayerGenerator.Universes.StarWars;

public static class StarWarsMapper
{
    public static int MapToTotalRecords(string json)
    {

        JObject personJsonParser = JObject.Parse(json);
        var totalRecords = (int)personJsonParser["total_records"];
        return totalRecords;
    }

    public static PlayerDto MapToPlayerDto(string json)
    {

        JObject personJsonParser = JObject.Parse(json);
        var id = (int)personJsonParser["result"]["uid"];
        var name = (string)personJsonParser["result"]["properties"]["name"];
        var height = (int)personJsonParser["result"]["properties"]["height"];
        var mass = (int)(double)personJsonParser["result"]["properties"]["mass"];
        return new PlayerDto
        {
            Id = id,
            Name = name,
            Weight = mass,
            Height = height
        };
    }
}
