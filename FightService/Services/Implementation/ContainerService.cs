using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using FightService.Exceptions;
using FightService.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace FightService.Services.Implementation
{
  public class ContainerService : IContainerService
  {
    private IDockerClient dockerClient;
    private ILogger<ContainerService> logger;

    public async Task<string> CreateAndStart(string imageTag)
    {
      var containerId = await this.CreateContainer(imageTag);

      await this.StartContainer(containerId);

      return containerId;
    }

    private async Task<string> CreateContainer(string imageTag)
    {
      try
      {
        var createResult = await this.dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
        {
          AttachStdin = true,
          AttachStdout = true,
          AttachStderr = true,
          NetworkDisabled = true,
          Image = imageTag
          //TODO Возможно, сюда добавить User, от кого будут выполняться команды внутри контейнера.
        });

        return createResult.ID;
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, $"Не удалось создать контейнер для образа {imageTag}.");
        throw new ContainerNotCreatedException("Не удалось создать контейнер.", imageTag, ex);
      }
    }

    private async Task StartContainer(string containerId)
    {
      try
      {
        // Chirkov_IA не проверяется на true, т.к. false может быть, если такой контейнер уже запущен. Иначе - эксепшен.
        await this.dockerClient.Containers.StartContainerAsync(containerId, new ContainerStartParameters());
      }
      catch (Exception ex)
      {
        this.logger.LogError(ex, $"Не удалось запустить контейнер {containerId}.");
        throw new ContainerNotStartedException("Не удалось запустить контейнер.", containerId, ex);
      }
    }


    public ContainerService(IDockerClient dockerClient, ILogger<ContainerService> logger)
    {
      this.dockerClient = dockerClient;
      this.logger = logger;
    }
  }
}