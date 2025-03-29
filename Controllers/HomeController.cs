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
    public IActionResult ConvertirMoneda(decimal monto, string monedaOrigen, string monedaDestino)
    {
        // Tasas de cambio ajustadas para conversión directa de BRL a PEN
        var tasasCambio = new Dictionary<string, decimal>
        {
            { "BRL", 1.00m },  // Real Brasileño como base
            { "PEN", 0.74m }   // 1 BRL = 0.74 PEN (ejemplo, actualizar con tasa real)
        };

        // Validar monedas
        if (!tasasCambio.ContainsKey(monedaOrigen) || !tasasCambio.ContainsKey(monedaDestino))
        {
            ViewBag.Resultado = "Moneda no válida.";
            return View("Index");
        }

        // Realizar conversión directa
        decimal montoConvertido = monto * (tasasCambio[monedaDestino] / tasasCambio[monedaOrigen]);

        // Redirigir a la vista de boleta
        return RedirectToAction("Boleta", new { montoConvertido, monedaDestino });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Boleta(decimal montoConvertido, string monedaDestino)
    {
        ViewBag.MontoConvertido = montoConvertido;
        ViewBag.MonedaDestino = monedaDestino;
        return View();
    }

    [HttpPost]
    public IActionResult GenerarBoleta(string nombre, string dni, string email, decimal montoConvertido, string monedaDestino)
    {
        ViewBag.Nombre = nombre;
        ViewBag.DNI = dni;
        ViewBag.Email = email;
        ViewBag.MontoConvertido = montoConvertido;
        ViewBag.MonedaDestino = monedaDestino;

        return View("BoletaGenerada");
    }
}
