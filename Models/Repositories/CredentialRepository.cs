using LTKGMaster.Models.Users;
using Microsoft.Data.SqlClient;

namespace LTKGMaster.Models.Repositories
{
    public class CredentialRepository:IUserRepo<ICredential>
    {
        private readonly string _connectionString;

        public CredentialRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }

        public void Add(ICredential credential)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Credentials (Email, PasswordHash) " +
                             "VALUES (@Email, @PasswordHash)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", credential.Email);
                command.Parameters.AddWithValue("@PasswordHash", credential.PasswordHash);
                command.ExecuteNonQuery();
            }
        }
    }
}
