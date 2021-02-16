namespace NBA_data.Service
{
    using NBA_data.Common.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Turn on your internet please!",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
            };
        }
        public async Task<Response> GetList<T>(string urlBase, string action)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Add("x-rapidapi-key", Application.Current.Resources["x-rapidapi-key"].ToString());
                client.DefaultRequestHeaders.Add("x-rapidapi-host", Application.Current.Resources["x-rapidapi-host"].ToString());
                client.DefaultRequestHeaders.Add("useQueryString", Application.Current.Resources["useQueryString"].ToString());
                var response = await client.GetAsync(action);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var obj = JsonConvert.DeserializeObject<ApiResponse<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetImage(string urlBase, string action)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Add("x-rapidapi-key", Application.Current.Resources["x-rapidapi-key"].ToString());
                client.DefaultRequestHeaders.Add("x-rapidapi-host", Application.Current.Resources["x-rapidapi-host2"].ToString());
                client.DefaultRequestHeaders.Add("useQueryString", Application.Current.Resources["useQueryString"].ToString());
                var response = await client.GetAsync(action);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var obj = JsonConvert.DeserializeObject<Common.Models.Image>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
