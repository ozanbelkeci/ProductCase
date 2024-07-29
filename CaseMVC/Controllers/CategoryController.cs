using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using DAL.Operations;
using DAL.Entity;
using System.Linq;
using CaseMVC.Models;

namespace CaseMVC.Controllers
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
                Description = i.Description
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
                Description = i.Description
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

        public JsonResult CreateCategory(string _categoryName, string _desc)
        {
            try
            {
                var category = new Category();
                category.Name = _categoryName;
                category.Description = _desc;


                GenericRepository<Category> repository = new GenericRepository<Category>();
                var result = repository.Create(category);

                var categoryModel = new CategoryModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description
                };


                return Json(categoryModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult UpdateCategory(int _id, string _categoryName, string _desc)
        {
            try
            {
                var category = new Category();
                category.Id = _id;
                category.Name = _categoryName;
                category.Description = _desc;


                GenericRepository<Category> repository = new GenericRepository<Category>();
                var result = repository.Update(category);

                var categoryModel = new CategoryModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description
                };


                return Json(categoryModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult DeleteCategory(int _id)
        {
            try
            {
                GenericRepository<Category> repository = new GenericRepository<Category>();
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
