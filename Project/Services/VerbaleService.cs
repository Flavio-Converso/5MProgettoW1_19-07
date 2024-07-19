using Project.Models;
using Project.Services;

public class VerbaleService : BaseService, IVerbaleService
{
    private const string CREATE_VERBALE_COMMAND = "INSERT INTO [dbo].[Verbale] " +
        "(DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDAnagrafica, IDViolazione) " +
        "OUTPUT INSERTED.IDVerbale " +
        "VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IDAnagrafica, @IDViolazione)";

    public VerbaleService(IConfiguration configuration)
        : base(configuration.GetConnectionString("DB"))
    {
    }

    public Verbale Create(Verbale verbale)
    {
        try
        {
            verbale.DataTrascrizioneVerbale = DateTime.Now;

            verbale.IDVerbale = ExecuteScalar<int>(
                CREATE_VERBALE_COMMAND,
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                    cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                    cmd.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                    cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                    cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                    cmd.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                    cmd.Parameters.AddWithValue("@IDAnagrafica", verbale.IDAnagrafica);
                    cmd.Parameters.AddWithValue("@IDViolazione", verbale.IDViolazione);
                }
            );

            return verbale;
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating Verbale: " + ex.Message);
        }
    }

    private const string GET_ALL_VERBALI_BY_TRASGRESSORE_COMMAND = "SELECT a.IDAnagrafica, a.Nome, a.Cognome, COUNT(v.IDVerbale) AS TotaleVerbali " +
        "FROM [dbo].[Verbale] v " +
        "JOIN [dbo].[Anagrafica] a ON v.IDAnagrafica = a.IDAnagrafica " +
        "GROUP BY a.IDAnagrafica, a.Nome, a.Cognome " +
        "ORDER BY TotaleVerbali DESC;";

    public List<VerbaleByTrasgressore> GetAllVerbaliByTrasgressore()
    {
        return ExecuteReader(
            GET_ALL_VERBALI_BY_TRASGRESSORE_COMMAND,
            reader => new VerbaleByTrasgressore
            {
                IDAnagrafica = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Cognome = reader.GetString(2),
                TotaleVerbali = reader.GetInt32(3)
            }
        );
    }
}
