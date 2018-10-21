using System.Threading.Tasks;
using FightService.Models;
using GameLogic.Interfaces.Public;

namespace FightService.Services.Interfaces
{
  public interface IBattleRunner
  {
    Task<User> RunBattle(string[] imageTags, IMapInfo mapInfo, IGameSettings gameSettings);
  }
}