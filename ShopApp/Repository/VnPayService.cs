using Newtonsoft.Json;
using ShopApp.Model;
using System.Text;
using System.Text.Json;

namespace ShopApp.Repository
{
    public class VnPayService : IVnPayService
    {
        readonly JsonSerializerOptions _serializerOptions = new();

        public async Task<string> CreatePaymentUrl(PaymentInformation model, int id)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(model, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/VnPay/{id}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                return await Task.FromResult(resContent);
            }
            return null!;
        }

        public async Task<PaymentResponse> PaymentExecute(string address)
        {
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = address;
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                PaymentResponse paymentResponse = JsonConvert.DeserializeObject<PaymentResponse>(content);
                return await Task.FromResult(paymentResponse);
            }
            return null!;
        }
    }
}
