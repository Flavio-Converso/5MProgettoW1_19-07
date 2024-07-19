using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class VerbaleByTrasgressore : Persona
    {
        [Required(ErrorMessage = "L'ID Anagrafica è obbligatorio.")]
        public int IDAnagrafica { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Il totale verbali deve essere un valore non negativo.")]
        public int TotaleVerbali { get; set; }
    }
}
