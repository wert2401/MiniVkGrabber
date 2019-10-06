using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiniGraber
{
    static class MyHttpClient
    {
        static HttpClient client;

        public static async Task<string> Get(string URL)
        {
            CheckIfInitialized();
            HttpResponseMessage resp = await client.GetAsync(URL);
            return await resp.Content.ReadAsStringAsync();
        }

        public static async Task<Image> GetImage(string URL)
        {
            CheckIfInitialized();
            return Image.FromStream(await client.GetStreamAsync(URL));
        }

        static void CheckIfInitialized()
        {
            if (client == null) client = new HttpClient();
        }

        public static async Task<bool> CheckConnection()
        {
            CheckIfInitialized();
            bool t = true;
            try
            {
                HttpResponseMessage m = await client.GetAsync("https://google.com/");
            }
            catch
            {
                t = false;
            }
            return t;
        }
    }
}
