using Project.Models;
using Project.Services;

public class ViolazioneService : BaseService, IViolazioneService
{
    private const string CREATE_VIOLAZIONE_COMMAND = "INSERT INTO [dbo].[Violazione] (Descrizione) OUTPUT INSERTED.IDViolazione VALUES (@Descrizione)";

    public ViolazioneService(IConfiguration configuration)
        : base(configuration.GetConnectionString("DB"))
    {
    }

    public Violazione Create(Violazione violazione)
    {
        try
        {
            violazione.IDViolazione = ExecuteScalar<int>(
                CREATE_VIOLAZIONE_COMMAND,
                cmd => cmd.Parameters.AddWithValue("@Descrizione", violazione.Descrizione)
            );
            return violazione;
        }
        catch (Exception ex)
        {
            throw new Exception("Error creating Violazione: " + ex.Message);
        }
    }

    private const string GET_ALL_VIOLAZIONI_COMMAND = "SELECT IDViolazione, Descrizione FROM [dbo].[Violazione]";
    public List<Violazione> GetAllViolazioni()
    {
        return ExecuteReader(
            GET_ALL_VIOLAZIONI_COMMAND,
            reader => new Violazione
            {
                IDViolazione = reader.GetInt32(0),
                Descrizione = reader.GetString(1)
            }
        );
    }

    private const string GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND = @"
        SELECT 
            v.Importo, 
            a.Nome, 
            a.Cognome, 
            v.DataViolazione, 
            v.DecurtamentoPunti
        FROM [dbo].[Verbale] v
        JOIN [dbo].[Anagrafica] a ON v.IDAnagrafica = a.IDAnagrafica
        WHERE v.DecurtamentoPunti > 10
        ORDER BY v.DecurtamentoPunti DESC;";
    public List<ViolazioneOver10Punti> GetViolazioneOver10Punti()
    {
        return ExecuteReader(
            GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND,
            reader => new ViolazioneOver10Punti
            {
                Importo = reader.GetDecimal(0),
                Nome = reader.GetString(1),
                Cognome = reader.GetString(2),
                DataViolazione = reader.GetDateTime(3),
                DecurtamentoPunti = reader.GetInt32(4)
            }
        );
    }

    private const string GET_VIOLAZIONI_OVER_400_IMPORTO_COMMAND = @"
        SELECT 
            a.Nome, 
            a.Cognome, 
            v.DataViolazione,                
            v.Importo
        FROM [dbo].[Verbale] v
        JOIN [dbo].[Anagrafica] a ON v.IDAnagrafica = a.IDAnagrafica
        WHERE v.Importo > 400
        ORDER BY v.Importo DESC;";

    public List<ViolazioneOver400Importo> GetViolazioneOver400Importo()
    {
        return ExecuteReader(
            GET_VIOLAZIONI_OVER_400_IMPORTO_COMMAND,
            reader => new ViolazioneOver400Importo
            {
                Nome = reader.GetString(0),
                Cognome = reader.GetString(1),
                DataViolazione = reader.GetDateTime(2),
                Importo = reader.GetDecimal(3)
            }
        );
    }
}
