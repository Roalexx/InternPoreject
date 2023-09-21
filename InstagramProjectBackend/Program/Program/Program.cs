using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BackendApp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {   //iqAppId kullanıcı tokenidir yani kimi görmek istediğin değil kimin görmek istediğidir
        string igAppId = "1754362624997642";
        //user id hangi kullanıcının takip ettiklerini görmek isrtiyorsan onlar
        string target1UserName = "leomessi";
        string target2UserName = "cristiano";
        //kullanıcı id si alma
        string idCallerLink = "https://www.instagram.com/api/v1/users/web_profile_info/?username=" + target1UserName;

        //userid call user follower and following count for target1
        JObject dataInfo = await Functions.AsyncHttpRequest(idCallerLink, igAppId);
        string target1UserId = dataInfo["data"]["user"]["id"].ToString(); //ilk kullanıcının user id si
        long target1FollowerCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_followed_by"]["count"]).Value; //ilk kullanıcın takipçi sayısı
        long target1FollowingCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_follow"]["count"]).Value; //ilk kullanıcının takp ettiği kişi sayısı

        //userid call user follower and following count for target2
        idCallerLink = "https://www.instagram.com/api/v1/users/web_profile_info/?username=" + target2UserName;
        dataInfo = await Functions.AsyncHttpRequest(idCallerLink, igAppId);
        string target2UserId = dataInfo["data"]["user"]["id"].ToString(); //ikinci kullanıcının user id si
        long target2FollowerCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_followed_by"]["count"]).Value; // ikinci kullanıcın takipçi sayısı
        long target2FollowingCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_follow"]["count"]).Value; //ilk kullanıcının takp ettiği kişi sayısı

        //user 1 in takip ettiklerini alalım
        long max_id = 0;
        bool whileController = true;
        string followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target1UserId + "/following/?count=200&max_id=" + max_id;
        string data1;
        InstagramResponse response1;
        //response1 e onceki verileri ekleyemiyouz sadece son veriler yazılıyo onu züzelt
        while (whileController)
        {
            if (target1FollowerCount > max_id)
            {
                followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target1UserId + "/following/?count=200&max_id=" + max_id;
                dataInfo = await Functions.AsyncHttpRequest(followingCallerLink, igAppId);
                data1 = dataInfo.ToString();
                response1 = JsonConvert.DeserializeObject<InstagramResponse>(data1);
                max_id += 200;
            }
            else
            {
                whileController = false;
            }
        }

        //user 2 nin takiip ettiklerini alalım
        max_id = 0;
        whileController = true;     
        followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target2UserId + "/following/?count=200&max_id="+max_id;
        while (whileController)
        {
            if (target1FollowerCount > max_id)
            {
                followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target2UserId + "/following/?count=200&max_id=" + max_id;
                dataInfo = await Functions.AsyncHttpRequest(followingCallerLink, igAppId);
                max_id += 200;
            }
            else
            {
                whileController = false;
            }
        }
        string data2 = dataInfo.ToString();
        
        InstagramResponse response2 = JsonConvert.DeserializeObject<InstagramResponse>(data2);

        //user 1 ve user 2 nin ortak olan takip ettiklerini alalım
        //List<string> commonFollowing = Functions.CompareFollows(response1, response2);
        //foreach (var followings in commonFollowing)
        //{
        //    await Console.Out.WriteLineAsync(followings);
        //}
    }
}
