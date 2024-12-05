﻿
using LTKGMaster.Models.Products;
using LTKGMaster.Models.SalesAd;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace LTKGMaster.Models.Repositories
{
    public class SalesAdRepository : ISalesAdRepository
    {
        private readonly string _connectionString;
        private readonly ProductFactory _factory;

        public SalesAdRepository(IConfiguration configuration, ProductFactory factory)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
            _factory = factory;
        }
        public void Add(SalesAds salesAd)
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
        public void Delete(int id)
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
                            SELECT ProdId, UserId, Title, CreationDate, Products.Description, Products.Price, Products.CatId 
                            FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId where UserId = @UserId";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@UserId", id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SalesAds output = new SalesAds();
                    output.ProdId = reader.GetInt32(0);
                    output.UserId = reader.GetInt32(1);
                    output.Title = reader.GetString(2);
                    output.DateOfCreation = reader.GetDateTime(3);

                    Product outputProduct = _factory.Create((ProductType)reader.GetInt32(6));
                    outputProduct.Description = reader.GetString(4);
                    outputProduct.Price = reader.GetDecimal(5);
                    output._product = outputProduct;
                    //output._product.Description = reader.GetString(4);
                    //output._product.Price = reader.GetDecimal(5);
                    
                    
                    list.Add(output);
                }
                return list;
            }
        }

        public SalesAds GetById(int id)
        {
            SalesAds output = new SalesAds();

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

            return output;
        }

        public List<SalesAds> GetAll()
        {
            List<SalesAds> allProducts = new List<SalesAds>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT CatId, ProdId, UserId, Title, CreationDate, Products.Description, Products.Price " +
                            "FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId";

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SalesAds output = new SalesAds();
                    output.ProdId = reader.GetInt32(1);
                    output.UserId = reader.GetInt32(2);
                    output.Title = reader.GetString(3);
                    output.DateOfCreation = reader.GetDateTime(4);

                    Product outputProduct = _factory.Create((ProductType)reader.GetInt32(0));
                    outputProduct.Description = reader.GetString(5);
                    outputProduct.Price = reader.GetDecimal(6);

                    output._product = outputProduct;

                    allProducts.Add(output);
                }
                return allProducts;
            }
            }

        public List<SalesAds> GetAllLaptops()
        {
            List<SalesAds> allProducts = new List<SalesAds>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT CatId, ProdId, UserId, Title, CreationDate, Products.Description, Products.Price " +
                            "FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId WHERE CatId = 1";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@CatId", 1);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SalesAds output = new SalesAds();
                    //int type;
                    //type = (int)output._product.Type;

                    //type = reader.GetInt32(0);
                    output.ProdId = reader.GetInt32(1);
                    output.UserId = reader.GetInt32(2);
                    output.Title = reader.GetString(3);
                    output.DateOfCreation = reader.GetDateTime(4);
                    //output._product.Description = reader.GetString(5);
                    //output._product.Price = reader.GetDecimal(6);

                    Product pp = _factory.Create((ProductType)reader.GetInt32(0));
                    pp.Description = reader.GetString(5);
                    pp.Price = reader.GetDecimal(6);

                    output._product = pp;



                    allProducts.Add(output);
                }
                return allProducts;
            }
        }

        public List<SalesAds> GetAllProductsOfType(ProductType prodtype)
        {
            List<SalesAds> laptops = new List<SalesAds>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql ="SELECT CatId, ProdId, UserId, Title, CreationDate, Products.Description, Products.Price " +
                            "FROM SalesAds JOIN Products ON Products.Id = SalesAds.ProdId WHERE CatId = 1";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@CatId", 1);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SalesAds output = new SalesAds();
                    int type;
                    type = (int)output._product.Type;
                    type = reader.GetInt32(0);
                    output.ProdId = reader.GetInt32(1);
                    output.UserId = reader.GetInt32(2);
                    output.Title = reader.GetString(3);
                    output.DateOfCreation = reader.GetDateTime(4);
                    //output._product.Description = reader.GetString(5);
                    //output._product.Price = reader.GetDecimal(6);

                    Product product = _factory.Create(prodtype);
                    product.Description = reader.GetString(5);
                    product.Price = reader.GetDecimal(6);
                    
                    output._product = product;

                    laptops.Add(output);
                }
                return laptops;
            }
        }
    }
}
