using System;
using System.Security.Policy;

namespace M2MApi
{
  public class Firmware : Resource, IFirmware
  {
    private string updateStatus;

    public Firmware(DateTimeOffset expirationTime, string version, string name, Uri url) : base(expirationTime)
    {
      Uri = url;
      Name = name;
      Version = version;
    }

    public string Version { get; set; }
    public string Name { get; set; }
    public Uri Uri { get; set; }
    public string UpdateStatus
    {
      get { return updateStatus; }
      set
      {
        updateStatus = value;
        UpdateLastModifiedTime();
      }
    }
  }
}