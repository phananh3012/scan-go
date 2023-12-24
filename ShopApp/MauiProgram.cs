using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Camera.MAUI;
using Syncfusion.Maui.Core.Hosting;
using System.Net;
using System.Net.Sockets;

namespace ShopApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCameraView()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
      
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static readonly string con_string = "Data Source = NAMNP-2020\\SQLEXPRESS; Initial Catalog = POS; User Id = sa; Password = 123; Integrated Security = True; Encrypt = False";
        public static SqlConnection con = new(con_string);
  
        public static int cus_id;
        public static int CUS_ID
        {
            get { return cus_id; }
            set { cus_id = value; }
        }
        public static int user_id;
        public static int USER_ID
        {
            get { return user_id; }
            set { user_id = value; }
        }
        public static HttpClientHandler ClientHandler()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        };
            return clientHandler;
        }

        public static string GetLocalIpAddress()
        {
            string IP4Address = "172.20.10.4";
            return IP4Address;
        }

    }
}