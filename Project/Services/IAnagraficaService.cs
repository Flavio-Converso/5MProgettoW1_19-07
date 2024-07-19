using Project.Models;

namespace Project.Services
{
    public interface IAnagraficaService
    {
        Anagrafica Create(Anagrafica anagrafica);
        List<TrasgressoreByPuntiDecurtati> GetAllTrasgressoreByPuntiDecurtati();
    }
}