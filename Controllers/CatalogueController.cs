// using KodaStore.Api.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace KodaStore.Api.Controllers;

// public sealed class CatalogueController(IProductService service) : Controller
// {
//     private readonly IProductService _service = service;

//     [HttpGet("/catalogue")]
//     public IActionResult Index()
//     {
//         var products = _service.GetAll();
//         return View(products);
//     }
// }