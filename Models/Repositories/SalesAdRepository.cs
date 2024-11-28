
using LTKGMaster.Models.SalesAd;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace LTKGMaster.Models.Repositories
{
    public class SalesAdRepository
    {
        private readonly string _connectionString;

        public SalesAdRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }
        public void Add(ISalesAd salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO SalesAds (ProdId, UserId, Title)" + "VALUES (@ProdId, @UserId, @Title)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProdId", salesAd.ProductId);
                command.Parameters.AddWithValue("@UserId", salesAd.UserId);
                command.Parameters.AddWithValue("@Title", salesAd.Title);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(ISalesAd salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM SalesAds WHERE ProdId = @ProdId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ProdId", salesAd.ProductId);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void Update(ISalesAd salesAd)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE SalesAds SET Title = @Title, Product = @Product, UserId = @UserId, DateOfCreation = @DateOfCreation WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Title", salesAd.Title);
                    command.Parameters.AddWithValue("@Product", salesAd.ProductId);
                    command.Parameters.AddWithValue("@UserId", salesAd.UserId);
                    command.Parameters.AddWithValue("@DateOfCreation", salesAd.DateOfCreation);

                    command.ExecuteNonQuery();
                }
            }
        }
        public List<SalesAd.SalesAd> GetAll(ISalesAd salesAd)
        {
            List<SalesAd.SalesAd> salesAds = new List<SalesAd.SalesAd>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT ProdID, UserId, Title, DataOfCreation FROM SalesAds";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@ProdId", salesAd.ProductId);

                SqlDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        SalesAd.SalesAd salesAd1 = new SalesAd.SalesAd
                        {
                            ProductId = reader.GetInt32(reader.GetOrdinal("ProdId")),
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            DateOfCreation = reader.GetDateTime(reader.GetOrdinal("DateOfCreation"))
                        };
                        salesAds.Add(salesAd1);
                    }
                }
            }
            return salesAds;
        }
    }
}
