using System;
using System.Threading.Tasks;
using FightService.Services.Interfaces;
using GameLogic.Interfaces.Public;
using Microsoft.AspNetCore.Mvc;

namespace FightService.Controllers
{
  [Route("api/[controller]")]
  public class FightController : Controller
  {
    private readonly IBattleRunner battleRunner;

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
      if (imageTags == null || imageTags.Length == 0)
      {
        return this.BadRequest();
      }

      if (mapInfo == null)
      {
        return this.BadRequest();
      }

      if (gameSettings == null)
      {
        return this.BadRequest();
      }

      await this.battleRunner.RunBattle(imageTags, mapInfo, gameSettings);

      throw new NotImplementedException();
    }

    public FightController(IBattleRunner battleRunner)
    {
      this.battleRunner = battleRunner;
    }
  }
}