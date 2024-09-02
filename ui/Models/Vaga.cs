using Dapper_estacionamento.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper_estacionamento.Models
{
    [Table("vagas")]
    public class Vaga
    {
        [IgnoreInDapper]
        public int Id { get; set; }
        public string CodigoLocalizacao { get; set; }
        public bool Ocupada { get; set; }
    }
}