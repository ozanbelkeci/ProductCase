using DAL.Entity;
using DAL.Operations;
using EShopperAdminPanel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EShopperAdminPanel.Controllers
{
    public class SliderController : Controller
    {
        public IActionResult Index()
        {
            GenericRepository<Slider> repository = new GenericRepository<Slider>();

            var sliders = repository.GetList();

            List<SliderModel> sliderModel = sliders.Select(i => new SliderModel()
            {
                Id = i.Id,
                Name = i.Name,
                Photo = i.Photo
            }
            ).ToList();


            return View(sliderModel);
        }

        public JsonResult GetSliders()
        {
            GenericRepository<Slider> repository = new GenericRepository<Slider>();

            var sliders = repository.GetList();

            List<SliderModel> sliderModel = sliders.Select(i => new SliderModel()
            {
                Id = i.Id,
                Name = i.Name,
                Photo = i.Photo
            }
            ).ToList();


            return Json(sliderModel);
        }

        public JsonResult CreateSlider(string _sliderName, IFormFile _sliderPhoto)
        {
            try
            {
                var slider = new Slider();
                slider.Name = _sliderName;

                if (_sliderPhoto != null)
                {
                    var extension = Path.GetExtension(_sliderPhoto.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sliderPhotos/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    _sliderPhoto.CopyTo(stream);
                    slider.Photo = newImageName;
                }

                GenericRepository<Slider> repository = new GenericRepository<Slider>();
                var result = repository.Create(slider);

                var sliderModel = new SliderModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Photo = result.Photo
                };

                return Json(sliderModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult UpdateSlider(int _id, string _sliderName, IFormFile _sliderPhoto, string _oldPhotoName = null)
        {
            try
            {
                var slider = new Slider();
                slider.Id = _id;
                slider.Name = _sliderName;

                if (_sliderPhoto != null)
                {
                    var extension = Path.GetExtension(_sliderPhoto.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/sliderPhotos/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    _sliderPhoto.CopyTo(stream);
                    slider.Photo = newImageName;
                }
                else
                    slider.Photo = _oldPhotoName;

                GenericRepository<Slider> repository = new GenericRepository<Slider>();
                var result = repository.Update(slider);

                var sliderModel = new SliderModel()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Photo= result.Photo
                };


                return Json(sliderModel);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public JsonResult DeleteSlider(int _id)
        {
            try
            {
                GenericRepository<Slider> repository = new GenericRepository<Slider>();
                var result = repository.Delete(_id);

                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public JsonResult GetSliderById(int _id)
        {
            try
            {
                GenericRepository<Slider> repository = new GenericRepository<Slider>();
                var result = repository.GetItemById(_id);

                return Json(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
