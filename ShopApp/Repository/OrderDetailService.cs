using Newtonsoft.Json;
using ShopApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopApp.Repository
{
    public class OrderDetailService : IOrderDetailRepository
    {
        readonly JsonSerializerOptions _serializerOptions = new();

        public async Task<OrderDetail> Add(OrderDetail orderDetail)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(orderDetail, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/OrderDetail/";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                OrderDetail orderDetail1 = JsonConvert.DeserializeObject<OrderDetail>(resContent);
                return await Task.FromResult(orderDetail1);
            }
            return null!;
        }
    }
}
