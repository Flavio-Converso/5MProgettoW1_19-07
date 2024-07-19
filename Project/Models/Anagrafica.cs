using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Anagrafica : Persona
    {
        public int IDAnagrafica { get; set; }

        [Required(ErrorMessage = "Il Codice Fiscale è obbligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale può contenere al massimo 16 caratteri.")]
        public string Cod_Fisc { get; set; }

        [Required(ErrorMessage = "L'indirizzo è obbligatorio.")]
        [StringLength(100, ErrorMessage = "L'indirizzo può contenere al massimo 100 caratteri.")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "La città è obbligatoria.")]
        [StringLength(50, ErrorMessage = "La città può contenere al massimo 50 caratteri.")]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "La città può contenere solo lettere, spazi, apostrofi e trattini.")]
        public string Città { get; set; }

        [Required(ErrorMessage = "Il CAP è obbligatorio.")]
        [StringLength(5, ErrorMessage = "Il CAP deve essere di 5 caratteri.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Il CAP deve contenere esattamente 5 numeri.")]
        public string CAP { get; set; }
    }
}
