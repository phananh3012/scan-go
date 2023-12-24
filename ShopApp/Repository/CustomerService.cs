using Newtonsoft.Json;
using ShopApp.Model;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShopApp.Repository
{
    public class CustomerService : ICustomerRepository
    {
        readonly JsonSerializerOptions _serializerOptions = new();

        public async Task<Customer> GetById(int userId)
        {
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Customer/{userId}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                Customer customer = JsonConvert.DeserializeObject<Customer>(content);
                MauiProgram.CUS_ID = customer.CustomerId;
                return await Task.FromResult(customer);
            }
            return null!;
        }
        public async Task<Customer> Add(Customer customer)
        {
            string json = JsonSerializer.Serialize(customer, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Customer/";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                Customer customer1 = JsonConvert.DeserializeObject<Customer>(resContent);
                return await Task.FromResult(customer1);
            }
            return null!;
        }

        public async Task<Customer> UpdatePoint(Customer customer)
        {
            string json = JsonSerializer.Serialize(customer, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Customer/{customer.CustomerId}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PutAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<Customer>(resContent);
                return await Task.FromResult(customer);
            }
            return null!;
        }
    }
}
