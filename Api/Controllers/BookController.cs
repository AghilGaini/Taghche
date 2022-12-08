using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Api.Startup;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Entities;
using Core.Services;
using System.Net;
using Domain.ApiModel;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookDomain _level1Cache;
        private readonly IBookDomain _level2Cache;

        static HttpClient client = new HttpClient();

        public BookController(ServiceResolver serviceAccessor)
        {
            _level1Cache = serviceAccessor("Level1");
            _level2Cache = serviceAccessor("Level2");
        }

        [HttpGet]
        public async Task<ApiResponseModel> Get(long id)
        {
            var res = new ApiResponseModel()
            {
                IsSuccess = true,
                ResultCode = 0
            };

            try
            {
                var book = await _level1Cache.GetByIdAsync(id);

                if (book == null)
                {
                    book = await _level2Cache.GetByIdAsync(id);
                    if (book != null)
                    {
                        await _level1Cache.AddAsync(book);
                    }
                    else
                    {
                        var apiRes = await TaghcheApiService.GetCall(id.ToString());
                        if (apiRes.IsSuccess)
                        {
                            book = new BookDomain()
                            {
                                Id = id,
                                Description = apiRes.Data
                            };

                            await _level1Cache.AddAsync(book);
                            await _level2Cache.AddAsync(book);

                        }
                        else
                        {
                            throw new Exception(apiRes.ErrorMessage);
                        }
                    }
                }

                res.Data = book;
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.ResultCode = 500;
                res.ErrorMessage = ex.Message;
                return res;
            }

            return res;
        }

    }
}
