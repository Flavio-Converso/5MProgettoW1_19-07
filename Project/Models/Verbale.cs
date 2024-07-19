using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Verbale
    {
        public int IDVerbale { get; set; }

        [Required]
        public DateTime DataViolazione { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "L'indirizzo della violazione può contenere al massimo 100 caratteri.")]
        public string IndirizzoViolazione { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Il nome dell'agente può contenere al massimo 100 caratteri.")]
        [RegularExpression(@"^[a-zA-Z\s'-]+$", ErrorMessage = "Il nome dell'agente può contenere solo lettere, spazi, apostrofi e trattini.")]
        public string Nominativo_Agente { get; set; }

        [Required]
        public DateTime DataTrascrizioneVerbale { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "L'importo deve essere un valore positivo.")]
        public decimal Importo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "I punti di decurtazione devono essere un valore non negativo.")]
        public int DecurtamentoPunti { get; set; }

        [Required]
        public int IDAnagrafica { get; set; }

        [Required]
        public int IDViolazione { get; set; }
    }
}
