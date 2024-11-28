using LTKGMaster.Models.Users;
using Microsoft.Data.SqlClient;

namespace LTKGMaster.Models.Repositories
{
    public class UserRepository : IUserRepo<IUser>
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }

        public void Add(IUser user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Users (UserName, CredEmail, City) " +
                             "VALUES (@UserName, @CredEmail, @City)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@CredEmail", user.CredMail);
                command.Parameters.AddWithValue("@City", user.City);
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
    }
}
