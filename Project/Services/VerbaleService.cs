using Project.Models;
using System.Data.SqlClient;


namespace Project.Services
{
    public class VerbaleService : IVerbaleService
    {
        private readonly string _connectionString;

        private const string CREATE_VERBALE_COMMAND = "INSERT INTO [dbo].[Verbale] " +
            "(DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti, IDAnagrafica, IDViolazione) " +
            "OUTPUT INSERTED.IDVerbale " +
            "VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti, @IDAnagrafica, @IDViolazione)";

        public VerbaleService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        public Verbale Create(Verbale verbale)
        {
            try
            {              
                verbale.DataTrascrizioneVerbale = DateTime.Now;

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_VERBALE_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                        command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                        command.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                        command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                        command.Parameters.AddWithValue("@Importo", verbale.Importo);
                        command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                        command.Parameters.AddWithValue("@IDAnagrafica", verbale.IDAnagrafica);
                        command.Parameters.AddWithValue("@IDViolazione", verbale.IDViolazione);

                        verbale.IDVerbale = (int)command.ExecuteScalar();
                    }
                    return verbale;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Verbale: " + ex.Message);
            }
        }
    }
}
