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
    }
}
