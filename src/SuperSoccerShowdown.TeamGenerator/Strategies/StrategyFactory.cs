using SuperSoccerShowdown.Common.Dtos;
using SuperSoccerShowdown.TeamGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSoccerShowdown.TeamGenerator.Strategies;

internal class StrategyFactory : IStrategyFactory
{
    private readonly IDictionary<StrategyType, ITeamStrategy> _strategies;

    public StrategyFactory(IEnumerable<ITeamStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(
            u => u.Type,
            u => u
        );
    }

    public ITeamStrategy Create(StrategyType strategyType)
    {
        if (_strategies.TryGetValue(strategyType, out var strategy))
            return strategy;

        throw new ArgumentException($"Strategy type '{strategyType}' not registered.");
    }
}
