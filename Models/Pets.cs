using System;

namespace Models
{
    public class Pets
    {
        public int Id { get; set; }
        public string DonoNome { get; set; }
        public string Nome { get; set; }
        public string TipoDePet { get; set; }
        public int Idade { get; set; }
        public string Cor { get; set; }
        public DateTime DataDeRegistro { get; set; } = DateTime.Now;
    }
}
