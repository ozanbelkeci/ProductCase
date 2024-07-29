using DAL.Entity;
using DAL.Operations;
using EShopperAdminPanel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EShopperAdminPanel.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            GenericRepository<Page> repository = new GenericRepository<Page>();

            var products = repository.GetList();

            List<PageModel> pageModel = products.Select(i => new PageModel()
            {
                Name = i.Name,
                Description = i.Description,
                PageHtml = i.PageHtml
            }).ToList();
            return View(pageModel);
        }

        public IActionResult CreateLandingPage(string viewName, string pageHtml)
        {
            var cshtmlcontent = string.Format(@"@{Layout = ""~/Views/Shared/_Layout.cshtml"";ViewData[""{0}""] = ""Page Title"";} {0}",pageHtml);
            System.IO.File.WriteAllText(@"EShoppier\EShopperMVC\Views\Home\" + viewName + ".cshtml", cshtmlcontent);
            return Json(new { redirectToUrl = Url.Action(viewName) });
        }
    }
}
