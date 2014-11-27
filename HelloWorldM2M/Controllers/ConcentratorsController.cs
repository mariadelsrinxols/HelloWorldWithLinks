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
  public class ConcentratorsController : ApiController
  {
    //I know this is wrong!!! (we don't want to have one in each controller!!)
    private static ICSEBase m2m = null;
    public ConcentratorsController()
    {
      if(m2m == null)
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

    // GET api/concentrators
    public IEnumerable<ConcentratorDto> Get()
    {
      var concentrators = m2m.GetNodes<Concentrator>();
      return concentrators.Select(c => c.ToDto(GetConcentratorUri(c.Id), GetDeviceInfoUri(c.Id), GetFirmwareJobsUri(c.Id)));
    }

    // GET api/concentrators/5
    public ConcentratorDto Get(string id)
    {
      var concentrator = m2m.GetNode<Concentrator>(id);
      return concentrator == null ? null : concentrator.ToDto(GetConcentratorUri(concentrator.Id), GetDeviceInfoUri(concentrator.Id), GetFirmwareJobsUri(concentrator.Id));
    }

    // GET api/concentrators/{id}/deviceinfo
    [Route("api/concentrators/{id}/deviceinfo", Name = "DeviceInfoOfConcentrator")]
    public DeviceInfoDto GetDeviceInfoOfConcentrator(string id)
    {
      var concentrator = m2m.GetNode<Concentrator>(id);
      if(concentrator == null)
        throw new HttpResponseException(HttpStatusCode.NotFound);
      var deviceInfo = concentrator.DeviceInfo;
      if (deviceInfo == null)
        throw new HttpResponseException(HttpStatusCode.NotFound);

      return deviceInfo.ToDto(GetDeviceInfoUri(concentrator.Id));
    }

    // POST api/concentrators
    public HttpResponseMessage Post([FromBody]ConcentratorDto concentratorDto)
    {
      var concentrator = concentratorDto.FromDto();
      m2m.AddNode(concentrator);

      var response = Request.CreateResponse(HttpStatusCode.Created, concentrator.ToDto(GetConcentratorUri(concentrator.Id), "", ""));
      response.Headers.Location = new Uri(GetConcentratorUri(concentrator.Id));
      return response;
    }

    // POST api/concentrators/{id}/deviceinfo
    public HttpResponseMessage PostDeviceInfo(string id, [FromBody]DeviceInfoDto deviceInfoDto)
    {
      HttpResponseMessage response;

      var concentrator = m2m.GetNode<Concentrator>(id);
      if (concentrator.DeviceInfo != null)
      {
        response = Request.CreateResponse(HttpStatusCode.NotModified, concentrator.DeviceInfo.ToDto(GetDeviceInfoUri(concentrator.Id)));
      }
      else
      {
        var deviceInfo = deviceInfoDto.FromDto();
        concentrator.DeviceInfo = deviceInfo;
        response = Request.CreateResponse(HttpStatusCode.Created, deviceInfo.ToDto(GetDeviceInfoUri(concentrator.Id)));
      }
      var uri = GetDeviceInfoUri(id);
      response.Headers.Location = new Uri(uri);
      return response;
    }
    
    // DELETE api/concentrators/5
    public void Delete(string id)
    {
      var node = m2m.GetNode<Concentrator>(id);
      if(node != null)
        m2m.RemoveNode(node);
    }


    private string GetFirmwareJobsUri(string concentratorId)
    {
      return Url.Link("FirmwaresOfConcentrator", new { id = concentratorId }) ?? "";
    }

    private string GetDeviceInfoUri(string concentratorId)
    {
      return Url.Link("DeviceInfoOfConcentrator", new { id = concentratorId }) ?? "";
    }

    private string GetConcentratorUri(string concentratorId)
    {
      return Url.Link("DefaultApi", new { id = concentratorId }) ?? "";
    }
  }
}