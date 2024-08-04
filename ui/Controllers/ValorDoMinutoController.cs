using Dapper;
using Dapper_estacionamento.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Dapper_estacionamento.Controllers;

[Route("/valores")]
public class ValorDoMinutoController : Controller
{
    private readonly IDbConnection _connection;

    public ValorDoMinutoController(IDbConnection connection)
    {
        _connection = connection;
    }

    public IActionResult Index()
    {
        var valores = _connection.Query<ValorDoMinuto>("SELECT * FROM valores");

        return View(valores);
    }
}