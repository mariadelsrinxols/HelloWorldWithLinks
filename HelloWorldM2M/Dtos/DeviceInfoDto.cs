namespace HelloWorldM2M.Dtos
{
  public class DeviceInfoDto : ResourceDto
  {
    public string DeviceLabel { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string DeviceType { get; set; }
    public string SoftwareVersion { get; set; }
    public string HardwareVersion { get; set; }
  }
}