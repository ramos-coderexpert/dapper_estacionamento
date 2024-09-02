using Dapper_estacionamento.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper_estacionamento.Models
{
    [Table("vaiculos")]
    public class Veiculo
    {
        [IgnoreInDapper]
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int ClientId { get; set; }
        [IgnoreInDapper]
        public Cliente Client { get; set; }
    }
}