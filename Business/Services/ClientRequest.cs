using Business.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Business.Services
{
    public class ClientRequest
    {
        static private readonly HttpClient httpClient;
        static ClientRequest()
        {
            httpClient = new HttpClient();
        }

        public async Task<RequestResult<T>> GetEntity<T>(string url)
        {
            return await Get<T>(url);
        }
        public async Task<RequestResult<T>> PostEntity<T>(string url, object data)
        {
            return await Post<T>(url, data);
        }
        public async Task<RequestResult<T>> Get<T>(string url)
        {
            var requestResult = new RequestResult<T>();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                httpClient.DefaultRequestHeaders.Add("User-Agent", "C# app");
                var prodResp = await httpClient.GetAsync(url);
                if (!prodResp.IsSuccessStatusCode)
                {
                    throw new System.Exception("request failed.");
                }
                var result = await prodResp.Content.ReadAsStringAsync();
                requestResult.Result = JsonConvert.DeserializeObject<T>(result);
            }
            catch (Exception)
            {
                requestResult.HasError = true;
            }
            return requestResult;
        }

        public async Task<RequestResult<T>> Post<T>(string url, object jsonObject)
        {
            var requestResult = new RequestResult<T>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        requestResult.Result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return requestResult;
        }
    }
}
