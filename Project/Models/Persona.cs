using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public abstract class Persona
    {
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome può contenere al massimo 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il cognome può contenere al massimo 50 caratteri.")]
        public string Cognome { get; set; }
    }
}
