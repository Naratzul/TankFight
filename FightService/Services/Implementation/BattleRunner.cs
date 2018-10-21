using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FightService.Models;
using FightService.Services.Interfaces;
using GameLogic.Implementations.Game;
using GameLogic.Interfaces.Public;
using Microsoft.Extensions.Logging;

namespace FightService.Services.Implementation
{
  public class BattleRunner: IBattleRunner
  {
    private readonly IContainerService containerService;
    private readonly IMovesService movesService;
    private readonly ILogger<BattleRunner> logger;

    public async Task<User> RunBattle(string[] imageTags, IMapInfo mapInfo, IGameSettings gameSettings)
    {
      List<User> users = new List<User>();

      foreach (var imageTag in imageTags)
      {
        var user = new User
        {
          UserId = Guid.NewGuid().ToString(),
          ImageTag = imageTag
        };

        try
        {
          user.ContainerId = await this.containerService.CreateAndStart(imageTag);
        }
        catch (Exception ex)
        {
          this.logger.LogError(ex, $"Не удалось создать контейнер {imageTag}");
        }

        users.Add(user);
      }

      var game = new Game(users.Select(x => x.UserId).ToArray(), mapInfo, gameSettings);

      throw new System.NotImplementedException();
    }

    public BattleRunner(IContainerService containerService, 
      IMovesService movesService, 
      ILogger<BattleRunner> logger)
    {
      this.containerService = containerService;
      this.movesService = movesService;
      this.logger = logger;
    }
  }
}