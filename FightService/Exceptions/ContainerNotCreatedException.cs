using System;

namespace FightService.Exceptions
{
  internal sealed class ContainerNotCreatedException : Exception
  {
    public string ImageTag { get; }

    public ContainerNotCreatedException(string message, string imageTag)
    {
      this.ImageTag = imageTag;
    }

    public ContainerNotCreatedException(string message, string imageTag, Exception innerException) : base(message, innerException)
    {
      this.ImageTag = imageTag;
    }
  }
}