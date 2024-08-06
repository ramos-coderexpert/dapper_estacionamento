using System.Diagnostics;

namespace Dapper_estacionamento.Models
{
    [DebuggerDisplay("Valor {Id}: {Minutos} valem R${Valor}")]
    public class ValorDoMinuto
    {
        public int Id { get; set; }
        public int Minutos { get; set; }
        public float Valor { get; set; }
    }
}