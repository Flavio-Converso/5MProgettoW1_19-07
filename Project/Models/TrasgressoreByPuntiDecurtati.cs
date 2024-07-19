using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class TrasgressoreByPuntiDecurtati : Persona
    {
        [Required(ErrorMessage = "L'ID Anagrafica è obbligatorio.")]
        public int IDAnagrafica { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Il totale punti decurtati deve essere un valore non negativo.")]
        public int TotalePuntiDecurtati { get; set; }
    }
}
