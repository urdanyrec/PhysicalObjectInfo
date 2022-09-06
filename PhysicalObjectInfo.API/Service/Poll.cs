using PhysicalObjectInfo.Domain;
using System.Text;
using System.Text.Json;

namespace PhysicalObjectInfo.API.Service
{
    public class Poll : IpollService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Poll(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;
        public List<PhysicalObject> PhysicalObjects { get; set; }
        public List<Parameter> Parameters { get; set; }

        public async void PollObjects()
        {
            Console.WriteLine("Синглтон запущен");
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
