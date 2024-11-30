using AutoMapper;
using CRUDApp.BaseResponse;
using CRUDApp.DTOs;
using CRUDApp.IRepository;
using CRUDApp.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController : BaseResponseHandler
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(string searchQuery = "")
    {
        var products = await _productRepository.GetAllProductsAsync(searchQuery);
        if (products == null || !products.Any())
            return CreateNotFoundResponse("No products found.");

        var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return CreateSuccessResponse(productDTOs, "Products retrieved successfully.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
            return CreateNotFoundResponse($"Product with ID {id} not found.");

        var productDTO = _mapper.Map<ProductDTO>(product);
        return CreateSuccessResponse(productDTO, "Product retrieved successfully.");
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateDTO productCreateDTO)
    {
        if (productCreateDTO == null)
            return CreateErrorResponse("Invalid product data.");

        var product = _mapper.Map<Product>(productCreateDTO);
        await _productRepository.AddProductAsync(product);

        return CreateSuccessResponse(product, "Product created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
    {
        if (!ModelState.IsValid)
            return CreateErrorResponse("Invalid product data.");

        var product = _mapper.Map<Product>(productUpdateDTO);
        product.Id = id;

        var isUpdated = await _productRepository.UpdateProductAsync(product);
        if (!isUpdated)
            return CreateNotFoundResponse($"Product with ID {id} not found.");

        return CreateSuccessResponse(product, "Product updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var isDeleted = await _productRepository.DeleteProductAsync(id);
        if (!isDeleted)
            return CreateNotFoundResponse($"Product with ID {id} not found.");
        return CreateSuccessResponse("Product deleted successfully.");
    }
 
}
