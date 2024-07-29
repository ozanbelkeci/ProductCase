using DAL.Entity;
using DAL.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using CaseMVC.Models;
using System.Linq;

namespace CaseMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            GenericRepository<Product> repository = new GenericRepository<Product>();
            GenericRepository<Category> categoryRepo = new GenericRepository<Category>();

            var products = repository.GetList();

            List<ProductModel> productModel = products.Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Stock = i.Stock,
                CategoryId = i.CategoryId
            }).ToList();

            productModel.ForEach(delegate (ProductModel product)
            {
                CategoryModel categoryModel = new CategoryModel();
                var category = categoryRepo.GetItemById(product.CategoryId);
                categoryModel.Name = category.Name;
                categoryModel.Description = category.Description;

                product.Category = categoryModel;

            });


            return View(productModel);
        }

        public JsonResult UpdateTable()
        {
            GenericRepository<Product> repository = new GenericRepository<Product>();
            GenericRepository<Category> categoryRepo = new GenericRepository<Category>();

            var products = repository.GetList();

            List<ProductModel> productModel = products.Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Stock = i.Stock,
                CategoryId = i.CategoryId
            }).ToList();

            productModel.ForEach(delegate (ProductModel product)
            {
                CategoryModel categoryModel = new CategoryModel();
                var category = categoryRepo.GetItemById(product.CategoryId);
                categoryModel.Name = category.Name;
                categoryModel.Description = category.Description;

                product.Category = categoryModel;

            });


            return Json(productModel);
        }

        public JsonResult GetProductById(int _id)
        {
            try
            {
                GenericRepository<Product> repository = new GenericRepository<Product>();
                var result = repository.GetItemById(_id);

                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult CreateProduct(string _name, string _desc, int _stock, int _categoryId)
        {
            try
            {
                var product = new Product();
                product.Name = _name;
                product.Description = _desc;
                product.Stock = _stock;
                product.CategoryId = _categoryId;

                GenericRepository<Product> repository = new GenericRepository<Product>();
                var result = repository.Create(product);

                var productModel = new ProductModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    CategoryId = _categoryId,
                    Stock = result.Stock,
                };

                return Json(productModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult UpdateProduct(int _id, string _name, string _desc, int _stock, int _categoryId)
        {
            try
            {
                var product = new Product();
                product.Id = _id;
                product.Name = _name;
                product.Description = _desc;
                product.Stock = _stock;
                product.CategoryId = _categoryId;

                GenericRepository<Product> repository = new GenericRepository<Product>();
                var result = repository.Update(product);

                var productModel = new ProductModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    CategoryId = _categoryId,
                    Stock = result.Stock,
                };

                return Json(productModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult DeleteProduct(int _id)
        {
            try
            {
                GenericRepository<Product> repository = new GenericRepository<Product>();
                var result = repository.Delete(_id);

                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult GetCategories()
        {
            try
            {
                GenericRepository<Category> repository = new GenericRepository<Category>();
                var result = repository.GetList();

                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
