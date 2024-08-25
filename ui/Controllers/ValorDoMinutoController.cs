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

        // Executando Procedure, forma simples
        //var parameters = new DynamicParameters();
        //parameters.Add("@idCliente", 1);
        //var tickets = _connection.Query<TicketComCliente>("ObterTicketsPorCliente", parameters, commandType: CommandType.StoredProcedure);


        // Executando Procedure, forma completa
        //var parameters = new DynamicParameters();
        //parameters.Add("@idCliente", 1);
        //var tickets = _connection.Query<Ticket, string, TicketComCliente>("ObterTicketsPorCliente", (ticket, nomeCliente) =>
        //{
        //    return new TicketComCliente { Ticket = ticket, NomeCliente = nomeCliente };
        //},
        //parameters, splitOn: "NomeCliente", commandType: CommandType.StoredProcedure);


        // Executando View, forma simples
        //var sqlView = "SELECT * FROM VistaTicketComClientes";
        //var ticketsComClientes = _connection.Query<TicketComCliente>(sqlView);


        // Executando View, forma completa
        //var ticketsComClientes = _connection.Query<Ticket, string, TicketComCliente>("SELECT * FROM VistaTicketComClientes",
        //(ticket, nomeCliente) =>
        //{
        //    return new TicketComCliente { Ticket = ticket, NomeCliente = nomeCliente };
        //}, splitOn: "NomeCliente");

        return View(valores);
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("criar")]
    public IActionResult Criar([FromForm] ValorDoMinuto valorDoMinuto)
    {
        string sql = "INSERT INTO valores (Minutos, Valor) VALUES (@Minutos, @Valor)";
        _connection.Execute(sql, new { Minutos = valorDoMinuto.Minutos, Valor = valorDoMinuto.Valor });

        return Redirect("/valores");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        string sql = "SELECT * FROM valores WHERE Id = @Id";
        var valor = _connection.Query<ValorDoMinuto>(sql, new { Id = id }).FirstOrDefault();

        return View(valor);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] ValorDoMinuto valorDoMinuto)
    {
        valorDoMinuto.Id = id;

        string sql = "UPDATE valores SET Minutos = @Minutos, Valor = @Valor WHERE Id = @Id";
        _connection.Execute(sql, valorDoMinuto);

        return Redirect("/valores");
    }

    [HttpPost("{id}/excluir")]
    public IActionResult Excluir([FromRoute] int id)
    {
        string sql = "DELETE FROM valores WHERE Id = @Id";
        _connection.Execute(sql, new { Id = id });

        return Redirect("/valores");
    }
}