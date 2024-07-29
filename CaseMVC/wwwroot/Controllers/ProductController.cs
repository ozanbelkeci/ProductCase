using DAL.Entity;
using DAL.Operations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using EShopperAdminPanel.Models;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace EShopperAdminPanel.Controllers
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
                Price = i.Price,
                Stock = i.Stock,
                IsApproved = i.IsApproved,
                Photo = i.Photo,
                CategoryId = i.CategoryId
            }).ToList();

            productModel.ForEach(delegate(ProductModel product)
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
                Price = i.Price,
                Stock = i.Stock,
                IsApproved = i.IsApproved,
                Photo = i.Photo,
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

        public JsonResult CreateProduct(string _name, string _desc, string _price, int _stock, bool _isApproved, IFormFile _photo, int _categoryId)
        {
            try
            {
                double priceD = double.Parse(_price, CultureInfo.InvariantCulture);
                var product = new Product();
                if (_photo != null)
                {
                    var extension = Path.GetExtension(_photo.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productPhotos/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    _photo.CopyTo(stream);
                    product.Photo = newImageName;
                }

                product.Name = _name;
                product.Description = _desc;
                product.Price = priceD;
                product.IsApproved = _isApproved;
                product.Stock = _stock;
                product.CategoryId = _categoryId;

                GenericRepository<Product> repository = new GenericRepository<Product>();
                var result = repository.Create(product);

                var productModel = new ProductModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Price = result.Price,
                    CategoryId= _categoryId,
                    IsApproved= result.IsApproved,
                    Photo= result.Photo,
                    Stock= result.Stock,
                };

                return Json(productModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult UpdateProduct(int _id, string _name, string _desc, string _price, int _stock, bool _isApproved, IFormFile _photo, int _categoryId)
        {
            try
            {
                double priceD = double.Parse(_price, CultureInfo.InvariantCulture);
                var product = new Product();
                if (_photo != null)
                {
                    var extension = Path.GetExtension(_photo.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productPhotos/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    _photo.CopyTo(stream);
                    product.Photo = newImageName;
                }

                product.Id= _id;
                product.Name = _name;
                product.Description = _desc;
                product.Price = priceD;
                product.IsApproved = _isApproved;
                product.Stock = _stock;
                product.CategoryId = _categoryId;

                GenericRepository<Product> repository = new GenericRepository<Product>();
                var result = repository.Update(product);

                var productModel = new ProductModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    Price = result.Price,
                    CategoryId = _categoryId,
                    IsApproved = result.IsApproved,
                    Photo = result.Photo,
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
