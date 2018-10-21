using System.Threading.Tasks;
using GameLogic.Interfaces.Public;

namespace FightService.Services.Interfaces
{
  public interface IMovesService
  {
    Task<IUserMove> GetMoves(IMapInfo mapInfo, string containerId);
  }
}