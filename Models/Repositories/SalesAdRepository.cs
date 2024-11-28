
using LTKGMaster.Models.SalesAd;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace LTKGMaster.Models.Repositories
{
    public class SalesAdRepository
    {
        private readonly string _connectionString;

        public SalesAdRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }
        public void Add(SalesAd.SalesAd salesAd)
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

        public void Delete(SalesAd.SalesAd salesAd)
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
        public void Update(SalesAd.SalesAd salesAd)
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
    }
}
