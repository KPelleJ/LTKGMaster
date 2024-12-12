using LTKGMaster.Models.Products;
using Microsoft.Data.SqlClient;

namespace LTKGMaster.Models.Repositories
{
    /// <summary>
    /// This class handles CRUD funktions for the product class.
    /// </summary>
    public class ProductRepository:IProductRepository
    {
        private readonly string _connectionString;
        private readonly ProductFactory _factory;

        public ProductRepository(IConfiguration configuration, ProductFactory factory)
        {
            _connectionString = configuration.GetConnectionString("myDb1");
            _factory = factory;
        }

        /// <summary>
        /// This method adds a new product to the database and returns the product again.
        /// </summary>
        /// <param name="product">This is the product we want to add to the database</param>
        /// <returns>The product we want to return so that we can use it somewhere else in our code.</returns>
        public Product Add(Product product)
        {
            try
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
            //Here we catch an error if that would have occured
            catch(SqlException e)
            {
                throw new InvalidOperationException("Database operation failed.", e);
            }
        }

        /// <summary>
        /// This method handles the update of an already excisting product in the database.
        /// </summary>
        /// <param name="product">The product we want to update.</param>
        public void Update(Product product)
        {
            try
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
            catch (SqlException e) 
            {
                throw new InvalidOperationException("Database operation failed.", e);
            } 
        }

        /// <summary>
        /// This method handles the deleting of the product we have on our website, so that the customers can delete their sales ads
        /// it also deletes the product form the database.
        /// </summary>
        /// <param name="id">It is the id of the product we want to delete.</param>
        public void Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM Products Where Id = @Id";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException("Database operation failed.", ex);
            }
        }

        /// <summary>
        /// Retrieves a Product object from the sql database by it's ProductId
        /// </summary>
        /// <param name="id">The Id of the product we want to retrieve</param>
        /// <returns>Returns the Product from the databaes with a matching id</returns>
        public Product GetById(int id)
        {
            try
            {
                Product output = new StandardProduct();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string sql = "SELECT Id, Description, Year, Brand, Model, Price, CatId FROM Products WHERE Id = @Id;";

                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        output = _factory.Create((ProductType)reader.GetInt32(6));
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
            catch (SqlException e)
            {
                throw new InvalidOperationException("Database operation failed.", e);
            }
        }
    }
}
