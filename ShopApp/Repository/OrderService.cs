using Newtonsoft.Json;
using ShopApp.Model;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShopApp.Repository
{
    public class OrderService : IOrderRepository
    {
        readonly JsonSerializerOptions _serializerOptions = new();

        public async Task<Order> Add(Order order)
        {
            string json = JsonSerializer.Serialize(order, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Order/";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                Order order1 = JsonConvert.DeserializeObject<Order>(resContent);
                return await Task.FromResult(order1);
            }
            return null!;
        }

        public async Task<Order> Update(Order order)
        {
            string json = JsonSerializer.Serialize(order, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Order";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PutAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                order = JsonConvert.DeserializeObject<Order>(resContent);
                return await Task.FromResult(order);
            }
            return null!;
        }
    }
}
