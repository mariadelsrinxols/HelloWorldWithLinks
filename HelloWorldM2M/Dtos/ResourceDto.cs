using System;
using System.Security.Policy;

namespace HelloWorldM2M.Dtos
{
  public abstract class ResourceDto
  {
    public Url Href { get; set; }
    public string Id { get; set; }

    //DateTime vs DateTimeOffset as type in Dto? offset generates a bigger return object in xml, but it is more precise
    //Json looks good with both
    public DateTime CreationTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }
    public DateTimeOffset ExpirationTime { get; set; }
  }
}