using Newtonsoft.Json;
using ShopApp.Model;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShopApp.Repository
{
    public class UserService : IUserRepository
    {
        readonly JsonSerializerOptions _serializerOptions = new();
        public async Task<User> Add(User user)
        {
            string json = JsonSerializer.Serialize(user, _serializerOptions);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/User/";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                string resContent = response.Content.ReadAsStringAsync().Result;
                User user1 = JsonConvert.DeserializeObject<User>(resContent);
                return await Task.FromResult(user1);
            }
            return null!;

        }
        public async Task<User> Login(string username, string password)
        {
            var client = new HttpClient(MauiProgram.ClientHandler());
            string url = $"https://{MauiProgram.GetLocalIpAddress()}:7038/api/User/{username}/{password}";
            client.BaseAddress = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                User user = JsonConvert.DeserializeObject<User>(content);
                MauiProgram.USER_ID = user.Userid;
                return await Task.FromResult(user);
            }
            return null!;
        }
    }
}
