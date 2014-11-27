using System;
using System.Collections.Generic;

namespace M2MApi
{
  public class Concentrator : Resource, IConcentrator
  {
    private readonly List<IFirmware> firmwareJobs;

    public Concentrator(DateTimeOffset expirationTime) : base(expirationTime)
    {
      firmwareJobs = new List<IFirmware>();
    }
    public IDeviceInfo DeviceInfo { get; set; }
    public IEnumerable<IFirmware> FirmwareJobs
    {
      get { return firmwareJobs; }
    }

    public void CreateFirmwareJob(IFirmware firmware)
    {
      firmwareJobs.Add(firmware);
    }
  }
}
