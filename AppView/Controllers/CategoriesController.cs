using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AppDB.Models.DtoAndViewModels.CategoriesService.ViewModels;
using AppDB.Models.DtoAndViewModels.CategoriesService.Dto;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(string categoryName = "", string description = "", int pageIndex = 1, int pageSize = 10)
    {
        var search = new CategoriesSearch
        {
            CategoryName = categoryName,
            Description = description,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
        var categories = await _categoryService.GetCategoriesAsync(search);
        ViewBag.Search = search;
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoriesCreateVM model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _categoryService.CreateCategoryAsync(model);
        if (result)
        {
            TempData["ToastMessage"] = "Tạo danh mục thành công!";
            TempData["ToastType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        TempData["ToastMessage"] = "Tạo danh mục thất bại!";
        TempData["ToastType"] = "danger";
        return View(model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            TempData["ToastMessage"] = "Không tìm thấy danh mục!";
            TempData["ToastType"] = "danger";
            return RedirectToAction(nameof(Index));
        }
        var updateModel = new CategoriesUpdateVM
        {
            Id = category.id,
            CategoryName = category.CategoryName,
            Description = category.Description
        };
        return View(updateModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CategoriesUpdateVM model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _categoryService.UpdateCategoryAsync(model);
        if (result)
        {
            TempData["ToastMessage"] = "Cập nhật danh mục thành công!";
            TempData["ToastType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        TempData["ToastMessage"] = "Cập nhật danh mục thất bại!";
        TempData["ToastType"] = "danger";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (result)
        {
            TempData["ToastMessage"] = "Xóa danh mục thành công!";
            TempData["ToastType"] = "success";
        }
        else
        {
            TempData["ToastMessage"] = "Xóa danh mục thất bại!";
            TempData["ToastType"] = "danger";
        }
        return RedirectToAction(nameof(Index));
    }
}