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
        string igAppId = ""; //x-ig-app-id inizi buraya girin
        //user id hangi kullanıcının takip ettiklerini görmek isrtiyorsan onlar
        await Console.Out.WriteLineAsync("Ortak takipleri görmek istediğiniz kullanıcı adını buraya girin");
        string target1UserName = Console.ReadLine();
        string target2UserName = Console.ReadLine();
        Console.Clear();
        string cookie = "mid=ZMtaqQALAAFIsrg55_kPJsY-hPgb; ig_did=89F1C663-68C0-417A-834B-B87841E39AF2; ig_nrcb=1; datr=qFrLZIMGM-iWwZy7pkG_ltkN; fbm_124024574287414=base_domain=.instagram.com; shbid=\"3313\\0548240144112\\0541726667722:01f7b7962ae18830a8cd79434bc2654a249ea191151896080501165d678128ca001b723c\"; shbts=\"1695131722\\0548240144112\\0541726667722:01f72b2788590406f206b9357279582402e6fc3ffb5998442d33d029242a58274f00e0b6\"; ds_user_id=8240144112; sessionid=8240144112%3A92tWtdlPrUfRDw%3A7%3AAYdAQ793Grj7SZ_AStSlvkxoWfSWRKCTYhdkcO20ww; csrftoken=XMoipA7se4FGXCMwLChgZnQlSqitaQrr; fbsr_124024574287414=dpRtK3K_83hpCebUNbAEE-qcEhCIjF1mNNsCGxPNHsE.eyJ1c2VyX2lkIjoiMTAwMDA4NDY4MzE3NjM1IiwiY29kZSI6IkFRQzV0Z1JjLWtXM0NwVWJXaDROLTQzTFduUjItdkt6bWdNVTFoRXVJREhXMGtnUExQWmRSZDNxWG1TTWttTkRqZjZpTU1ZZXZtSEVlUlFZN1h6Z01ONUtZUTdyUkg0RTdTaV9nRjJtbkZtMXEyWmpTUTRfOFZHX2doa1BONXBrcktVSFZFaG5RWVVuNEhSRGpRZ0s0V28xLXJGb2p5MGJfUTJiSEd2VnpuRnpjaWxBOVJQdUhKT1JKUHFHdGhTN0MzUXA5UFk1bjJFa2Jja0o5aS1sR1Vlclljbl9ZQ2llcERzS1hQRjRFakd1YXlTRkdXS092MEx6NTVMbXAyeVF6REZzZVh0d2lPNjJHVHRrai0tRWIwMV82ZXpyX2YzcHBlMkNYVlg4cG9tVW5tdklSOExudFlSMnBQcXRMLVlRYnNrOHQyTzZaOE1nLVpyNDVlRjI0blBJIiwib2F1dGhfdG9rZW4iOiJFQUFCd3pMaXhuallCT1pCVllrNFVJWVVpUGtaQlBKZ0hzdFpBcDhYb1BaQUtVWkJYbnFId0NjWkNzbVRtbXZuZUR5dGxqRlUwbHVUeWZtUTZkUmxCVGhQUzg5QmtxSTZNbmRFODdQcnFEcTY3U251Tkx0QzdyUGl6TnNaQktHaFVocFE1V3hCWkJ0UkRaQXFVdEpyRmpWbEUzRGtCWkI3UXNNdVFDclpCaHphUlpDcmp0WkJaQ09XWHoyWkFPbldaQUJ0cjVXdnJNZXNJdTM4WkQiLCJhbGdvcml0aG0iOiJITUFDLVNIQTI1NiIsImlzc3VlZF9hdCI6MTY5NTM3MTU2MX0; fbsr_124024574287414=iik9aTPSeRoyfHEPb2uFJgO66cBqrkVZBpT8S8nu48w.eyJ1c2VyX2lkIjoiMTAwMDA4NDY4MzE3NjM1IiwiY29kZSI6IkFRRHd3QlZFem5uQ29Sc29sR1V6dUdGcGhMWlJpM3laeVg1NG5LSFVhNk9fckhNZXdtNXRoTzJNVjdYOHNfLUQtRHRvdzVhcjNULTdCUVY3bFd6ZUhUVVhMNmtZeFZ2SThGUU5xVFJ1Mkd3Q0tVQlNwaEFpWUJibWE3am5YQURGWktKMDFuNFRJbVByeFN5a2pWa1pSdkxKeU5ybVdLeFlkUU5lVkJkcG5Va3d5bVJqOGJETUY3RktZVVQ4U1ltZ1ZMZWdyVnhQT21fS1Z0NjJuZEtFWV93Q1pFdEpydmotQ3dVM3FHQjZTYVhBUGxHTkdFbGM2RFRYYTRmMl9yRmExd2JoMlVUNGdTd0xoamRJVERmMjN6V1Zwd1FMdHVySG9kUzlWcGFHUkhodFFXUDFVODc2N29jYTNUVGRjb2d4R1ZiRkMwbUdWX25UdzlrdEFiaE5WaUxsIiwib2F1dGhfdG9rZW4iOiJFQUFCd3pMaXhuallCTzQ5T3VhMkJUTTBFVmRYQlpBMjhDRzE0SkZRbEZKdExISVBFVzMzVzBKT1N4VXVXTGpCS0RWczF1bnM0WkFtV2cxZ0JVT3VPZTBRRFZ5TFpBcmxyVDhsVldjTU50WkMwVlpBdTJRU1Q5WEw2ME5yOUxSTVk0dlRXazVqTDJPY3FJVVlmQ3hNUUt4OEtSNTFtUm1QWG5aQm1BMjB6YlpDTFRvd1pBUldWRlR4ZFl6YTdIMDhCWWxtdnBYVVpEIiwiYWxnb3JpdGhtIjoiSE1BQy1TSEEyNTYiLCJpc3N1ZWRfYXQiOjE2OTUzNzIwNjR9; rur=\"FRC\\0548240144112\\0541726908069:01f7965ccfabb70611eca8127c068db46217d5f4397d5da0f6be589426cb3f995bf9a48f\"";
        //kullanıcı id si alma
        string idCallerLink = "https://www.instagram.com/api/v1/users/web_profile_info/?username=" + target1UserName;

        //userid call user follower and following count for target1
        JObject dataInfo = await Functions.AsyncHttpRequest(idCallerLink, igAppId, cookie);

        string target1UserId = dataInfo["data"]["user"]["id"].ToString(); //ilk kullanıcının user id si
        long target1FollowerCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_followed_by"]["count"]).Value; //ilk kullanıcın takipçi sayısı
        long target1FollowingCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_follow"]["count"]).Value; //ilk kullanıcının takp ettiği kişi sayısı

        //userid call user follower and following count for target2
        idCallerLink = "https://www.instagram.com/api/v1/users/web_profile_info/?username=" + target2UserName;
        dataInfo = await Functions.AsyncHttpRequest(idCallerLink, igAppId, cookie);
        string target2UserId = dataInfo["data"]["user"]["id"].ToString(); //ikinci kullanıcının user id si
        long target2FollowerCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_followed_by"]["count"]).Value; // ikinci kullanıcın takipçi sayısı
        long target2FollowingCount = (long)((Newtonsoft.Json.Linq.JValue)dataInfo["data"]["user"]["edge_follow"]["count"]).Value; //ilk kullanıcının takp ettiği kişi sayısı

        //user 1 in takip ettiklerini alalım
        string followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target1UserId + "/following/?count=200&max_id=";
        string data;
        cookie = "mid=ZMtaqQALAAFIsrg55_kPJsY-hPgb; ig_did=89F1C663-68C0-417A-834B-B87841E39AF2; ig_nrcb=1; datr=qFrLZIMGM-iWwZy7pkG_ltkN; fbm_124024574287414=base_domain=.instagram.com; shbid=\"3313\\0548240144112\\0541726667722:01f7b7962ae18830a8cd79434bc2654a249ea191151896080501165d678128ca001b723c\"; shbts=\"1695131722\\0548240144112\\0541726667722:01f72b2788590406f206b9357279582402e6fc3ffb5998442d33d029242a58274f00e0b6\"; ds_user_id=8240144112; sessionid=8240144112%3A92tWtdlPrUfRDw%3A7%3AAYdAQ793Grj7SZ_AStSlvkxoWfSWRKCTYhdkcO20ww; csrftoken=XMoipA7se4FGXCMwLChgZnQlSqitaQrr; fbsr_124024574287414=iik9aTPSeRoyfHEPb2uFJgO66cBqrkVZBpT8S8nu48w.eyJ1c2VyX2lkIjoiMTAwMDA4NDY4MzE3NjM1IiwiY29kZSI6IkFRRHd3QlZFem5uQ29Sc29sR1V6dUdGcGhMWlJpM3laeVg1NG5LSFVhNk9fckhNZXdtNXRoTzJNVjdYOHNfLUQtRHRvdzVhcjNULTdCUVY3bFd6ZUhUVVhMNmtZeFZ2SThGUU5xVFJ1Mkd3Q0tVQlNwaEFpWUJibWE3am5YQURGWktKMDFuNFRJbVByeFN5a2pWa1pSdkxKeU5ybVdLeFlkUU5lVkJkcG5Va3d5bVJqOGJETUY3RktZVVQ4U1ltZ1ZMZWdyVnhQT21fS1Z0NjJuZEtFWV93Q1pFdEpydmotQ3dVM3FHQjZTYVhBUGxHTkdFbGM2RFRYYTRmMl9yRmExd2JoMlVUNGdTd0xoamRJVERmMjN6V1Zwd1FMdHVySG9kUzlWcGFHUkhodFFXUDFVODc2N29jYTNUVGRjb2d4R1ZiRkMwbUdWX25UdzlrdEFiaE5WaUxsIiwib2F1dGhfdG9rZW4iOiJFQUFCd3pMaXhuallCTzQ5T3VhMkJUTTBFVmRYQlpBMjhDRzE0SkZRbEZKdExISVBFVzMzVzBKT1N4VXVXTGpCS0RWczF1bnM0WkFtV2cxZ0JVT3VPZTBRRFZ5TFpBcmxyVDhsVldjTU50WkMwVlpBdTJRU1Q5WEw2ME5yOUxSTVk0dlRXazVqTDJPY3FJVVlmQ3hNUUt4OEtSNTFtUm1QWG5aQm1BMjB6YlpDTFRvd1pBUldWRlR4ZFl6YTdIMDhCWWxtdnBYVVpEIiwiYWxnb3JpdGhtIjoiSE1BQy1TSEEyNTYiLCJpc3N1ZWRfYXQiOjE2OTUzNzIwNjR9; fbsr_124024574287414=iik9aTPSeRoyfHEPb2uFJgO66cBqrkVZBpT8S8nu48w.eyJ1c2VyX2lkIjoiMTAwMDA4NDY4MzE3NjM1IiwiY29kZSI6IkFRRHd3QlZFem5uQ29Sc29sR1V6dUdGcGhMWlJpM3laeVg1NG5LSFVhNk9fckhNZXdtNXRoTzJNVjdYOHNfLUQtRHRvdzVhcjNULTdCUVY3bFd6ZUhUVVhMNmtZeFZ2SThGUU5xVFJ1Mkd3Q0tVQlNwaEFpWUJibWE3am5YQURGWktKMDFuNFRJbVByeFN5a2pWa1pSdkxKeU5ybVdLeFlkUU5lVkJkcG5Va3d5bVJqOGJETUY3RktZVVQ4U1ltZ1ZMZWdyVnhQT21fS1Z0NjJuZEtFWV93Q1pFdEpydmotQ3dVM3FHQjZTYVhBUGxHTkdFbGM2RFRYYTRmMl9yRmExd2JoMlVUNGdTd0xoamRJVERmMjN6V1Zwd1FMdHVySG9kUzlWcGFHUkhodFFXUDFVODc2N29jYTNUVGRjb2d4R1ZiRkMwbUdWX25UdzlrdEFiaE5WaUxsIiwib2F1dGhfdG9rZW4iOiJFQUFCd3pMaXhuallCTzQ5T3VhMkJUTTBFVmRYQlpBMjhDRzE0SkZRbEZKdExISVBFVzMzVzBKT1N4VXVXTGpCS0RWczF1bnM0WkFtV2cxZ0JVT3VPZTBRRFZ5TFpBcmxyVDhsVldjTU50WkMwVlpBdTJRU1Q5WEw2ME5yOUxSTVk0dlRXazVqTDJPY3FJVVlmQ3hNUUt4OEtSNTFtUm1QWG5aQm1BMjB6YlpDTFRvd1pBUldWRlR4ZFl6YTdIMDhCWWxtdnBYVVpEIiwiYWxnb3JpdGhtIjoiSE1BQy1TSEEyNTYiLCJpc3N1ZWRfYXQiOjE2OTUzNzIwNjR9; rur=\"FRC\\0548240144112\\0541726908078:01f7e6939b218280eeb53185363910c24ca435500140247f6a852ec703d812c0265a7449\"";
        dataInfo = await Functions.RequestCaller(followingCallerLink, igAppId, cookie, target1FollowingCount);
        data = dataInfo.ToString();
        InstagramResponse response1 = JsonConvert.DeserializeObject<InstagramResponse>(data);
        //user 2 nin takiip ettiklerini alalım
        followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target2UserId + "/following/?count=200&max_id=";
        dataInfo = await Functions.RequestCaller(followingCallerLink, igAppId, cookie, target2FollowingCount);
        data = dataInfo.ToString();
        InstagramResponse response2 = JsonConvert.DeserializeObject<InstagramResponse>(data);

        //user 1 in takipçilerini alalım
        idCallerLink = "https://www.instagram.com/api/v1/friendships/" + target1UserId + "/followers/?count=200&max_id=";
        dataInfo = await Functions.RequestCaller(followingCallerLink, igAppId, cookie, target1FollowerCount);
        data = dataInfo.ToString();
        InstagramResponse response3 = JsonConvert.DeserializeObject<InstagramResponse>(data);


        //user 2 nin takipçilerini alalım
        followingCallerLink = "https://www.instagram.com/api/v1/friendships/" + target2UserId + "/followers/?count=200&max_id=";
        dataInfo = await Functions.RequestCaller(followingCallerLink, igAppId, cookie, target2FollowerCount);
        data = dataInfo.ToString();
        InstagramResponse response4 = JsonConvert.DeserializeObject<InstagramResponse>(data);


        //user 1 ve user 2 nin ortak olan takip ettiklerini alalım
        List<string> commonFollowing = Functions.CompareFollows(response1, response2);
        await Console.Out.WriteLineAsync("Ortak tekipçiler -------------------");
        foreach (var followings in commonFollowing)
        {
            await Console.Out.WriteLineAsync(followings);
        }
        await Console.Out.WriteLineAsync("-------------------------------------");

        //user 1 ve user 2 nin ortal olan takip ettiklerini alalım
        List<string> commonFollowers = Functions.CompareFollows(response3, response4);
        await Console.Out.WriteLineAsync("Ortak tekip edilenler ---------------");
        foreach (var followings in commonFollowing)
        {
            await Console.Out.WriteLineAsync(followings);
        }
        await Console.Out.WriteLineAsync("-------------------------------------");

    }
}
