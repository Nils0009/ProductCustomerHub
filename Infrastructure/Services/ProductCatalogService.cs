using Infrastructure.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductCatalogService
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ManufacturerRepository _manufacturerRepository;
        private readonly PriceRepository _priceRepository;
        private readonly ProductRepository _productRepository;

        public ProductCatalogService(CategoryRepository categoryRepository, ManufacturerRepository manufacturerRepository, PriceRepository priceRepository, ProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
            _priceRepository = priceRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> CreateProduct(ProductEntity product)
        {
            try
            {
                if (!await _productRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
                {
                    var newProduct = new ProductEntity();

                    var existingCategory = await _categoryRepository.GetOne(x => x.CategoryName == product.Category.CategoryName);
                    if (existingCategory == null)
                    {
                        existingCategory = new CategoryEntity
                        {
                            CategoryName = product.Category.CategoryName
                        };
                        await _categoryRepository.Create(existingCategory);
                    }

                    var existingManufacturer = await _manufacturerRepository.GetOne(x => x.Manufacturer == product.Manufacturer.Manufacturer);
                    if (existingManufacturer == null)
                    {
                        existingManufacturer = new ManufacturerEntity
                        {
                            Manufacturer = product.Manufacturer.Manufacturer
                        };
                        await _manufacturerRepository.Create(existingManufacturer);
                    }

                    var existingPrice = await _priceRepository.GetOne(x => x.UnitPrice == product.Price.UnitPrice);
                    if (existingPrice == null)
                    {
                        existingPrice = new PriceEntity
                        {
                            UnitPrice = product.Price.UnitPrice
                        };
                        await _priceRepository.Create(existingPrice);
                    }

                    newProduct.ArticleNumber = Guid.NewGuid().ToString();
                    newProduct.Title = product.Title;
                    newProduct.Description = product.Description;
                    newProduct.CategoryId = existingCategory.Id;
                    newProduct.ManufacturerId = existingManufacturer.Id;
                    newProduct.PriceId = existingPrice.Id;

                    await _productRepository.Create(newProduct);

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

        public async Task<IEnumerable<ProductEntity>> GetAllProducts()
        {
            try
            {
                var existingProducts = await _productRepository.GetAll();
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

        public async Task<ProductEntity> GetOneProduct(string articleNumber)
        {
            try
            {
                var existingProduct = await _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
                if (existingProduct != null)
                {
                    var existingCategory = await _categoryRepository.GetOne(x => x.Id == existingProduct.CategoryId);
                    if (existingCategory != null)
                    {
                        var existingManufacturer = await _manufacturerRepository.GetOne(x => x.Id == existingProduct.ManufacturerId);
                        if (existingManufacturer != null)
                        {
                            var existingPrice = await _priceRepository.GetOne(x => x.Id == existingProduct.PriceId);
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

        public async Task<bool> UpdateProduct(ProductEntity product, string articleNumber)
        {
            try
            {
                var existingProduct = await _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
                if (existingProduct != null)
                {
                    existingProduct.Title = product.Title;
                    existingProduct.Description = product.Description;
                    existingProduct.Price.UnitPrice = product.Price.UnitPrice;
                    existingProduct.Category.CategoryName = product.Category.CategoryName;
                    existingProduct.Manufacturer.Manufacturer = product.Manufacturer.Manufacturer;

                    await _productRepository.Update(x => x.ArticleNumber == articleNumber, existingProduct);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteProduct(string articleNumber)
        {
            try
            {
                var existingProduct = await _productRepository.GetOne(x => x.ArticleNumber == articleNumber);
                if (existingProduct != null)
                {
                    await _productRepository.Delete(x => x.ArticleNumber == existingProduct.ArticleNumber);
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
}