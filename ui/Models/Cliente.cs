using Dapper_estacionamento.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper_estacionamento.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [IgnoreInDapper]
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Cpf { get; set; }
    }
}