using App4.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App4.Data
{
    public class RestService
    {
        HttpClient client;
      //  string grant_type = "password";
        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 250000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
       
        }

        public async Task<Token> Login(User user)
        {
            var postData = new List<KeyValuePair<string, string>>();
          //  postData.Add(new KeyValuePair<string, string>("grant_type", grant_type));
            postData.Add(new KeyValuePair<string, string>("email", user.email));
            postData.Add(new KeyValuePair<string, string>("password", user.passWord));
            var content = new FormUrlEncodedContent(postData);
            var weburl = constants.Loginurl;
            var response = await PostResponseLogin<Token>(weburl,content);
            return response;
        }

        public async Task<T> PostResponseLogin<T>(string weburl,FormUrlEncodedContent content) where T:class
        {
            
            var response = await client.PostAsync(weburl, content);
            Console.WriteLine(response);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(jsonResult);
            var token = JsonConvert.DeserializeObject<T>(jsonResult);
            Console.WriteLine(token);
            return token;
        }

        public async Task<T> PostResponse<T>(string weburl,string jsonString) where T:class
        {
           
            var token = App.TokenDatabase.GetToken();
            string ContentType = "application/json";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
            try
            {
                var result = await client.PostAsync(weburl, new StringContent(jsonString, Encoding.UTF8, ContentType));
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResult = result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResp = JsonConvert.DeserializeObject<T>(jsonResult);
                        return ContentResp;
                    }
                    catch { return null; }
                }
            }catch { return null; }
            return null;
        }

        public async Task<Token> GetResponse<Token>(string weburl)
        {
            
            var token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.access_token);
            var response = await client.GetAsync(weburl);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var contentResp = JsonConvert.DeserializeObject<Token>(jsonResult);
            return contentResp;


        }
    }
}
