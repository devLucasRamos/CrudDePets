using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class CreateViewModel
    {
        [Required]
        public string DonoNome { get; set; }
        public string Nome { get; set; }
        public string TipoDePet { get; set; }
        public int Idade { get; set; }
        public string Cor { get; set; }
    }
}
