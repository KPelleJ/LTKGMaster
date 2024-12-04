using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAd;
using Microsoft.Data.SqlClient;

namespace LTKGMaster.Models.Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly string _connectionString;
        public LaptopRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }
        public Laptop Add(Laptop product)
        {
               
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
        
                string sql = "INSERT INTO Products (CatId, Description, Year, Brand, Model, Price) " +
                             "VALUES (@CatId, @Description, @Year, @Brand, @Model, @Price)" +
                             "SELECT Id FROM Products WHERE Id = SCOPE_IDENTITY();";
        
                SqlCommand command = new SqlCommand(sql, connection);
                
                command.Parameters.AddWithValue("@CatId", product.Type);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Year", product.Year);
                command.Parameters.AddWithValue("@Brand", product.Brand);
                command.Parameters.AddWithValue("@Model", product.Model);
                command.Parameters.AddWithValue("@Price", product.Price);
        
                SqlDataReader reader = command.ExecuteReader();
        
                while (reader.Read())
                {
                    product.Id = reader.GetInt32(0);
                }
            }
            return product;
        }
        public void Update(Laptop product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
        
                string sql = "UPDATE Products SET Description = @Description, Year = @Year," +
                    "Brand = @Brand, Model = @Model, Price = @Price WHERE Id = @Id";
        
                SqlCommand command = new SqlCommand(sql, connection);
        
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Year", product.Year);
                command.Parameters.AddWithValue("@Brand", product.Brand);
                command.Parameters.AddWithValue("@Model", product.Model);
                command.Parameters.AddWithValue("@Price", product.Price);
        
                command.ExecuteNonQuery();
            }
        }
        public void Delete(Laptop product)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
        
                string sql = "DELETE FROM Products Where Id = @Id";
        
                SqlCommand command = new SqlCommand(sql, connection);
        
                command.Parameters.AddWithValue("@Id", product.Id);
        
                command.ExecuteNonQuery();
            }
        }
        public Laptop GetById(int id)
        {
            Laptop output = new Laptop();
        
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
        
                string sql = "SELECT Id, Description, Year, Brand, Model, Price FROM Products WHERE Id = @Id;";
        
                SqlCommand command = new SqlCommand(sql, connection);
        
                command.Parameters.AddWithValue("@Id", id);
        
                SqlDataReader reader = command.ExecuteReader();
        
                while (reader.Read())
                {
                    output.Id = reader.GetInt32(0);
                    output.Description = reader.GetString(1);
                    output.Year = reader.GetInt32(2);
                    output.Brand = reader.GetString(3);
                    output.Model = reader.GetString(4);
                    output.Price = reader.GetDecimal(5);
                }
            }
        
            return output;
        }
    }
}
