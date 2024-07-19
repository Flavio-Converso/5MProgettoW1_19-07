using Project.Models;
using System.Data.SqlClient;


namespace Project.Services
{
    public class ViolazioneService : IViolazioneService
    {
        private readonly string _connectionString;

       

        public ViolazioneService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        private const string CREATE_VIOLAZIONE_COMMAND = "INSERT INTO [dbo].[Violazione] (Descrizione) OUTPUT INSERTED.IDViolazione VALUES (@Descrizione)";

        public Violazione Create(Violazione violazione)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_VIOLAZIONE_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);

                        violazione.IDViolazione = (int)command.ExecuteScalar();
                    }
                }
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
            var violazioni = new List<Violazione>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_ALL_VIOLAZIONI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var violazione = new Violazione
                                {
                                    IDViolazione = reader.GetInt32(0),
                                    Descrizione = reader.GetString(1)
                                };
                                violazioni.Add(violazione);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving violazioni: " + ex.Message);
            }

            return violazioni;
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
            var result = new List<ViolazioneOver10Punti>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(GET_VIOLAZIONI_OVER_10_PUNTI_COMMAND, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var violazione = new ViolazioneOver10Punti
                                {
                                    Importo = reader.GetDecimal(0),
                                    Nome = reader.GetString(1),
                                    Cognome = reader.GetString(2),
                                    DataViolazione = reader.GetDateTime(3),
                                    DecurtamentoPunti = reader.GetInt32(4)
                                };
                                result.Add(violazione);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving violations with over 10 points: " + ex.Message);
            }

            return result;
        }
    }
}
