using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using static System.Net.WebRequestMethods;
using PersonalProyect.Core;

namespace PersonalProyect.Controllers
{
    public class AdminBrandsController : Controller
    {
        // ---------------------------
        // -- Inyectar dependencias --
        // ---------------------------

        private readonly HttpClient _http;

        public AdminBrandsController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("ApiClient");
        }


        // --------------------------
        // -- Crear Categoria Ajax --
        // --------------------------
        [HttpPost]
        public async Task<IActionResult> CreateFromAjax([FromBody] BrandCreateDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/Brand", dto);

            if (!response.IsSuccessStatusCode)
                return BadRequest("No se pudo crear la marca");

            var apiResponse = await response.Content
                .ReadFromJsonAsync<Response<BrandDTO>>();

            if (apiResponse == null || !apiResponse.IsSuccess || apiResponse.Result == null)
                return BadRequest(apiResponse?.Message ?? "Error al crear la marca");

            return Json(new
            {
                id = apiResponse.Result.Id,
                brandName = apiResponse.Result.BrandName
            });
        }

    }
}
