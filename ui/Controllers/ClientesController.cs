using Dapper_estacionamento.Models;
using Dapper_estacionamento.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_estacionamento.Controllers;

[Route("/clientes")]
public class ClientesController : Controller
{
    private readonly IRepository<Cliente> _repository;

    public ClientesController(IRepository<Cliente> repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var clientes = _repository.ObterTodos();

        return View(clientes);
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
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("criar")]
    public IActionResult Criar([FromForm] Cliente cliente)
    {
        _repository.Inserir(cliente);
        return Redirect("/clientes");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var valor = _repository.ObterPorId(id);
        return View(valor);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Cliente cliente)
    {
        cliente.Id = id;

        _repository.Atualizar(cliente);
        return Redirect("/clientes");
    }

    [HttpPost("{id}/excluir")]
    public IActionResult Excluir([FromRoute] int id)
    {
        _repository.Excluir(id);
        return Redirect("/clientes");
    }
}