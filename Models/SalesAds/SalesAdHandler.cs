using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.Users;

namespace LTKGMaster.Models.SalesAds
{
    /// <summary>
    /// This class handles operations related to sales ads, such as adding, deleting, retrieving, and managing sales ad data.
    /// It interacts with repositories for products, sales ads, users, and product pictures.
    /// </summary>
    public class SalesAdHandler
    {
        // Repositories for interacting with product, sales ad, account, and picture data
        private readonly IProductRepository _productRepository;
        private readonly ISalesAdRepository _salesAdRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPictureRepository _pictureRepository;
        private readonly ProductPictureConverter _pictureConverter;

        public SalesAdHandler(IProductRepository productRepository, ISalesAdRepository salesAdRepository, IPictureRepository pictureRepository, IAccountRepository accountRepository, ProductPictureConverter productPictureConverter)
        {
            /// <summary>
            /// Constructor that initializes a new instance of the SalesAdHandler class
            /// with the specified repositories and converter for handling sales ads.
            /// </summary>
            /// <param name="productRepository">The product repository for managing product data.</param>
            /// <param name="salesAdRepository">The sales ad repository for managing sales ad data.</param>
            /// <param name="pictureRepository">The picture repository for managing product pictures.</param>
            /// <param name="accountRepository">The account repository for managing user account data.</param>
            /// <param name="productPictureConverter">The converter for handling product pictures.</param>
            _productRepository = productRepository;
            _salesAdRepository = salesAdRepository;
            _pictureRepository = pictureRepository;
            _accountRepository = accountRepository;
            _pictureConverter = productPictureConverter;
        }

        /// <summary>
        /// Adds a product and its associated sales ad, including product images.
        /// </summary>
        /// <param name="product">The product to be added in the sales ad.</param>
        /// <param name="salesAd">The sales ad object containing details about the ad.</param>
        /// <param name="productImages">The images associated with the product.</param>
        public void Add(Product product, SalesAd salesAd, List<IFormFile> productImages)
        {
            salesAd.ProdId = _productRepository.Add(product).Id;

            // Convert each image to a byte array and add it to the picture repository
            foreach (var image in productImages)
            {
                ProductPicture output = _pictureConverter.ConvertToByteArray(image);
                output.ProductId = salesAd.ProdId;
                _pictureRepository.Add(output);
            }
            // Add the sales ad to the sales ad repository
            _salesAdRepository.Add(salesAd);
        }

        /// <summary>
        /// Retrieves a sales ad by its ID and product type, along with related user, product, and picture data.
        /// </summary>
        /// <param name="id">The ID of the sales ad to retrieve.</param>
        /// <param name="type">The product type associated with the sales ad.</param>
        /// <returns>The sales ad with its associated product, user, and pictures.</returns>
        public SalesAd Get(int id, ProductType type)
        {
            SalesAd output = _salesAdRepository.GetById(id);
            output.User = _accountRepository.GetById(output.UserId);
            output.Product = _productRepository.GetById(id);
            output.ProductPictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(id));
            return output;
        }

        /// <summary>
        /// Retrieves all sales ads posted by a specific user.
        /// </summary>
        /// <param name="userId">The user ID to filter sales ads by.</param>
        /// <returns>A list of sales ads posted by the specified user.</returns>
        public List<SalesAd> GetSalesAdsFromUser(int userId)
        {
            return _salesAdRepository.GetAllFromUser(userId);
        }

        /// <summary>
        /// Deletes a sales ad, its associated product, and pictures from the system.
        /// </summary>
        /// <param name="id">The ID of the sales ad to delete.</param>
        public void DeleteSalesAd(int id)
        {
            // Delete the associated product pictures, sales ad, and product from the repositories
            _pictureRepository.Delete(id);
            _salesAdRepository.Delete(id);
            _productRepository.Delete(id);
        }

        /// <summary>
        /// Retrieves all sales ads of a specific product type.
        /// </summary>
        /// <param name="type">The product type to filter sales ads by.</param>
        /// <returns>A list of sales ads that match the specified product type.</returns>
        public List<SalesAd> GetProductsOfType(ProductType type)
        {
            List<SalesAd> outputList = new List<SalesAd>();

            foreach (SalesAd output in _salesAdRepository.GetAllProductsOfType(type))
            {
                // Retrieve related product, user, and pictures for each sales ad
                output.Product = _productRepository.GetById(output.ProdId);
                output.User = _accountRepository.GetById(output.UserId);
                output.ProductPictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(output.ProdId));
                outputList.Add(output);
            }

            return outputList;
        }

        /// <summary>
        /// Retrieves a list of sales ads that match the specified search query.
        /// </summary>
        /// <param name="searchQuery">The search term used to filter sales ads by title, product type, brand, or model.</param>
        /// <returns>A list of sales ads that match the search query, including product, user, and picture information.</returns>
        public List<SalesAd> GetSearchResults(string searchQuery)
        {
            // Initialize lists for searching and storing output results
            List<SalesAd> listToSearch = new List<SalesAd>();
            List<SalesAd> outputList = new List<SalesAd>();

            // Populate the list to search with all sales ads, including product information
            foreach (SalesAd output in _salesAdRepository.GetAll())
            {
                output.Product = _productRepository.GetById(output.ProdId);
                listToSearch.Add(output);
            }

            // Filter the list based on the search query
            foreach (SalesAd output in listToSearch.FindAll(x => 
            x.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) || 
            x.Product.Type.GetDisplayName().Equals(searchQuery, StringComparison.OrdinalIgnoreCase)  ||
            x.Product.Brand.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
            x.Product.Model.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
            {
                // Populate additional data for the matched sales ads
                output.User = _accountRepository.GetById(output.UserId);
                output.ProductPictures = _pictureConverter.ByteArrayToBase64(_pictureRepository.GetAll(output.ProdId));
                outputList.Add(output);
            }

            return outputList; // Return the final list of matched sales ads
        }
    }
}