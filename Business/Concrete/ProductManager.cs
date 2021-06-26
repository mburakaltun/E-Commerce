using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> getAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> getAllByCategoryID(int ID)
        {
            return _productDal.GetAll(p=>p.CategoryID==ID);
        }

        public List<Product> getByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=>p.UnitPrice <= max && p.UnitPrice >= min);
        }

        public List<ProductDetailDTO> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
