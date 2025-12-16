using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Core.Pagination;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.DTOs.Products;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Services.Implementations;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PersonalProyect.ViewModels.Products;


public class AdminProductsController : Controller
{
    // --------------------------------------
    // -- Inyectar dependencias necesarias --
    // --------------------------------------

    private readonly HttpClient _http;
    private readonly DataContext _context;
    private readonly ICategoryService _categoryService;
    private readonly IBrandService _brandService;

    public AdminProductsController(IHttpClientFactory httpFactory, DataContext context, ICategoryService categoryService, IBrandService brandService)
    {
        _http = httpFactory.CreateClient("ApiClient");
        _context = context;
        _categoryService = categoryService;
        _brandService = brandService;
    }

    // -----------------------------
    // -- Renderizar vista Create --
    // -----------------------------

    public async Task<IActionResult> Create()
    {
        await LoadSelects();
        return base.View(new ProductCreateDTO());
    }

    // -----------------------
    // -- Crear un producto --
    // -----------------------

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDTO dto)
    {
        if (!ModelState.IsValid)
        {
            await LoadSelects();
            return View(dto);
        }

        var result = await _http.PostAsJsonAsync("api/Product", dto);
        if (!result.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Error al crear el producto");
            await LoadSelects(); 
            return View(dto);
        }

        return RedirectToAction("Index");
    }



    // ---------------------------------------------------------------------------
    // -- Método auxiliar para recargar selectores si hay errores de validación --
    // ---------------------------------------------------------------------------
    private async Task LoadSelects()
    {
        var categories = await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
        var brands = await _context.Brands.OrderBy(b => b.BrandName).ToListAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
        ViewBag.Brands = new SelectList(brands, "Id", "BrandName");
    }

    // ------------------------------------------------------------
    // --  Renderizar vista de lista de productos con paginación --
    // ------------------------------------------------------------


    public async Task<IActionResult> Index()
    {
        var viewModel = new ProductsIndexViewModel
        {
            Categories = (await _categoryService.GetCompleteListAsync())?.Result ?? new List<CategoryDTO>(),
            Brands = (await _brandService.GetCompleteListAsync())?.Result ?? new List<BrandDTO>(),

            Products = new PaginationResponse<ProductListDTO>() // vacía inicialmente
        };

        ViewBag.ShowCreateButton = true;
        return View(viewModel);
    }




// ---------------------------
// -- Renderizar vista Edit --
// ---------------------------

public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _http.GetFromJsonAsync<Response<ProductCreateDTO>>($"api/Product/{id}");
        return View(response?.Result);
    }

    // ---------------------
    // -- Editar producto --
    // ---------------------

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, ProductCreateDTO dto)
    {
        var result = await _http.PutAsJsonAsync($"api/Product/{id}", dto);

        if (!result.IsSuccessStatusCode)
            return View(dto);

        return RedirectToAction("Index");
    }

    // -----------------------
    // -- Eliminar producto --
    // -----------------------
    public async Task<IActionResult> Delete(Guid id)
    {
        await _http.DeleteAsync($"api/Product/{id}");
        return RedirectToAction("Index");
    }
}

    