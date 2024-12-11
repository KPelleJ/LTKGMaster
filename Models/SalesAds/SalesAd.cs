﻿using LTKGMaster.Models.Products;
using LTKGMaster.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace LTKGMaster.Models.SalesAds
{
    public class SalesAd
    {
        public int ProdId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public Product Product { get; set; }
        public IUser User { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }

        public SalesAd()
        {
        }
    }
}
