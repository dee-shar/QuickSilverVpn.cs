using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace QuickSilverVpnApi
{
    public class QuickSilverVpn
    {
        private readonly HttpClient httpClient;
        private readonly string uuid = Guid.NewGuid().ToString();
        private readonly string apiUrl = "https://api.quicksilvervpn.com/api/v1";

        public QuickSilverVpn()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("okhttp/3.14.9");
        }
        
        public static string GenerateDeviceSerialNumber()
        {
            var random = new Random();
            return new string(Enumerable.Repeat(
                "abcdefghijklmnopqrstuvwxyz0123456789", 16).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateWgPublicKey()
        {
            var data = new byte[32];
            RandomNumberGenerator.Fill(data);
            return Convert.ToBase64String(data).TrimEnd('-').PadRight(44, '=');
        }
        
        public async Task<string> Register()
        {
            var data = JsonContent.Create(new
            {
                model = "RMX3551",
                brand = "realme",
                productName = "RMX3551",
                manufacture = "realme",
                device = "gracelte",
                deviceLanguage = "ru",
                timeZone = "GMT+01:00",
                appId = "com.quicksilvervpn",
                screenDensityDpi = 480,
                screenHeightPx = 1920,
                screenWidthPx = 1080,
                deviceSerialNumber = GenerateDeviceSerialNumber(),
                networkOperator = new Random().Next(100000, 1000000).ToString(),
                wgPublicKey = GenerateWgPublicKey(),
                uuid = uuid
            });
            var response = await httpClient.PostAsync($"{apiUrl}/register", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetServers()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/servers?uuid={uuid}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
