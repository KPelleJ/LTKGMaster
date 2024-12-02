using LTKGMaster.Models.Users;
using Microsoft.Data.SqlClient;

namespace LTKGMaster.Models.Repositories
{
    //Author Kasper
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }

        public void Add(IUser account)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Credentials (Email, PasswordHash) " +
                             "VALUES (@Email, @PasswordHash);" +
                             "INSERT INTO Users (UserName, CredEmail, City)" +
                             "VALUES (@UserName, @CredEmail, @City)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", account.Credential.Email);
                command.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.EnhancedHashPassword(account.Credential.Password,12));
                command.Parameters.AddWithValue("@UserName", account.UserName);
                command.Parameters.AddWithValue("@CredEmail", account.Credential.Email);
                command.Parameters.AddWithValue("@City", account.City);
                command.ExecuteNonQuery();
            }
        }

        public void Update(IUser user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Users (UserName, City) " +
                             "VALUES (@UserName, @City)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@City", user.City);
                command.ExecuteNonQuery();
            }
        }

        public IUser Get(string email)
        {
            //Det her skal laves om så der er en bedre måde at lave RegularUser på
            IUser output = new RegularUser();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, CredEmail, UserName, SignUpDate, Rating, City, Credentials.PasswordHash FROM Users " +
                             $"JOIN Credentials ON Users.CredEmail = Credentials.Email WHERE CredEmail = '{email}'";

                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    output.Id = reader.GetInt32(0);
                    output.CredMail = reader.GetString(1);
                    output.Credential.Email = output.CredMail;
                    output.UserName = reader.GetString(2);
                    output.SignUpDate = reader.GetDateTime(3);
                    output.Rating = reader.GetInt32(4);
                    output.City = reader.GetString(5);
                    output.Credential.PasswordHash = reader.GetString(6);
                }
            }
            return output;
        }
    }
}
