namespace Dapper_estacionamento.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public string CodigoLocalizacao { get; set; }
        public bool Ocupada { get; set; }
    }
}