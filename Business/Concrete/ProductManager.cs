﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryID),
                CheckIfProductNameIsExists(product.ProductName)
                );

            if(result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult();

        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryID),
                CheckIfProductNameIsExists(product.ProductName)
                );

            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult();
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 05) 
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryID(int ID)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryID==ID));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice <= max && p.UnitPrice >= min));
        }

        public IDataResult<List<ProductDetailDTO>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 02)
            {
                return new ErrorDataResult<List<ProductDetailDTO>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());
        }


        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryID == categoryId);
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductMaximumNumberOfCategoryReachedError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameIsExists(string name)
        {
            var result = _productDal.GetAll(p => p.ProductName == name).Any();
            if(result)
            {
                return new ErrorResult(Messages.ProductNameExistsError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryCountIsCorrect(int categoryId)
        {
            var count = _categoryService.GetAll().Data.Count;
            if (count >= 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }
            return new SuccessResult();

        }
    }
}
