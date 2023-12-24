using Newtonsoft.Json;
using ShopApp.Model;
using System.Text;
using System.Text.Json;

namespace ShopApp.Repository
{
    public class LogProductService : ILogProductRepository
    {
        readonly JsonSerializerOptions _serializerOptions = new();

        public async Task<LogProduct> Add(LogProduct log)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(log, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/LogProduct/";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                LogProduct log1 = JsonConvert.DeserializeObject<LogProduct>(resContent);
                return await Task.FromResult(log1);
            }
            return null!;
        }
    }
}
