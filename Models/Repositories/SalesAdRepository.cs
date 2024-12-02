
using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAd;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;

namespace LTKGMaster.Models.Repositories
{
    public class SalesAdRepository : ISalesAdRepository
    {
        private readonly string _connectionString;

        public SalesAdRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }
        public void Add(SalesAds salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO SalesAds (ProdId, UserId, Title)" + "VALUES (@ProdId, @UserId, @Title)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProdId", salesAd._product.Id);
                command.Parameters.AddWithValue("@UserId", salesAd._user.Id);
                command.Parameters.AddWithValue("@Title", salesAd.Title);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(SalesAds salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM SalesAds WHERE ProdId = @ProdId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProdId", salesAd._product.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void Update(SalesAds salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE SalesAds SET Title = @Title, Product = @Product, UserId = @UserId, DateOfCreation = @DateOfCreation WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Title", salesAd.Title);
                    command.Parameters.AddWithValue("@Product", salesAd._product.Id);
                    command.Parameters.AddWithValue("@UserId", salesAd._user.Id);
                    command.Parameters.AddWithValue("@DateOfCreation", salesAd.DateOfCreation);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<SalesAds> GetAllFromUser(int id)
        {
            List<SalesAds> list = new List<SalesAds>();

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = @"
                            SELECT ProdId, UserId, Title, CreationDate, Products.Description, Products.Price 
                            FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId where UserId = @UserId";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@UserId", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SalesAds output = new SalesAds();
                    output._product.Id = reader.GetInt32(0);
                    output._user.Id = reader.GetInt32(1);
                    output.Title = reader.GetString(2);
                    output.DateOfCreation = reader.GetDateTime(3);
                    output._product.Description = reader.GetString(4);
                    output._product.Price = reader.GetDecimal(5);
                    
                    
                    list.Add(output);
                }
                return list;
            }
        }
        public List<SalesAds> GetAll(SalesAds salesAd)
        {
            List<SalesAds> salesAds = new List<SalesAds>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT ProdID, UserId, Title, CreationDate FROM SalesAds";

                SqlCommand command = new SqlCommand(sql, connection);

                SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        SalesAds salesAd1 = new SalesAds
                        {
                           // _product = reader.GetInt32(reader.GetOrdinal("ProdId")),
                           // _user = reader.GetInt32(reader.GetOrdinal("UserId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            DateOfCreation = reader.GetDateTime(reader.GetOrdinal("CreationDate"))
                        };
                        salesAds.Add(salesAd1);
                    }
                }
            }
            return salesAds;
        }
    }
}
