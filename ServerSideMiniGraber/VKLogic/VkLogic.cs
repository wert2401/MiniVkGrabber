using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;
using ServerSideMiniGraber.Objects;

namespace ServerSideMiniGraber.VKLogic
{
    public class VkLogic
    {
        string apiPath = "https://api.vk.com/method/";
        string token;
        public VkLogic(string token)
        {
            this.token = token;
        }

        public async Task<string> GetPersonFriends(string id)
        {
            string[] fields = new string[] { "bdate", "city", "photo_200_orig" };
            Dictionary<string, string> pars = new Dictionary<string, string>() { { "user_id", id }, { "name_case", "nom" }, { "count", "200" }, { "order", "name" } };
            string method = "friends.get";
            string result = await MyHttpClient.Get(GetMethodUri(method, pars, fields));
            return result;
        }

        public async Task<string> GetPersonId(string address)
        {
            string rBody = await MyHttpClient.Get("https://vk.com/" + address);
            string pattern = "([0-9]+)_([0-9]+)";
            string id = Regex.Match(rBody, pattern).ToString();
            return id.Split('_')[0];
        }

        public async Task<string> GetPerson(string id)
        {
            Dictionary<string, string> pars = new Dictionary<string, string>() { { "user_ids", id }, { "name_case", "Nom" } };
            string method = "users.get";

            return await MyHttpClient.Get(GetMethodUri(method, pars));
        }

        string GetMethodUri(string methodName, Dictionary<string, string> pars, string[] fields)
        {
            string methodUri = apiPath;
            methodUri += methodName + "?";
            foreach (var item in pars)
            {
                methodUri += item.Key + "=" + item.Value + "&";
            }
            methodUri += "fields=";
            foreach (var item in fields)
            {
                methodUri += item + ",";
            }
            methodUri += "&access_token=" + token +"&v=5.101";
            return methodUri;
        }

        string GetMethodUri(string methodName, Dictionary<string, string> pars)
        {
            string methodUri = apiPath;
            methodUri += methodName + "?";
            foreach (var item in pars)
            {
                methodUri += item.Key + "=" + item.Value + "&";
            }
            methodUri += "&access_token=" + token + "&v=5.101";
            return methodUri;
        }
    }
}
