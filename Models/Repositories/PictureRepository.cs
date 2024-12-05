using LTKGMaster.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace LTKGMaster.Models.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly string _connectionString;

        public PictureRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }

        public void Add(ProductPicture image)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO ProductPictures (ProdId, PictureString, FileName) " +
                             "VALUES (@ProdId, @PictureString, @FileName)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProdId", image.ProductId);
                command.Parameters.AddWithValue("@PictureString", image.ImageDataByteArray);
                command.Parameters.AddWithValue("FileName", image.FileName);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "DELETE FROM ProductPictures Where ProdId = @ProdId";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@ProdId", id);

                command.ExecuteNonQuery();
            }
        }

        public List<ProductPicture> GetAll(int productId)
        {
            List<ProductPicture> outputPictures = new List<ProductPicture>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM ProductPictures WHERE ProdId = @ProdId";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ProdId", productId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    byte[] outputByte = (byte[])reader[1];
                    ProductPicture output = new ProductPicture(reader.GetInt32(0), reader.GetString(2), outputByte);
                    outputPictures.Add(output);
                }

            }
            return outputPictures;
        }
    }
}
