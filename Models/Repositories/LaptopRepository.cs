﻿using LTKGMaster.Models.Products;
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

                string sql = "INSERT INTO Products (CatId, Description, Year, Brand, Model, Price, ScreenSize, Storage, OperatingSystem, RAM, CPU, GPU) " +
                             "VALUES (@CatId, @Description, @Year, @Brand, @Model, @Price, @ScreenSize, @Storage, @OperatingSystem, @RAM, @CPU, @GPU)" +
                             "SELECT Id FROM Products WHERE Id = SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(sql, connection);
                
                command.Parameters.AddWithValue("@CatId", (int)product.Type);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Year", product.Year);
                command.Parameters.AddWithValue("@Brand", product.Brand);
                command.Parameters.AddWithValue("@Model", product.Model);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@ScreenSize", product.ScreenSize);
                command.Parameters.AddWithValue("@Storage", product.Storage);
                command.Parameters.AddWithValue("@OperatingSystem", product.OperateSystem);
                command.Parameters.AddWithValue("@GPU", product.GPU);
                command.Parameters.AddWithValue("@RAM", product.RAM);
                command.Parameters.AddWithValue("@CPU", product.CPU);

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
                    "Brand = @Brand, Model = @Model, Price = @Price, ScreenSize = @ScreenSize," +
                    " Storage = @Storage, OperatingSystem = @OperatingSystem, GPU = @GPU, RAM = @RAM, CPU = @CPU WHERE Id = @Id";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@Year", product.Year);
                command.Parameters.AddWithValue("@Brand", product.Brand);
                command.Parameters.AddWithValue("@Model", product.Model);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@ScreenSize", product.ScreenSize);
                command.Parameters.AddWithValue("@Storage", product.Storage);
                command.Parameters.AddWithValue("@OperatingSystem", product.OperateSystem);
                command.Parameters.AddWithValue("@GPU", product.GPU);
                command.Parameters.AddWithValue("@RAM", product.RAM);
                command.Parameters.AddWithValue("@CPU", product.CPU);

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
    }
}
