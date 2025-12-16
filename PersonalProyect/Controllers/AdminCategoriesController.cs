using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using System.Net.Http;
using PersonalProyect.Core;
using static System.Net.WebRequestMethods;

namespace PersonalProyect.Controllers
{
    public class AdminCategoriesController : Controller
    {
        // ---------------------------
        // -- Inyectar dependencias --
        // ---------------------------

        private readonly HttpClient _http;

        public AdminCategoriesController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("ApiClient");
        }


        // --------------------------
        // -- Crear Categoria Ajax --
        // --------------------------
        [HttpPost]
        public async Task<IActionResult> CreateFromAjax([FromBody] CategoryCreateDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/Category", dto);

            if (!response.IsSuccessStatusCode)
                return BadRequest("No se pudo crear la categoría");

            var apiResponse = await response.Content
                .ReadFromJsonAsync<Response<CategoryDTO>>();

            if (apiResponse == null || !apiResponse.IsSuccess || apiResponse.Result == null)
                return BadRequest(apiResponse?.Message ?? "Error al crear la categoría");

            return Json(new
            {
                id = apiResponse.Result.Id,
                categoryName = apiResponse.Result.CategoryName
            });
        }
    }
}

