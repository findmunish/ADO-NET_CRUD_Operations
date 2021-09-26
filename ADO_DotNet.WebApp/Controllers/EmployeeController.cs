using ADO_DotNet.Models.Entities;
using ADO_DotNet.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace ADO_DotNet.WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _service;
        private IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public IActionResult Details(int id)
        {
            var model = _service._getModelById(id, "EmployeeView", "View");
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View("EmployeeView", model);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _service.GetDepartments();
            return View("EmployeeView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {
            if (model != null)
            {
                await UploadImage(model.Upload);

                //string wwwRootPath = _environment.WebRootPath;
                model.ImagePath = model.Upload.FileName; // Path.Combine(wwwRootPath + "/Upload/", model.Upload.FileName);

                _service.Create(model);
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _service.GetDepartments();
            return View("EmployeeView");
        }

        public IActionResult Update(int id)
        {
            var model = _service._getModelById(id, "EmployeeView", "Update");
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _service.GetDepartments();
            return View("EmployeeView", model);
        }

        public async Task UploadImage(IFormFile Upload)
        {
            string imageUploadPath = "Upload";
            string wwwRootPath = _environment.WebRootPath;

            if (!Directory.Exists(wwwRootPath + $"/{imageUploadPath}"))
            {
                Directory.CreateDirectory(wwwRootPath + $"/{imageUploadPath}");
            }

            string path = Path.Combine(wwwRootPath + $"/{imageUploadPath}/", Upload.FileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Employee model)
        {
            if (model != null)
            {
                await UploadImage(model.Upload);

                //string wwwRootPath = _environment.WebRootPath;
                model.ImagePath = model.Upload.FileName; // Path.Combine(wwwRootPath + "/Upload/", model.Upload.FileName);

                _service.Update(id, model);
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _service.GetDepartments();
            return View("EmployeeView");
        }

        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = _service.GetById(id);
            if (model == null)
            {
                return NotFound();
            }

            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}