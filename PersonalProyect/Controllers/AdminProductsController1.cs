using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class AdminProductsController : Controller
{
    // Inyectar dependencia IHttpClientFactory
    private readonly HttpClient _http;
    private readonly DataContext _context;

    public AdminProductsController(IHttpClientFactory httpFactory, DataContext context)
    {
        _http = httpFactory.CreateClient("ApiClient");
        _context = context;
    }

    // Pide a la API todos los productos y los muestra en la vista Index
    public async Task<IActionResult> Index()
    {
        var response = await _http.GetFromJsonAsync<Response<List<ProductDTO>>>("api/Product");
        ViewBag.ShowCreateButton = true;
        return View(response?.Result);
    }

    // Renderiza la vista CREATE
    public IActionResult Create()
    {
        return View();
    }

    // Recibe los datos del formulario, los envía a la API (ProductController)
    // y según la respuesta redirige o vuelve a mostrar la vista.
    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO dto)
    {
        using var content = new MultipartFormDataContent();

        content.Add(new StringContent(dto.Name ?? ""), "Name");
        content.Add(new StringContent(dto.Description ?? ""), "Description");
        content.Add(new StringContent(dto.Price.ToString()), "Price");
        content.Add(new StringContent(dto.Code ?? ""), "Code");
        content.Add(new StringContent(dto.Status ?? ""), "Status");

        if (dto.ImageFile != null)
        {
            var streamContent = new StreamContent(dto.ImageFile.OpenReadStream());
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(dto.ImageFile.ContentType);
            content.Add(streamContent, "ImageFile", dto.ImageFile.FileName);
        }

        var response = await _http.PostAsync("api/Product", content);

        return RedirectToAction("Index", "AdminProducts");

    }


    // Busca el producto en la API usando su id y muestra sus datos en la pantalla de edición
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _http.GetFromJsonAsync<Response<ProductDTO>>($"api/Product/{id}");
        return View(response?.Result);
    }

    //Toma los datos del formulario de edición, los envía a la API para actualizar el producto
    //Si todo sale bien, vuelve a la lista de productos
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, ProductDTO dto)
    {
        var result = await _http.PutAsJsonAsync($"api/Product/{id}", dto);

        if (!result.IsSuccessStatusCode)
            return View(dto);

        return RedirectToAction("Index");
    }

    // Envía a la API el id del producto que se quiere eliminar y, cuando termina, vuelve a la lista
    public async Task<IActionResult> Delete(Guid id)
    {
        await _http.DeleteAsync($"api/Product/{id}");
        return RedirectToAction("Index");
    }
}

    