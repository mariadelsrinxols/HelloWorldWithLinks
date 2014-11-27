using System;
using System.Security.Policy;

namespace M2MApi
{
  public interface IFirmware : IResource
  {
    string Version { get; }
    string Name { get;  }
    Uri Uri { get;  }
    string UpdateStatus { get; set; }
  }
}