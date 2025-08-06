using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var response = await _categoriesService.GetAllAsync();

            if (response.Success)
            {
                return View(response.Data);
            }

            TempData["ErrorMessage"] = response.Message;
            return View(new List<CategoriesDto>());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _categoriesService.GetByIdAsync(id);

            if (response.Success)
            {
                return View(response.Data);
            }

            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoriesService.CreateAsync(model);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Category created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = response.Message;
            }

            return View(model);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _categoriesService.GetByIdAsync(id);

            if (response.Success)
            {
                var updateModel = new CategoriesUpdateVM
                {
                    Id = response.Data.id,
                    CategoryName = response.Data.CategoryName,
                    Description = response.Data.Description
                };

                return View(updateModel);
            }

            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CategoriesUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _categoriesService.UpdateAsync(id, model);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Category updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = response.Message;
            }

            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _categoriesService.DeleteAsync(id);

            if (response.Success)
            {
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Search
        public async Task<IActionResult> Search(CategoriesSearch searchModel)
        {
            var response = await _categoriesService.SearchAsync(searchModel);

            if (response.Success)
            {
                return View("Index", response.Data);
            }

            TempData["ErrorMessage"] = response.Message;
            return View("Index", new List<CategoriesDto>());
        }
    }
}