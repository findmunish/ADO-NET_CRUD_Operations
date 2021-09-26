using ADO_DotNet.Models.Entities;
using ADO_DotNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ADO_DotNet.WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetAll());
        }

        public IActionResult Details(int id)
        {
            var model = _service._getModelById(id, "DepartmentView", "View");
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View("DepartmentView", model);
        }

        public IActionResult Create()
        {
            return View("DepartmentView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                _service.Create(model);
                return RedirectToAction("Index");
            }

            return View("DepartmentView");
        }

        public IActionResult Update(int id)
        {
            var model = _service._getModelById(id, "DepartmentView", "Update");
            if (model == null)
            {
                return View("Index");
            }

            return View("DepartmentView", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Department model)
        {
            if (ModelState.IsValid)
            {
                _service.Update(id, model);
                return RedirectToAction("Index");
            }

            return View("DepartmentView");
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
