namespace M2MApi
{
  public interface IDeviceInfo : IResource
  {
    string DeviceLabel { get; }
    string Manufacturer { get;  }
    string Model { get; }
    string DeviceType { get; }
    string SoftwareVersion { get; set; }
    string HardwareVersion { get; set; }
  }
}