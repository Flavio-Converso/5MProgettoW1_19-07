using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Violazione
    {
        public int IDViolazione { get; set; }

        [Required(ErrorMessage = "La descrizione è obbligatoria.")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "La descrizione non può superare i 100 caratteri.")]
        public string Descrizione { get; set; }
    }
}
