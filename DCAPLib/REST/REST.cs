using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Runtime.CompilerServices;

namespace DCAPI.REST
{
    using FormData = IEnumerable<(string Key, string Value)>;
    
    //디시인사이드와의 통신에 사용되는 REST입니다.
    public class REST {
        protected const string UserAgent = "dcinside.app";
        protected const string Referer = "http://www.dcinside.com";
        
        private static HttpClient client = new HttpClient();
        
        protected virtual HttpResponseMessage Send(HttpRequestMessage req)
            => client.SendAsync(req).Result;
        
        public Json Get(string url) {
            using var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.UserAgent.ParseAdd(UserAgent);
            req.Headers.Referrer = new Uri(Referer);
            using var res = Send(req);
            if(res.StatusCode == HttpStatusCode.Found)
                return Get(res.Headers.Location.ToString());
            return new Json(res.Content.ReadAsStreamAsync().Result);
        }
        
        public Json Get(string url, FormData form) {
            const string URL = "http://app.dcinside.com/api/redirect.php";
            return Get($"{URL}?hash={Base64($"{url}?{GetQuery(form)}")}");
        }
        
        public Json Post(string url, HttpContent content) {
            using var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.UserAgent.ParseAdd(UserAgent);
            req.Headers.Referrer = new Uri(Referer);
            req.Content = content;
            using var res = Send(req);
            //if(res.StatusCode == HttpStatusCode.Found)
            //    return Post(res.Headers.Location.ToString(), content);
            return new Json(res.Content.ReadAsStreamAsync().Result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Json Post(string url, FormData form)
            => Post(url, new StringContent(GetQuery(form),
                Encoding.UTF8, "application/x-www-form-urlencoded"));
            //=> Post(url, new FormUrlEncodedContent(
            //    form.Select((i) => new KeyValuePair<string, string>(i.Key, i.Value))));
        
        public Json PostXHR(string url, FormData form) {
            using var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.Referrer = new Uri(Referer);
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Content = new StringContent(GetQuery(form),
                Encoding.UTF8, "application/x-www-form-urlencoded");
            using var res = Send(req);
            if(res.Content.Headers.ContentLength == 0)
                throw new DCException("Invalid Operation");
            return new Json(res.Content.ReadAsStreamAsync().Result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Base64(string s)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string GetQuery(FormData form) {
            var sb = new StringBuilder();
            foreach(var i in form)
                if(i.Value != null)
                    sb.Append($"{i.Key}={Uri.EscapeDataString(i.Value)}&");
            return sb.ToString(0, sb.Length - 1);
        }
    }
}