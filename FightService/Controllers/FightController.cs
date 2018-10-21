using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using GameLogic.Implementations.Game;
using GameLogic.Interfaces.Public;
using Microsoft.AspNetCore.Mvc;

namespace FightService.Controllers
{
  [Route("api/[controller]")]
  public class FightController : Controller
  {
    private IDockerClient dockerClient;

    /// <summary>
    /// Играет бой:
    /// 1. Создает контейнеры из переданных имен образов, запускает, аттачится к ним;
    /// 2. Запускает игру с переданной картой и параметрами;
    /// 3. Передает в контейнеры данные;
    /// 4. Сохраняет результаты хода в StorageService;
    /// 5. Возвращает тэг победителя;
    /// </summary>
    /// <param name="imageTags">Список образов-ботов</param>
    /// <param name="mapInfo">Данные о карте</param>
    /// <param name="gameSettings">Настройки боя</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> RunBattle(string[] imageTags, IMapInfo mapInfo, IGameSettings gameSettings)
    {
      //TODO Для тестового боя (когда игрок тестит свой код), не нужно создавать тестовые контейнеры, а возвращать случайный результат, не зависящий от входных данных.

      var game = new Game(imageTags, mapInfo, gameSettings);

      var containers = new List<string>();

      foreach (var imageTag in imageTags)
      {
        var createResult = await this.dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
        {
          AttachStdin = true,
          AttachStdout = true,
          AttachStderr = true,
          NetworkDisabled = true,
          Image = imageTag
        });

        //TODO контейнер может не подняться
        containers.Add(createResult.ID);
      }

      throw new NotImplementedException();
    }

    public FightController(IDockerClient dockerClient)
    {
      this.dockerClient = dockerClient;
    }
  }
}