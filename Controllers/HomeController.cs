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
        // Diccionario con tasas de cambio simuladas (1 unidad de cada moneda en USD)
        var tasasCambio = new Dictionary<string, decimal>
        {
            { "USD", 1.0m },   // Dólar estadounidense
            { "PEN", 0.27m },  // Sol peruano
            { "BRL", 0.20m }   // Real brasileño
        };

        // Verificar si las monedas existen en el diccionario
        if (!tasasCambio.ContainsKey(monedaOrigen) || !tasasCambio.ContainsKey(monedaDestino))
        {
            ViewBag.Resultado = "Moneda no válida.";
            return View("Index");
        }

        // Convertir a USD y luego a la moneda destino
        decimal montoEnUsd = monto / tasasCambio[monedaOrigen];
        decimal montoConvertido = montoEnUsd * tasasCambio[monedaDestino];

        // Enviar resultado a la vista
        ViewBag.Resultado = $"{monto} {monedaOrigen} equivale a {montoConvertido:F2} {monedaDestino}";
        return View("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
