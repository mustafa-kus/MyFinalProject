﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

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
        [SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
               CheckIfProductNameExist(product.ProductName), CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;

            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 2)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            //iş kodları
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            else
            {
                return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
            }

        }
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExist(product.ProductName));
            if (result != null)
            {
                return result;

            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdate);

        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryid)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryid).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {

            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
