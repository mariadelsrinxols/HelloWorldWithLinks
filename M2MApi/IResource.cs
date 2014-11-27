using System;
using System.Globalization;

namespace M2MApi
{
  public interface IResource
  {
    string Id { get; }
    DateTimeOffset CreationTime { get; }
    DateTimeOffset LastModifiedTime { get; }
    DateTimeOffset ExpirationTime { get; }
    void SetNewExpirationTime(DateTimeOffset newExpirationTime);
  }
}
