using LTKGMaster.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// Handles data related to ProductPictures that is to be written or read from the sql database.
    /// </summary>
    public class PictureRepository : IPictureRepository
    {
        private readonly string _connectionString;

        public PictureRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
        }

        /// <summary>
        /// Adds data relating to a ProductPicture object to the sql database
        /// </summary>
        /// <param name="image">The ProductPicture containing the data to be written to the sql database</param>
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

        /// <summary>
        /// Removes a picture from the sql database
        /// </summary>
        /// <param name="id">The id of the Product of which the ProductPicture belongs to.</param>
        public void Delete(int id)
        {
            try
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
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database operation failed.", ex);
            }
        }

        /// <summary>
        /// Retrieves a list of ProductPictures to be used in SalesAd
        /// </summary>
        /// <param name="productId">The id of the Product of which the ProductPictures belongs to.</param>
        /// <returns>A list containing the ProductPictures that match the Id of the input productId</returns>
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
