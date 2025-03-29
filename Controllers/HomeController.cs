using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Practica1_JeanEstrada.Models;

namespace Practica1_JeanEstrada.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ConvertirMoneda(decimal monto)
    {
        // Tasa de cambio de Real Brasileño (BRL) a Sol Peruano (PEN)
        decimal tasaCambioBRLtoPEN = 0.634m;

        // Realizar la conversión
        decimal montoConvertido = monto * tasaCambioBRLtoPEN;

        // Enviar resultado a la vista
        ViewBag.Resultado = $"{monto} BRL equivale a {montoConvertido:F2} PEN";
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
