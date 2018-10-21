using System.Threading.Tasks;

namespace FightService.Services.Interfaces
{
  public interface IContainerService
  {
    Task<string> CreateAndStart(string imageTag);
  }
}