# QuickSilverVpn.cs
Mobile-API for [QuickSilver VPN](https://play.google.com/store/apps/details?id=com.quicksilvervpn) the ultimate solution that provides you with a fast, secure, and unrestricted online experience, all at absolutely no cost to you

## Example
```cs
using System;
using QuickSilverVpnApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new QuickSilverVpn();
            string servers = await api.GetServers();
            Console.WriteLine(servers);
        }
    }
}
```
