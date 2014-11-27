using System;

namespace M2MApi
{
  public class DeviceInfo : Resource, IDeviceInfo
  {
    private string softwareVersion;
    private string hardwareVersion;

    public DeviceInfo(DateTimeOffset expirationTime) : base(expirationTime)
    {
    }

    public string DeviceLabel { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public string DeviceType { get; set; }

    public string SoftwareVersion
    {
      get { return softwareVersion; }
      set
      {
        softwareVersion = value;
        UpdateLastModifiedTime();
      }
    }

    public string HardwareVersion
    {
      get { return hardwareVersion; }
      set
      {
        hardwareVersion = value;
        UpdateLastModifiedTime();
      }
    }
  }
}