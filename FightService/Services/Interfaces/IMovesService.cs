using System.Threading.Tasks;
using GameLogic.Interfaces.Public;

namespace FightService.Services.Interfaces
{
  public interface IMovesService
  {
    Task<IUserMove> GetMovesFromContainer(IMapInfo mapInfo, string containerId);
  }
}