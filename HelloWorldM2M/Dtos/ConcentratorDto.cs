using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace HelloWorldM2M.Dtos
{
  public class ConcentratorDto : ResourceDto
  {
    //Type Uri doesn't work in Json and Url generates a big response.. should we use string?
    public Url DeviceInfo { get; set; }
    public Url FirmwareJobs { get; set; }
  }
}