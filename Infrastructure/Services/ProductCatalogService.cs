using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class ProductCatalogService(CategoryRepository categoryRepository, ManufacturerRepository manufacturerRepository, PriceRepository priceRepository, ProductRepository productRepository)
{
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    private readonly ManufacturerRepository _manufacturerRepository = manufacturerRepository;
    private readonly PriceRepository _priceRepository = priceRepository;
    private readonly ProductRepository _productRepository = productRepository;


    public bool CreateProduct(ProductEntity product)
    {
        try
        {
            if (!_productRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
            {
                var newProduct = new ProductEntity();

                var existingCategory = _categoryRepository.GetOne(x => x.CategoryName == product.Category.CategoryName);
                if (existingCategory == null)
                {
                    existingCategory = new CategoryEntity
                    {
                        CategoryName = product.Category.CategoryName
                    };
                    _categoryRepository.Create(existingCategory);
                }

                var existingManufacturer = _manufacturerRepository.GetOne(x => x.Manufacturer == product.Manufacturer.Manufacturer);
                if (existingManufacturer == null)
                {
                    existingManufacturer = new ManufacturerEntity
                    {
                        Manufacturer = product.Manufacturer.Manufacturer
                    };
                    _manufacturerRepository.Create(existingManufacturer);
                }

                var existingPrice = _priceRepository.GetOne(x => x.UnitPrice == product.Price.UnitPrice);
                if (existingPrice == null)
                {
                    existingPrice = new PriceEntity
                    {
                        UnitPrice = product.Price.UnitPrice
                    };
                    _priceRepository.Create(existingPrice);
                }

                newProduct.ArticleNumber = Guid.NewGuid().ToString();
                newProduct.Title = product.Title;
                newProduct.Description = product.Description;
                newProduct.CategoryId = existingCategory.Id;
                newProduct.ManufacturerId = existingManufacturer.Id;
                newProduct.PriceId = existingPrice.Id;

                _productRepository.Create(newProduct);

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public IEnumerable<ProductEntity> GetAllProducts()
    {
        try
        {
            var existingProducts = _productRepository.GetAll();
            if (existingProducts != null)
            {
                return existingProducts;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public ProductEntity GetOneProduct(string articleNumber)
    {
        try
        {
            var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
            if (existingProduct != null)
            {
                var existingCategory = _categoryRepository.GetOne(x => x.Id == existingProduct.CategoryId);
                if (existingCategory != null)
                {
                    var existingManufacturer = _manufacturerRepository.GetOne(x => x.Id == existingProduct.ManufacturerId);
                    if (existingManufacturer != null)
                    {
                        var existingPrice = _priceRepository.GetOne(x => x.Id == existingProduct.PriceId);
                        if (existingPrice != null)
                        {
                            return existingProduct;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }
    public bool UpdateProduct(ProductEntity product, string articleNumber)
    {
        try
        {
            var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
            if (existingProduct != null)
            {
                existingProduct.Title = product.Title;
                existingProduct.Description = product.Description;
                existingProduct.Price.UnitPrice = product.Price.UnitPrice;
                existingProduct.Category.CategoryName = product.Category.CategoryName;
                existingProduct.Manufacturer.Manufacturer = product.Manufacturer.Manufacturer;

                _productRepository.Update(x => x.ArticleNumber == articleNumber ,existingProduct);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public bool DeleteProduct(string articleNumber)
    {
        try
        {
            var existingProduct = _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
            if (existingProduct != null)
            {
                _productRepository.Delete(x => x.ArticleNumber == existingProduct.ArticleNumber);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}
