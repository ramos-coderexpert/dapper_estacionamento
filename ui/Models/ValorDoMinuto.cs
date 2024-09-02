using Dapper_estacionamento.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Dapper_estacionamento.Models
{
    [DebuggerDisplay("Valor {Id}: {Minutos} valem R${Valor}")]
    [Table("valores")]
    public class ValorDoMinuto
    {
        [IgnoreInDapper]
        public int Id { get; set; }
        public int Minutos { get; set; }
        public float Valor { get; set; }
    }
}