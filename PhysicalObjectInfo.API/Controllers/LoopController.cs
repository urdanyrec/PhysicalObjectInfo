using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PhysicalObjectInfo.Domain;
using PhysicalObjectInfo.Infrastructure;
using PhysicalObjectInfo.Infrastructure.Repository;

namespace PhysicalObjectInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoopController : ControllerBase
    {
        //private readonly Context _context;
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly PhysicalObjectRepository _PhysicalObjectRepository;
        public LoopController(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        /* public LoopController(Context context)
          {
              _context = context;
              _PhysicalObjectRepository = new PhysicalObjectRepository(_context);
          }*/

        public List<Parameter> Parameters { get; set; }
        public List<PhysicalObject> PhysicalObjects { get; set; }

        
        public async Task OnGet()
        {
            //get список PhysicalObject
            var httpClientPoll = new HttpClient();
            PhysicalObjects = await httpClientPoll.GetFromJsonAsync<List<PhysicalObject>>("https://localhost:7230/api/PhysicalObject");
            //httpClientPoll.Dispose();
            
            //опрос каждого PhysicalObject
            foreach (var ph in PhysicalObjects) 
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, ph.URL);
                var httpClient = new HttpClient();
                try
                {
                    Parameters = await httpClient.GetFromJsonAsync<List<Parameter>>(ph.URL); //<List<Parameter>>("http://192.168.3.12/api");
                    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        //Сохранение каждого параметра из списка получаемых параметров
                        foreach (var p in Parameters)
                        {
                            //dynamic dynJson = JsonConvert.DeserializeObject(json);
                            var testparam = new Parameter { Id = Guid.NewGuid(), ObjectId = Guid.Empty, Type = p.Type, Value = p.Value, Dimension = p.Dimension, PhysicalObjectId = new Guid(ph.Id.ToString()), PollingTime = DateTime.Now }; //PollingTime = , 
                            string ParamJson = JsonSerializer.Serialize(testparam);
                            Console.WriteLine(ParamJson);
                            var httpClientRequest = new HttpClient();
                            var responce = await httpClientRequest.PostAsync("https://localhost:7230/api/Parameter", new StringContent(ParamJson, Encoding.UTF8, "application/json"));//testparam);  //PostAsJsonAsync{
                            if ((responce.IsSuccessStatusCode) || ((int)responce.StatusCode != 500))
                            {
                                var httpClientState = new HttpClient();
                                ph.State = "On";
                                var UpdatedPhysicalObject = JsonSerializer.Serialize(ph);
                                var response = await httpClientState.PutAsync("https://localhost:7230/api/PhysicalObject", new StringContent(UpdatedPhysicalObject, Encoding.UTF8, "application/json"));
                            }
                            else
                            {
                                var httpClientState = new HttpClient();
                                ph.State = "Off";
                                var UpdatedPhysicalObject = JsonSerializer.Serialize(ph);
                                var response = await httpClientState.PutAsync("https://localhost:7230/api/PhysicalObject", new StringContent(UpdatedPhysicalObject, Encoding.UTF8, "application/json"));
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    var httpClientState = new HttpClient();
                    ph.State = "Off";
                    var UpdatedPhysicalObject = JsonSerializer.Serialize(ph);
                    var response = await httpClientState.PutAsync("https://localhost:7230/api/PhysicalObject", new StringContent(UpdatedPhysicalObject, Encoding.UTF8, "application/json"));
                }  
            }
        }
    }
}
/*
var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://192.168.3.12/api");
var httpClient = new HttpClient();
Parameters = await httpClient.GetFromJsonAsync<List<Parameter>>("http://192.168.3.12/api"); //<List<Parameter>>("http://192.168.3.12/api");

var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
if (httpResponseMessage.IsSuccessStatusCode)
{
    foreach (var p in Parameters)
    {
        //dynamic dynJson = JsonConvert.DeserializeObject(json);
        var testparam = new Parameter { Id = Guid.NewGuid(), ObjectId = Guid.Empty, Type = p.Type, Value = p.Value, Dimension = p.Dimension, PhysicalObjectId = new Guid("69435550-409e-48a0-ab35-dc56a34e2f10"), PollingTime = DateTime.Now }; //PollingTime = , 
        string ParamJson = JsonSerializer.Serialize(testparam);
        Console.WriteLine(ParamJson);
        var httpClientRequest = new HttpClient();
        var responce = await httpClientRequest.PostAsync("https://localhost:7230/api/Parameter", new StringContent(ParamJson, Encoding.UTF8, "application/json"));//testparam);  //PostAsJsonAsync{
        if ((int)responce.StatusCode != 400)
        {

        }
    }                             
}
*/


/*foreach(var p in PhysicalObjects)
{
    await _PhysicalObjectRepository.AddAsyncPhObject(p);
}
*/
//PhysicalObjects = new List<PhysicalObject>();

// using var contentStream =
//await httpResponseMessage.Content.ReadAsStreamAsync();

//PhysicalObjects = await HttpClient.

/*JsonSerializer.DeserializeAsync
<IEnumerable<PhysicalObject>>(contentStream);
//Console.WriteLine(PhysicalObjects);
}*/
/* //тест физобъекта
public async Task Test()
{
    var physicalobject = new PhysicalObject { Id = System.Guid.NewGuid(), ObjectId = System.Guid.NewGuid(), Series = "testcontrol", State = "testcontrol", URL = "testcontrol", ObjectTechId = System.Guid.Empty };
    string ParamJson = JsonSerializer.Serialize(physicalobject);
    Console.WriteLine(ParamJson);
    var httpClientRequest = new HttpClient();
        await httpClientRequest.PostAsJsonAsync("https://localhost:7230/api/PhysicalObject", physicalobject);  //PostAsJsonAsync


}
*/
/*
public async Task testfull()
{
    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://192.168.3.12/api");
    //var httpClient = _httpClientFactory.CreateClient();
    var httpClient = new HttpClient();
    Parameters = await httpClient.GetFromJsonAsync<Parameter>("http://192.168.3.12/api"); //<List<Parameter>>("http://192.168.3.12/api");
    var listparam = new List<Parameter>();

    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

    if (httpResponseMessage.IsSuccessStatusCode)
    {
        var testparam = new Parameter { Id = Guid.NewGuid(), ObjectId = Guid.NewGuid(), Type = Parameters.Type, Value = Parameters.Value, Dimension = "testcontrol", PhysicalObjectId = new Guid("ef113185-77e0-470c-9598-fcb1768f18c9") }; //PollingTime = , 
        listparam[0] = testparam;
        var testph = new PhysicalObject { Id = new Guid("69435550-409e-48a0-ab35-dc56a34e2f10"), ObjectId = new Guid("ef113185-77e0-470c-9598-fcb1768f18c9"), Series = "ESP8266", State = "On", URL = "testurl", ObjectTechId = Guid.Empty, Parameters = listparam };
        string ParamJson = JsonSerializer.Serialize(testph);
        Console.WriteLine(ParamJson);
        var httpClientRequest = new HttpClient();
        var responce = await httpClientRequest.PostAsync("https://localhost:7230/api/Parameter", new StringContent(ParamJson, Encoding.UTF8, "application/json"));//testparam);  //PostAsJsonAsync{
        if (responce.IsSuccessStatusCode)
        {

        }
    }
}
*/

