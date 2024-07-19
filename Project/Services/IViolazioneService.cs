using Project.Models;

namespace Project.Services
{
    public interface IViolazioneService
    {
        Violazione Create(Violazione violazione);   //FATTO
        List<Violazione> GetAllViolazioni();  //FATTO
        List<ViolazioneOver10Punti> GetViolazioneOver10Punti();   //FATTO
        List<ViolazioneOver400Importo> GetViolazioneOver400Importo();   //DA FARE
    }
}
