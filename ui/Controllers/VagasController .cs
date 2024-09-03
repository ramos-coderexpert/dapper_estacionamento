using Dapper_estacionamento.Models;
using Dapper_estacionamento.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dapper_estacionamento.Controllers;

[Route("/vagas")]
public class VagasController : Controller
{
    private readonly IRepository<Vaga> _repository;

    public VagasController(IRepository<Vaga> repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var vagas = _repository.ObterTodos();

        return View(vagas);
        // Executando Procedure, forma simples
        //var parameters = new DynamicParameters();
        //parameters.Add("@idVaga", 1);
        //var tickets = _connection.Query<TicketComVaga>("ObterTicketsPorVaga", parameters, commandType: CommandType.StoredProcedure);


        // Executando Procedure, forma completa
        //var parameters = new DynamicParameters();
        //parameters.Add("@idVaga", 1);
        //var tickets = _connection.Query<Ticket, string, TicketComVaga>("ObterTicketsPorVaga", (ticket, nomeVaga) =>
        //{
        //    return new TicketComVaga { Ticket = ticket, NomeVaga = nomeVaga };
        //},
        //parameters, splitOn: "NomeVaga", commandType: CommandType.StoredProcedure);


        // Executando View, forma simples
        //var sqlView = "SELECT * FROM VistaTicketComVagas";
        //var ticketsComVagas = _connection.Query<TicketComVaga>(sqlView);


        // Executando View, forma completa
        //var ticketsComVagas = _connection.Query<Ticket, string, TicketComVaga>("SELECT * FROM VistaTicketComVagas",
        //(ticket, nomeVaga) =>
        //{
        //    return new TicketComVaga { Ticket = ticket, NomeVaga = nomeVaga };
        //}, splitOn: "NomeVaga");
    }

    [HttpGet("novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("criar")]
    public IActionResult Criar([FromForm] Vaga vaga)
    {
        _repository.Inserir(vaga);
        return Redirect("/vagas");
    }

    [HttpGet("{id}/editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        var valor = _repository.ObterPorId(id);
        return View(valor);
    }

    [HttpPost("{id}/alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Vaga vaga)
    {
        vaga.Id = id;

        _repository.Atualizar(vaga);
        return Redirect("/vagas");
    }

    [HttpPost("{id}/excluir")]
    public IActionResult Excluir([FromRoute] int id)
    {
        _repository.Excluir(id);
        return Redirect("/vagas");
    }
}