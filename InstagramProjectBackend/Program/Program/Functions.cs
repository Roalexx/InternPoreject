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
        public static  async Task<JObject> AsyncHttpRequest(string link, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                // HTTP Headers
                client.DefaultRequestHeaders.Add("cookie", "mid=ZMtaqQALAAFIsrg55_kPJsY-hPgb; ig_did=89F1C663-68C0-417A-834B-B87841E39AF2; ig_nrcb=1; datr=qFrLZIMGM-iWwZy7pkG_ltkN; fbm_124024574287414=base_domain=.instagram.com; csrftoken=zE7SdCdd8Wf14jzxdExgVmzCXrk9LScL; ds_user_id=8240144112; sessionid=8240144112:6phchbPPsa4SSz:20:AYe8jjamDsa27Ck7PbcdxgRO6J15-akw0nPYaw9j4g; shbid=3313; shbts=1695131722; fbsr_124024574287414=ddDb2DnUUgI5AfeKuuEqVGnLMrVtmxdKzmvlezBx0CI.eyJ1c2VyX2lkIjoiMTAwMDA4NDY8HHHHHHHHHHHHHHH");
                client.DefaultRequestHeaders.Add("x-ig-app-id", token);  // Değişkeni burada kullanıyoruz

                HttpResponseMessage response = await client.GetAsync(link);
                JObject jsonObject;
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    jsonObject = JObject.Parse(content);
                }
                else
                {
                    Console.WriteLine("İstek başarısız oldu. HTTP kodu: " + response.StatusCode);
                    jsonObject = null;
                }
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
    }
}
