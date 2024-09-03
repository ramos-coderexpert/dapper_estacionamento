using Dapper;
using Dapper_estacionamento.Models;
using Dapper_estacionamento.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Dapper_estacionamento.Controllers;

[Route("/veiculos")]
public class VeiculosController : Controller
{
    private readonly IDbConnection _connection;
    private readonly IRepository<Veiculo> _repository;

    public VeiculosController(IRepository<Veiculo> repository, IDbConnection connection)
    {
        _connection = connection;
        _repository = new Repository<Veiculo>(_connection);
    }

    public IActionResult Index()
    {
        var sql = @"SELECT V.*, C.* FROM veiculos V INNER JOIN clientes C ON C.id = V.clientId";
        var veiculos = _connection.Query<Veiculo, Cliente, Veiculo>(sql, (veiculo, cliente) =>
        {
            veiculo.Client = cliente;
            return veiculo;
        }, splitOn: "Id");

        return View(veiculos);
        // Executando Procedure, forma simples
        //var parameters = new DynamicParameters();
        //parameters.Add("@idVeiculo", 1);
        //var tickets = _connection.Query<TicketComVeiculo>("ObterTicketsPorVeiculo", parameters, commandType: CommandType.StoredProcedure);


        // Executando Procedure, forma completa
        //var parameters = new DynamicParameters();
        //parameters.Add("@idVeiculo", 1);
        //var tickets = _connection.Query<Ticket, string, TicketComVeiculo>("ObterTicketsPorVeiculo", (ticket, nomeVeiculo) =>
        //{
        //    return new TicketComVeiculo { Ticket = ticket, NomeVeiculo = nomeVeiculo };
        //},
        //parameters, splitOn: "NomeVeiculo", commandType: CommandType.StoredProcedure);


        // Executando View, forma simples
        //var sqlView = "SELECT * FROM VistaTicketComVeiculos";
        //var ticketsComVeiculos = _connection.Query<TicketComVeiculo>(sqlView);


        // Executando View, forma completa
        //var ticketsComVeiculos = _connection.Query<Ticket, string, TicketComVeiculo>("SELECT * FROM VistaTicketComVeiculos",
        //(ticket, nomeVeiculo) =>
        //{
        //    return new TicketComVeiculo { Ticket = ticket, NomeVeiculo = nomeVeiculo };
        //}, splitOn: "NomeVeiculo");
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("criar")]
    public IActionResult Criar([FromForm] Veiculo veiculo)
    {
        _repository.Inserir(veiculo);
        return Redirect("/veiculos");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var valor = _repository.ObterPorId(id);
        return View(valor);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Veiculo veiculo)
    {
        veiculo.Id = id;

        _repository.Atualizar(veiculo);
        return Redirect("/veiculos");
    }

    [HttpPost("{id}/excluir")]
    public IActionResult Excluir([FromRoute] int id)
    {
        _repository.Excluir(id);
        return Redirect("/veiculos");
    }
}