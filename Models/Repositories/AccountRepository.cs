using LTKGMaster.Models.Users;
using Microsoft.Data.SqlClient;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// Handles data related to User accounts between the client and sql database.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }

        /// <summary>
        /// Adds a IUser object to the sql database
        /// </summary>
        /// <param name="account">IUser object with user information to be written to the sql database</param>
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
                command.Parameters.AddWithValue("@PasswordHash", account.Credential.PasswordHash);
                command.Parameters.AddWithValue("@UserName", account.UserName);
                command.Parameters.AddWithValue("@CredEmail", account.Credential.Email);
                command.Parameters.AddWithValue("@City", account.City);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates a IUser object in the sql database
        /// </summary>
        /// <param name="user">IUser object with user information to be updated in the sql database</param>
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

        /// <summary>
        /// Reads and gets an IUser object from the sql database. Used when users want to log in to the system.
        /// </summary>
        /// <param name="email">Unique email identifier used to retrieve information regarding the IUser object</param>
        /// <returns>The IUser object matching the email</returns>
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

        /// <summary>
        /// Reads and gets an IUser object from the sql database. Used when only the UserId is known for retrieving information
        /// regarding salesads
        /// </summary>
        /// <param name="id">The UserId of the IUser object that is to be retrieved</param>
        /// <returns>IUser object from the sql database with a matching UserId</returns>
        public IUser GetById(int id)
        {
            IUser output = new RegularUser();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT Id, CredEmail, UserName, SignUpDate, Rating, City, Credentials.PasswordHash FROM Users " +
                             $"JOIN Credentials ON Users.CredEmail = Credentials.Email WHERE Id = @Id";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

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
