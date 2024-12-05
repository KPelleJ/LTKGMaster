﻿using LTKGMaster.Models.Products;

namespace LTKGMaster.Models.Repositories
{
    public interface ILaptopRepository
    {
        Product Add(Product product);
        void Delete(Product product);
        void Update(Product product);
        Product GetById(int id);
    }
}