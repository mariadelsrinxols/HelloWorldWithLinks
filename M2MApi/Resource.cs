using System;

namespace M2MApi
{
  public abstract class Resource : IResource
  {
    protected Resource(DateTimeOffset expirationTime)
    {
      Id = ResourceHelper.GetNextId();
      ExpirationTime = expirationTime;
      CreationTime = DateTimeOffset.UtcNow;
      LastModifiedTime = CreationTime;
    }

    public string Id { get; private set; }
    public DateTimeOffset CreationTime { get; private set; }
    public DateTimeOffset LastModifiedTime { get; private set; }
    public DateTimeOffset ExpirationTime { get; private set; }

    public void SetNewExpirationTime(DateTimeOffset newExpirationTime)
    {
      ExpirationTime = newExpirationTime;
      UpdateLastModifiedTime();
    }

    protected void UpdateLastModifiedTime()
    {
      LastModifiedTime = DateTimeOffset.UtcNow;
    }
  }
}