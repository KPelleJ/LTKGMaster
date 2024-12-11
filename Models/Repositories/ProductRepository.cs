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
            if (configuration != null)
            {
                _connectionString = configuration.GetConnectionString("myDb1");
            }
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
                throw new InvalidOperationException("Fejl opstået ved tilføjelse af produkt. Server fejl.", e);
                //We could also have thrown a 'throw;' to keep the stack trace.
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
                throw new InvalidOperationException("Fejl ved opdatering af produkt. Server fejl.", e);
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
            }catch(SqlException e)
            {
                throw new InvalidOperationException("Fejl ved sletning af produkt. Server fejl.", e);
            }
        }

        /// <summary>
        /// This method finds the Product with the Id, 
        /// and because we have more that just one type of Product we send a ProductType parameter with it.
        /// </summary>
        /// <param name="id">It is the id that we use to get the Product</param>
        /// <param name="type">Its the type of Product we get when we call the method.</param>
        /// <returns>Returns the Product we want to get by id.</returns>
        public Product GetById(int id, ProductType type)
        {
            try
            {
                Product output = _factory.Create(type);

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
            catch (SqlException e)
            {
                throw new InvalidOperationException("Kunne ikke finde produkt med det Id. Server fejl.", e);
            }
            }
    }
}
