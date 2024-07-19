

using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class ViolazioneOver10Punti : Persona
    {
        [Range(0, double.MaxValue, ErrorMessage = "L'importo deve essere un valore positivo.")]
        public decimal Importo { get; set; }
        [Required(ErrorMessage = "La data di violazione è obbligatoria.")]
        public DateTime DataViolazione { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "I punti di decurtazione devono essere un valore non negativo.")]
        public int DecurtamentoPunti { get; set; }
    }
}
