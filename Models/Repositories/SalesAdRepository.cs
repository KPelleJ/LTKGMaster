
using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAds;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// This class is responsible for interacting with the database to manage sales ads.
    /// It handles operations such as adding, updating, deleting, and fetching sales ads.
    /// </summary>
    public class SalesAdRepository : ISalesAdRepository
    {
        private readonly string _connectionString;
        private readonly ProductFactory _factory;

        /// <summary>
        /// Initializes a new instance of the SalesAdRepository class.
        /// </summary>
        /// <param name="configuration">The configuration object that contains the connection string.</param>
        /// <param name="factory">The ProductFactory instance used to create products.</param>
        public SalesAdRepository(IConfiguration configuration, ProductFactory factory)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
            _factory = factory;
        }

        /// <summary>
        /// Adds a new sales ad to the database.
        /// </summary>
        /// <param name="salesAd">The SalesAd object to add to the database.</param>
        public void Add(SalesAd salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO SalesAds (ProdId, UserId, Title)" + "VALUES (@ProdId, @UserId, @Title)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProdId", salesAd.ProdId);
                command.Parameters.AddWithValue("@UserId", salesAd.UserId);
                command.Parameters.AddWithValue("@Title", salesAd.Title);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a sales ad from the database based on the provided product ID.
        /// </summary>
        /// <param name="id">The product ID of the sales ad to delete.</param>
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM SalesAds WHERE ProdId = @ProdId";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ProdId", id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database operation failed.", ex);
            }
        }

        /// <summary>
        /// Updates an existing sales ad in the database.
        /// </summary>
        /// <param name="salesAd">The SalesAd object containing the updated information.</param>
        public void Update(SalesAd salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE SalesAds SET Title = @Title, Product = @Product, UserId = @UserId, DateOfCreation = @DateOfCreation WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Title", salesAd.Title);
                    command.Parameters.AddWithValue("@Product", salesAd.Product.Id);
                    command.Parameters.AddWithValue("@UserId", salesAd.User.Id);
                    command.Parameters.AddWithValue("@DateOfCreation", salesAd.DateOfCreation);

                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Fetches all sales ads for a specific user based on the user ID.
        /// </summary>
        /// <param name="id">The user ID to fetch sales ads for.</param>
        /// <returns>A list of SalesAd objects for the specified user.</returns>
        public List<SalesAd> GetAllFromUser(int id)
        {
            try
            {
                List<SalesAd> list = new List<SalesAd>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = @"
                            SELECT ProdId, UserId, Title, CreationDate, Products.Description, Products.Price, Products.CatId 
                            FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId where UserId = @UserId";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@UserId", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        SalesAd output = new SalesAd();
                        output.ProdId = reader.GetInt32(0);
                        output.UserId = reader.GetInt32(1);
                        output.Title = reader.GetString(2);
                        output.DateOfCreation = reader.GetDateTime(3);

                        Product outputProduct = _factory.Create((ProductType)reader.GetInt32(6));
                        outputProduct.Description = reader.GetString(4);
                        outputProduct.Price = reader.GetDecimal(5);
                        output.Product = outputProduct;


                        list.Add(output);
                    }
                    return list;
                }
            }
            catch (SqlException ex)
            { 
                throw new InvalidOperationException("Database operation failed.", ex);
            }
        }

        /// <summary>
        /// Fetches a sales ad by its ID.
        /// </summary>
        /// <param name="id">The ID of the sales ad to fetch.</param>
        /// <returns>The SalesAd object with the specified ID.</returns>
        public SalesAd GetById(int id)
        {
            SalesAd output = new SalesAd();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT ProdId, UserId, Title, CreationDate FROM SalesAds WHERE ProdId = @ProdId";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@ProdId", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    output.ProdId = reader.GetInt32(0);
                    output.UserId = reader.GetInt32(1);
                    output.Title = reader.GetString(2);
                    output.DateOfCreation = reader.GetDateTime(3);
                }
            }

            return output; // Return the fetched SalesAd object
        }

        /// <summary>
        /// Fetches all sales ads from the database.
        /// </summary>
        /// <returns>A list of all SalesAd objects in the database.</returns>
        public List<SalesAd> GetAll()
        {
            List<SalesAd> allProducts = new List<SalesAd>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT CatId, ProdId, UserId, Title, CreationDate, Products.Description, Products.Price " +
                             "FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalesAd output = new SalesAd();
                    output.ProdId = reader.GetInt32(1);
                    output.UserId = reader.GetInt32(2);
                    output.Title = reader.GetString(3);
                    output.DateOfCreation = reader.GetDateTime(4);

                    Product outputProduct = _factory.Create((ProductType)reader.GetInt32(0));
                    outputProduct.Description = reader.GetString(5);
                    outputProduct.Price = reader.GetDecimal(6);

                    output.Product = outputProduct;

                    allProducts.Add(output);
                }
                return allProducts; // Return the list of all sales ads
            }
            }

        /// <summary>
        /// Fetches all sales ads of a specific product type.
        /// </summary>
        /// <param name="prodtype">The product type to filter the sales ads by.</param>
        /// <returns>A list of SalesAd objects of the specified product type.</returns>
        public List<SalesAd> GetAllProductsOfType(ProductType prodtype)
        {
            List<SalesAd> laptops = new List<SalesAd>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql ="SELECT CatId, ProdId, UserId, Title, CreationDate, Products.Description, Products.Price " +
                            "FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId WHERE CatId = @CatId";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CatId", prodtype);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalesAd output = new SalesAd();
                    output.ProdId = reader.GetInt32(1);
                    output.UserId = reader.GetInt32(2);
                    output.Title = reader.GetString(3);
                    output.DateOfCreation = reader.GetDateTime(4);

                    Product product = _factory.Create(prodtype);
                    product.Description = reader.GetString(5);
                    product.Price = reader.GetDecimal(6);
                    output.Product = product;

                    laptops.Add(output);
                }
                return laptops; // Return the list of all sales ads
            }
        }
    }
}
