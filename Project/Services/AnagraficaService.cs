using Project.Models;
using System.Data.SqlClient;


namespace Project.Services
{
    public class AnagraficaService : IAnagraficaService
    {
        private readonly string _connectionString;

        private const string CREATE_ANAGRAFICA_COMMAND = "INSERT INTO [dbo].[Anagrafica] " +
            "(Nome, Cognome, Indirizzo, Città, CAP, Cod_Fisc) " +
            "OUTPUT INSERTED.IDAnagrafica " + 
            "VALUES (@Nome, @Cognome, @Indirizzo, @Città, @CAP, @Cod_Fisc)";

        public AnagraficaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DB");
        }

        public Anagrafica Create(Anagrafica anagrafica)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(CREATE_ANAGRAFICA_COMMAND, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                        command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                        command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                        command.Parameters.AddWithValue("@Città", anagrafica.Città);
                        command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                        command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);

                        anagrafica.IDAnagrafica = (int)command.ExecuteScalar();
                    }
                    return anagrafica;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Anagrafica: " + ex.Message);
            }
        }
    }
}
