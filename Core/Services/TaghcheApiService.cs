using Domain.ApiModel;
using Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                var responseContent = await client.ExecuteAsync(request);

                var deserializeResponse = JsonSerializer.Deserialize<TaghcheApiResponseBookModel>
                    (responseContent.Content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (!responseContent.IsSuccessStatusCode)
                {
                    res.ResultCode = (int)responseContent.StatusCode;
                    res.IsSuccess = false;
                    res.ErrorMessage = $"Status : {responseContent.StatusCode}";
                }
                else
                {
                    res.Data = deserializeResponse;
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
