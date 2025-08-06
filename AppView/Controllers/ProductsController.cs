using AppDB.Models.DtoAndViewModels.ProductsService.Dto;
using AppDB.Models.DtoAndViewModels.ProductsService.ViewModels;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var response = await _productsService.GetAllAsync();

            if (response.Success)
            {
                return View(response.Data);
            }

            TempData["ErrorMessage"] = response.Message;
            return View(new List<ProductsDto>());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _productsService.GetByIdAsync(id);

            if (response.Success)
            {
                return View(response.Data);
            }

            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            // Load categories for dropdown
            var categoriesResponse = await _categoriesService.GetAllAsync();
            ViewBag.Categories = categoriesResponse.Success ? categoriesResponse.Data : new List<AppDB.Models.DtoAndViewModels.CategoriesService.Dto.CategoriesDto>();

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productsService.CreateAsync(model);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Product created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = response.Message;
            }

            // Reload categories for dropdown
            var categoriesResponse = await _categoriesService.GetAllAsync();
            ViewBag.Categories = categoriesResponse.Success ? categoriesResponse.Data : new List<AppDB.Models.DtoAndViewModels.CategoriesService.Dto.CategoriesDto>();

            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _productsService.GetByIdAsync(id);

            if (response.Success)
            {
                // Load categories for dropdown
                var categoriesResponse = await _categoriesService.GetAllAsync();
                ViewBag.Categories = categoriesResponse.Success ? categoriesResponse.Data : new List<AppDB.Models.DtoAndViewModels.CategoriesService.Dto.CategoriesDto>();

                var updateModel = new ProductsUpdateVM
                {
                    ProductName = response.Data.ProductName,
                    Description = response.Data.Description,
                    Price = response.Data.Price,
                    CategoryID = response.Data.CategoryID
                };

                return View(updateModel);
            }

            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductsUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productsService.UpdateAsync(id, model);

                if (response.Success)
                {
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = response.Message;
            }

            // Reload categories for dropdown
            var categoriesResponse = await _categoriesService.GetAllAsync();
            ViewBag.Categories = categoriesResponse.Success ? categoriesResponse.Data : new List<AppDB.Models.DtoAndViewModels.CategoriesService.Dto.CategoriesDto>();

            return View(model);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var response = await _productsService.DeleteAsync(id);

            if (response.Success)
            {
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Search
        public async Task<IActionResult> Search(ProductsSearch searchModel)
        {
            var response = await _productsService.SearchAsync(searchModel);

            if (response.Success)
            {
                return View("Index", response.Data);
            }

            TempData["ErrorMessage"] = response.Message;
            return View("Index", new List<ProductsDto>());
        }
    }
}