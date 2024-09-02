using Dapper_estacionamento.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper_estacionamento.Models
{
    [Table("tickets")]
    public class Ticket
    {
        [IgnoreInDapper]
        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
        public float Valor { get; set; }
        public int VeiculoId { get; set; }

        [IgnoreInDapper]
        public Veiculo veiculo { get; set; }
        public int VagaId { get; set; }
    }
}