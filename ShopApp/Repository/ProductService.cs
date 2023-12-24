using Newtonsoft.Json;
using ShopApp.Model;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShopApp.Repository
{
    public class ProductService : IProductRepository
    {
        readonly JsonSerializerOptions _serializerOptions = new();

        public async Task<Product> GetByBarcode(string barcode)
        {
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Product/Barcode/{barcode}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                Product product = JsonConvert.DeserializeObject<Product>(content);
                return await Task.FromResult(product);
            }
            return null!;
        }

        public async Task<Product> GetById(int id)
        {
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Product/{id}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                Product product = JsonConvert.DeserializeObject<Product>(content);
                return await Task.FromResult(product);
            }
            return null!;
        }

        public async Task<Product> UpdateQty(Product product)
        {
            string json = JsonSerializer.Serialize(product, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/Product/{product.ProductId}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PutAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(resContent);
                return await Task.FromResult(product);
            }
            return null!;
        }
    }
}
