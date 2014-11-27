using System;

namespace HelloWorldM2M.Dtos
{
  public class FirmwareDto : ResourceDto
  {
    public string Version { get; set; }
    public string Name { get; set; }
    public Uri Uri { get; set; }
    public string UpdateStatus { get; set; }
  }
}