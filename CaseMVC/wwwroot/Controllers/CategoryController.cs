using DAL.Entity;
using DAL.Operations;
using EShopperAdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace EShopperAdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            GenericRepository<Category> repository = new GenericRepository<Category>();

            var categories = repository.GetList();

            List<CategoryModel> categoryModel = categories.Select(i => new CategoryModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                MainCategoryId = i.MainCategoryId,
                Line = i.Line,
            }
            ).ToList();


            return View(categoryModel);
        }

        public JsonResult UpdateTable()
        {
            GenericRepository<Category> repository = new GenericRepository<Category>();

            var categories = repository.GetList();

            List<CategoryModel> categoryModel = categories.Select(i => new CategoryModel()
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                MainCategoryId= i.MainCategoryId,
                Line = i.Line,
            }
            ).ToList();


            return Json(categoryModel);
        }

        public JsonResult GetCategoryById(int _id)
        {
            try
            {
                GenericRepository<Category> repository = new GenericRepository<Category>();
                var result = repository.GetItemById(_id);

                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult CreateCategory(string _categoryName, string _desc, int _mainCategoryId,int _line)
        {
            try
            {
                var category = new Category();
                category.Name = _categoryName;
                category.Description = _desc;
                category.MainCategoryId = _mainCategoryId;
                category.Line = _line;

                GenericRepository<Category> repository = new GenericRepository<Category>();
                var result = repository.Create(category);

                var categoryModel = new CategoryModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    MainCategoryId = result.MainCategoryId,
                    Line = result.Line
                };


                return Json(categoryModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult UpdateCategory(int _id, string _categoryName, string _desc, int _mainCategoryId, int _line)
        {
            try
            {
                var category = new Category();
                category.Id = _id;
                category.Name = _categoryName;
                category.Description = _desc;
                category.MainCategoryId = _mainCategoryId;
                category.Line = _line;

                GenericRepository<Category> repository = new GenericRepository<Category>();
                var result = repository.Update(category);

                var categoryModel = new CategoryModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    MainCategoryId = result.MainCategoryId,
                    Line = result.Line
                };


                return Json(categoryModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult DeleteCategory(int _id, int _mainCategoryId)
        {
            try
            {
                GenericRepository<Category> repository = new GenericRepository<Category>();
                if (_mainCategoryId == 0)
                {
                    var categoryList = repository.GetList();
                    var subCategories = categoryList.Where(x => x.MainCategoryId == _id).ToList();
                    foreach (var subCategory in subCategories)
                        repository.Delete(subCategory.Id);
                }

                var result = repository.Delete(_id);
                return Json(result);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

    }
}
