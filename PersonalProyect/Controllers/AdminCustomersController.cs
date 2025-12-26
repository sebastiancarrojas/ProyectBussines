using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Core;
using PersonalProyect.DTOs.Customers;

namespace PersonalProyect.Controllers
{
    public class AdminCustomersController : Controller
    {
        // Inyectar dependecias
        private readonly HttpClient _http;

        public AdminCustomersController(IHttpClientFactory httpfactory)
        {
            _http = httpfactory.CreateClient("ApiClient");
        }

        // Get List
        public async Task<IActionResult> Index()
        {
            var response = await _http.GetFromJsonAsync<Response<List<CreateCustomerDTO>>>("api/Customer");
            return View(response?.Result);
        }

        // Render Create

        public IActionResult Create()
        {
            return View(); 
        }

        // Edit
        public async Task<IActionResult> Edit(Guid id, CreateCustomerDTO dto)
        {
            var result = await _http.PutAsJsonAsync($"api/Customer/{id}", dto);
            if (!result.IsSuccessStatusCode)
                return View(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _http.GetFromJsonAsync<Response<CreateCustomerDTO>>($"api/Customer/{id}");
            if (response == null || response.Result == null)
                return NotFound();

            return View(response.Result);
        }


        // Delete
        public async Task<IActionResult> Delete(Guid id)
        {
            await _http.DeleteAsync($"api/Customer/{id}");
            return RedirectToAction("Index");
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _http.PostAsJsonAsync("api/Customer", dto);

            if (!result.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error al crear el cliente");
                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}
