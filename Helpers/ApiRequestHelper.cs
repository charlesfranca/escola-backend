using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaDeVoce.Backend
{
    public class ApiRequestHelper
    {
        public const string CONTENT_TYPE_JSON = "application/json";
        public const string CONTENT_TYPE_URLENCODED = "application/x-www-form-urlencoded";
        public async static Task<T> Get<T>(string url, Dictionary<string, string> urlParameters = null){
            string saida = "";
            T response;
            
            using (var client = new HttpClient())
            {
                string _urlParameters = "";
                if(urlParameters != null){
                    StringBuilder strParam = new StringBuilder();
                    foreach(string p in urlParameters.Keys){
                        strParam.Append(string.Format("{0}={1}", p, urlParameters[p]));
                    }
                    _urlParameters = "?" + System.Net.WebUtility.UrlEncode(strParam.ToString());
                }

                saida = await client.GetStringAsync(url + _urlParameters);
                Console.Write(saida);
                response = JsonConvert.DeserializeObject<T>(saida);
            }

            return response;
        }

        public async static Task<T> Post<T>(string url, string jsonParameters = "", Dictionary<string, string> urlParameters = null, string contentType = "application/json"){
            return await postPutRequest<T>(url, HttpMethod.Post, jsonParameters, urlParameters, contentType);
        }

        public async static Task<T> Put<T>(string url, string jsonParameters = "", Dictionary<string, string> urlParameters = null, string contentType = "application/json"){
            return await postPutRequest<T>(url, HttpMethod.Put, jsonParameters, urlParameters, contentType);
        }

        public async static Task<T> postPutRequest<T>(string url, HttpMethod method, string jsonParameters = "", Dictionary<string, string> urlParameters = null, string contentType = "application/json"){
            T response; 

            using (var client = new HttpClient())
            {
                string _urlParameters = "";
                if(urlParameters != null){
                    StringBuilder strParam = new StringBuilder();
                    foreach(string p in urlParameters.Keys){
                        strParam.Append(string.Format("{0}={1}", p, urlParameters[p]));
                    }
                    _urlParameters = "?" + System.Net.WebUtility.UrlEncode(strParam.ToString());
                }
                
                HttpRequestMessage req = new HttpRequestMessage();
                req.Method = method;
                req.RequestUri = new Uri(url + _urlParameters);
                req.Content = new StringContent(jsonParameters, Encoding.UTF8, contentType);

                var resp = await client.SendAsync(req);
                response = await resp.Content.ReadAsAsync<T>();
            }

            return response;
        }

        public async static Task<T> postPutEncodedRequest<T>(string url, string username, string password){
            T response; 

            using (var client = new HttpClient())
            {   
                HttpRequestMessage req = new HttpRequestMessage();
                req.Method = HttpMethod.Post;
                req.RequestUri = new Uri(url);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                });
                req.Content = content;

                var resp = await client.SendAsync(req);
                response = await resp.Content.ReadAsAsync<T>();
            }

            return response;
        }
    }
}
