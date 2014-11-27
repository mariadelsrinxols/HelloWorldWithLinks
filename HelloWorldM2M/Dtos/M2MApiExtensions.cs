using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using M2MApi;

namespace HelloWorldM2M.Dtos
{
  public static class M2MApiExtensions
  {
    public static ConcentratorDto ToDto(this IConcentrator concentrator, string href, string deviceInfoUri, string firmwareJobsUri)
    {
      return new ConcentratorDto
        {
          Id = concentrator.Id,
          CreationTime = concentrator.CreationTime.DateTime,
          LastModifiedTime = concentrator.LastModifiedTime,
          ExpirationTime = concentrator.ExpirationTime,
          DeviceInfo = new Url(deviceInfoUri),
          FirmwareJobs = new Url(firmwareJobsUri),
          Href = new Url(href)
        };
    }

    public static DeviceInfoDto ToDto(this IDeviceInfo deviceInfo, string href)
    {
      return new DeviceInfoDto
        {
          Id = deviceInfo.Id,
          CreationTime = deviceInfo.CreationTime.DateTime,
          LastModifiedTime = deviceInfo.LastModifiedTime,
          ExpirationTime = deviceInfo.ExpirationTime,
          DeviceLabel = deviceInfo.DeviceLabel,
          DeviceType = deviceInfo.DeviceType,
          Manufacturer = deviceInfo.Manufacturer,
          Model = deviceInfo.Model,
          HardwareVersion = deviceInfo.HardwareVersion,
          SoftwareVersion = deviceInfo.SoftwareVersion,
          Href = new Url(href)
        };
    }

    public static FirmwareDto ToDto(this IFirmware firmware, string href)
    {
      return new FirmwareDto
        {
          Id = firmware.Id,
          CreationTime = firmware.CreationTime.DateTime,
          LastModifiedTime = firmware.LastModifiedTime,
          ExpirationTime = firmware.ExpirationTime,
          Version = firmware.Version,
          Name = firmware.Name,
          Uri = firmware.Uri,
          UpdateStatus = firmware.UpdateStatus,
          Href = new Url(href)
        };
    }

    
    public static IConcentrator FromDto(this ConcentratorDto dto)
    {
      return new Concentrator(dto.ExpirationTime);
    }

    public static IDeviceInfo FromDto(this DeviceInfoDto dto)
    {
      return new DeviceInfo(dto.ExpirationTime)
        {
          DeviceLabel = dto.DeviceLabel,
          DeviceType = dto.DeviceType,
          Manufacturer = dto.Manufacturer,
          Model = dto.Model,
          HardwareVersion = dto.HardwareVersion,
          SoftwareVersion = dto.SoftwareVersion
        };
    }

    public static IFirmware FromDto(this FirmwareDto dto)
    {
      return new Firmware(dto.ExpirationTime, dto.Version, dto.Name, dto.Uri)
        {
          UpdateStatus = dto.UpdateStatus
        };

    }

  }
}