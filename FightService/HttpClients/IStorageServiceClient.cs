using System.Threading.Tasks;
using FightService.Models;
using Refit;

namespace FightService.HttpClients
{
  public interface IStorageServiceClient
  {
    [Post("/api/battle")]
    Task StartNewBattle([Body] BattleInfo battleInfo);

    [Post("/api/battle/{battleId}/frame")]
    Task AddFrame([AliasAs("battleId")] string battleId, [Body] Frame frame);
  }
}