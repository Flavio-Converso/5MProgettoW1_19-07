using Project.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Project.Services
{
    public class ViolazioneService : IViolazioneService
    {
        private readonly string _connectionString;

        private const string GET_ALL_VIOLAZIONI_COMMAND = "SELECT IDViolazione, Descrizione FROM [dbo].[Violazione]";

        public ViolazioneService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

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
