using AutoMapper;
using CRUDApp.BaseResponse;
using CRUDApp.DTOs;
using CRUDApp.DTOs.Category.CRUDApp.DTOs;
using CRUDApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : BaseResponseHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        if (categories == null || !categories.Any())
            return CreateNotFoundResponse("No categories found");
        var categoryDTOs = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        return CreateSuccessResponse(categories, "Categories retrieved successfully");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);
        if (category == null)
            return CreateNotFoundResponse($"Category with ID {id} not found");
        return CreateSuccessResponse(category, "Category retrieved successfully");
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryCreateDTO categoryCreateDTO)
    {
        if (categoryCreateDTO == null)
            return CreateErrorResponse("Invalid category data");
        var category = _mapper.Map<Category>(categoryCreateDTO);
        await _categoryRepository.AddCategoryAsync(category);
        return CreateSuccessResponse(category, "Category created successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
    {
        if (categoryUpdateDTO == null)
            return CreateErrorResponse("Invalid category data");

        var category = _mapper.Map<Category>(categoryUpdateDTO);
        await _categoryRepository.AddCategoryAsync(category);
        if (category == null || id != category.Id)
        {
            return CreateErrorResponse("Invalid category data");
        }

        var isUpdated = await _categoryRepository.UpdateCategoryAsync(category);
        if (!isUpdated)
        {
            return CreateNotFoundResponse( "Category not found or update failed" );
        }
        return CreateSuccessResponse(category, "Category updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var isDeleted = await _categoryRepository.DeleteCategoryAsync(id);
        if (!isDeleted)
        {
            return CreateNotFoundResponse("Category not found or delete failed" );
        }
        return CreateSuccessResponse("Category deleted successfully");
    }
     
}
