using Project.Models;

namespace Project.Services
{
    public interface IVerbaleService
    {
        Verbale Create(Verbale verbale); 
        List<VerbaleByTrasgressore> GetAllVerbaliByTrasgressore(); 
        
    }
}
