using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BackendApp
{
    public class Functions
    {
        public static async Task<JObject> AsyncHttpRequest(string link, string token, string cookie)
        {
            using (HttpClient client = new HttpClient())
            {
                // HTTP Headers
                client.DefaultRequestHeaders.Add("cookie", cookie);
                client.DefaultRequestHeaders.Add("x-ig-app-id", token);

                HttpResponseMessage response = await client.GetAsync(link);
                JObject jsonObject;
                string content = await response.Content.ReadAsStringAsync();
                jsonObject = JObject.Parse(content);
                return jsonObject;
            }
        }

        public static List<string> CompareFollows(InstagramResponse response1, InstagramResponse response2)
        {
            HashSet<string> followers1 = new HashSet<string>();
            HashSet<string> followers2 = new HashSet<string>();

            foreach (var user in response1.Users)
            {
                followers1.Add(user.Username);
            }
            foreach (var user in response2.Users)
            {
                followers2.Add(user.Username);
            }
            followers1.IntersectWith(followers2);

            List<string> commonFollowers = followers1.ToList();
            return commonFollowers;
        }
        public static async Task<JObject> RequestCaller(string link, string token, string cookie, long count)
        {
            string url = link + 0;
            JObject result = new JObject();

            if (count > 1200)
            {
                Console.WriteLine("Takip edilen yada takipçi sayısı çok fazla");
            }
            else
            {
                long counter = count / 200;
                long mod = count % 200;

                for (int i = 0; i < counter; i++)
                {
                    url = link + "/following/?count=200&max_id=" + (i * 200);
                    JObject dataInfo = await AsyncHttpRequest(url, token, cookie);
                    result.Merge(dataInfo); // dataInfo'yu result ile birleştir
                }

                url = link + "/following/?count=200&max_id=" + (count - mod);
                JObject lastDataInfo = await AsyncHttpRequest(url, token, cookie);
                result.Merge(lastDataInfo); // Son datayı da result ile birleştir
            }

            return result;
        }


    }
}
