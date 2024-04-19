using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Repositories.Implementations;
using WebShop.Repositories.Interfaces;
using WebShop.Services.ImageService;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IImageService _imageService;

        public ProductService
            (
            IProductRepository productRepository,
            ISubcategoryRepository subcategoryRepository,
            ICategoryRepository categoryRepository,
            IAttributeRepository attributeRepository,
            IImageService imageService,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _subcategoryRepository = subcategoryRepository;
            _categoryRepository = categoryRepository;
            _attributeRepository = attributeRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ProductW productDto)
        {
            Product product = await MapFromDto(productDto);
            if (await _productRepository.IsExists(product))
            {
                throw new AlreadyExistsException($"Продукт {productDto.Title} уже существует");
            }
            product.ImageUrl = await _imageService.UploadPhoto(productDto.Image);
            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(productId);
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<List<ProductR>> GetAllAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync();
            List<ProductR> productDtos = new List<ProductR>();

            foreach (Product product in products)
            {
                productDtos.Add(await MapToDto(product));
            }

            return productDtos;
        }

        public async Task<ProductR> GetAsync(int id)
        {
            ProductR productDto = _mapper.Map<ProductR>(await _productRepository.GetAsync(id));
            return productDto;
        }

        public async Task<List<ProductR>> GetByCategoryAsync(int categoryId)
        {
            Category category = await _categoryRepository.GetAsync(categoryId);
            List<Product> products = await _productRepository.GetByCategoryAsync(category);
            List<ProductR> productDtos = new List<ProductR>();

            foreach (Product product in products)
            {
                productDtos.Add(await MapToDto(product));
            }

            return productDtos;
        }

        public async Task<List<ProductR>> GetBySubcategoryAsync(int subcategoryId)
        {
            Subcategory subcategory = await _subcategoryRepository.GetAsync(subcategoryId);
            List<Product> products = await _productRepository.GetBySubcategoryAsync(subcategory);
            List<ProductR> productDtos = new List<ProductR>();

            foreach (Product product in products)
            {
                productDtos.Add(await MapToDto(product));
            }

            return productDtos;
        }

        public async Task<bool> UpdateAsync(ProductW productDto)
        {
            Product product = await MapFromDto(productDto);
            if (await _productRepository.IsExists(product))
            {
                throw new AlreadyExistsException($"Продукт {productDto.Title} уже существует");
            }
            product.ImageUrl = await _imageService.UploadPhoto(productDto.Image);
            return await _productRepository.UpdateAsync(product);
        }

        private async Task<Product> MapFromDto(ProductW productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            product.Subcategory = await _subcategoryRepository.GetAsync(productDto.SubcategoryId);

            foreach(var attribute in productDto.AttributeValues)
            {
                product.AttributeValues.Add(new AttributeValue()
                {
                    Product = product,
                    Attribute = await _attributeRepository.GetAsync(attribute.Key),
                    Value = attribute.Value
                });
            }

            return product;
        }

        private async Task<ProductR> MapToDto(Product product)
        {
            ProductR productDto = _mapper.Map<ProductR>(product);

            List<AttributeValue> attributes = await _attributeRepository.GetValuesByProductAsync(product);
            foreach(var attribute in attributes)
            {
                productDto.Attributes.Add(new AttributeItem
                {
                    Attribute = _mapper.Map<AttributeR>(attribute.Attribute),
                    Value = attribute.Value
                });
            }
            return productDto;
        }
    }
}
