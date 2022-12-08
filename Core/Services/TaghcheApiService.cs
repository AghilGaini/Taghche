using Domain.ApiModel;
using Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Services
{
    public static class TaghcheApiService
    {
        public static string BaseUrlAddress { get; set; } = "https://get.taaghche.com/v2/book/";

        public static async Task<ApiResponseModel> GetCall(string query)
        {
            var res = new ApiResponseModel()
            {
                ResultCode = 0,
                IsSuccess = true
            };

            try
            {
                var client = new RestClient($"{BaseUrlAddress}{query}");

                var request = new RestRequest();
                var response = await client.ExecuteAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    res.ResultCode = (int)response.StatusCode;
                    res.IsSuccess = false;
                    res.ErrorMessage = $"Status : {response.StatusCode}";
                }
                else
                {
                    res.Data = response.Content;
                }

            }
            catch (Exception ex)
            {
                res.ResultCode = 500;
                res.IsSuccess = false;
                res.ErrorMessage = ex.Message;
            }

            return res;
        }

    }
}
