using System.Collections.Generic;

namespace M2MApi
{
  public interface IConcentrator : INode
  {
    IDeviceInfo DeviceInfo { get; set; }
    IEnumerable<IFirmware> FirmwareJobs { get;}

    void CreateFirmwareJob(IFirmware firmware);
  }
}