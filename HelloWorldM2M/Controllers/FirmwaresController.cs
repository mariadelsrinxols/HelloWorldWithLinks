using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelloWorldM2M.Dtos;
using M2MApi;

namespace HelloWorldM2M.Controllers
{
  public class FirmwaresController : ApiController
  {
    //I know this is wrong!!! (we don't want to have one in each controller!!)
    private static ICSEBase m2m = null;
    public FirmwaresController()
    {
      if (m2m == null)
        InitializeApi();
    }

    private static void InitializeApi()
    {
      var expirationTime = DateTimeOffset.UtcNow.AddDays(5);
      var c1 = new Concentrator(expirationTime)
        {
          DeviceInfo = new DeviceInfo(expirationTime)
            {
              DeviceLabel = "Concentrator 1",
              DeviceType = "Concentrator",
              Manufacturer = "Kamstrup",
              Model = "OMNICON",
              HardwareVersion = "A1",
              SoftwareVersion = "J6"
            }
        };

      var c2 = new Concentrator(expirationTime)
        {
          DeviceInfo = new DeviceInfo(expirationTime)
            {
              DeviceLabel = "Concentrator 2",
              DeviceType = "Concentrator",
              Manufacturer = "Kamstrup",
              Model = "OMNICON",
              HardwareVersion = "A1",
              SoftwareVersion = "H15"
            }
        };
      var firmware = new Firmware(DateTimeOffset.UtcNow.AddDays(2), "J6", "the new shit!",
                                  new Uri("http://localhost:52480/api/concentrators"));
      c2.CreateFirmwareJob(firmware);

      m2m = new CSEBase();
      m2m.AddNode(c1);
      m2m.AddNode(c2);
    }

    // GET api/concentrators/{id}/firmwares
    public IEnumerable<FirmwareDto> GetFirmwaresOfConcentrator(string id)
    {
      var concentrator = m2m.GetNode<Concentrator>(id);
      if (concentrator == null)
        throw new HttpResponseException(HttpStatusCode.NotFound);
      var jobs = concentrator.FirmwareJobs;
      if (jobs == null)
        throw new HttpResponseException(HttpStatusCode.NotFound);

      return jobs.Select(f => f.ToDto(GetFirmwareJobUriById(concentrator.Id, f.Id)));
    }

    // GET api/concentrators/{id}/firmwares/{firmwareId}
    public FirmwareDto GetFirmwareOfConcentratorById(string id, string firmwareId)
    {
      var concentrator = m2m.GetNode<Concentrator>(id);
      if (concentrator == null)
        throw new HttpResponseException(HttpStatusCode.NotFound);
      var job = concentrator.FirmwareJobs.FirstOrDefault(f => f.Id == firmwareId);
      if (job == null)
        throw new HttpResponseException(HttpStatusCode.NotFound);

      return job.ToDto(job.Id);
    }


    private string GetFirmwareJobUriById(string concentratorId, string id)
    {
      return Url.Link("FirmwareOfConcentratorById", new { id = concentratorId, firmwareId = id }) ?? "";
    }
  }
}