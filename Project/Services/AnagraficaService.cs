using Project.Models;
using Project.Services;

public class AnagraficaService : BaseService, IAnagraficaService
{
    private const string CREATE_ANAGRAFICA_COMMAND = "INSERT INTO [dbo].[Anagrafica] " +
        "(Nome, Cognome, Indirizzo, Città, CAP, Cod_Fisc) " +
        "OUTPUT INSERTED.IDAnagrafica " +
        "VALUES (@Nome, @Cognome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";

    public AnagraficaService(IConfiguration configuration)
        : base(configuration.GetConnectionString("DB"))
    {
    }

    public Anagrafica Create(Anagrafica anagrafica)
    {
        try
        {
            anagrafica.IDAnagrafica = ExecuteScalar<int>(
                CREATE_ANAGRAFICA_COMMAND,
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    cmd.Parameters.AddWithValue("@Città", anagrafica.Città);
                    cmd.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                    cmd.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);
                }
            );
            return anagrafica;
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating Anagrafica: " + ex.Message);
        }
    }

    private const string GET_ALL_VERBALI_BY_PUNTI_DECURTATI_COMMAND = "SELECT a.IDAnagrafica, a.Nome, a.Cognome, SUM(v.DecurtamentoPunti) AS TotalePuntiDecurtati " +
       "FROM [dbo].[Verbale] v " +
       "JOIN [dbo].[Anagrafica] a ON v.IDAnagrafica = a.IDAnagrafica " +
       "GROUP BY a.IDAnagrafica, a.Nome, a.Cognome " +
       "ORDER BY TotalePuntiDecurtati DESC;";

    public List<TrasgressoreByPuntiDecurtati> GetAllTrasgressoreByPuntiDecurtati()
    {
        return ExecuteReader(
            GET_ALL_VERBALI_BY_PUNTI_DECURTATI_COMMAND,
            reader => new TrasgressoreByPuntiDecurtati
            {
                IDAnagrafica = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Cognome = reader.GetString(2),
                TotalePuntiDecurtati = reader.GetInt32(3)
            }
        );
    }
}
