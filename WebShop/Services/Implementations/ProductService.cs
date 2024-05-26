﻿using AutoMapper;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
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
                throw new AlreadyExistsException($"Продукт {productDto.Title} уже существует");

            if (product.Subcategory == null)
                throw new NotFoundException($"Подкатегория {product.Subcategory.Id} не найдена");

            if (productDto.Image != null)
                product.ImageUrl = await _imageService.UploadPhoto(productDto.Image);

            return await _productRepository.AddAsync(product);
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            Product product = await _productRepository.GetAsync(productId);

            if (product == null)
                throw new NotFoundException($"Продукт {productId} не найден");

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<List<ProductR>> GetAllAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync();
            List<ProductR> productDtos = new List<ProductR>();

            foreach (Product product in products)
                productDtos.Add(await MapToDto(product));

            return productDtos;
        }

        public async Task<ProductR> GetAsync(int id)
        {
            Product productDto = await _productRepository.GetAsync(id);
            return await MapToDto(productDto);
        }

        public async Task<List<ProductR>> GetByCategoryAsync(int[] categoryId)
        {

            List<ProductR> productDtos = new List<ProductR>();

            foreach (var cat in categoryId)
            {
                Category category = await _categoryRepository.GetAsync(cat);

                if (category == null)
                    throw new NotFoundException($"Категория {categoryId} не найдена");

                List<Product> products = await _productRepository.GetByCategoryAsync(category);

                foreach (Product product in products)
                    productDtos.Add(await MapToDto(product));
            }

            return productDtos;
        }

        public async Task<List<ProductR>> GetBySubcategoryAsync(int[] subcategoryId)
        {
            List<ProductR> productDtos = new List<ProductR>();

            foreach (var subcat in subcategoryId)
            {
                Subcategory subcategory = await _subcategoryRepository.GetAsync(subcat);
                List<Product> products = await _productRepository.GetBySubcategoryAsync(subcategory);

                if (subcategory == null)
                    throw new NotFoundException($"Подкатегория {subcategoryId} не найдена");

                foreach (Product product in products)
                    productDtos.Add(await MapToDto(product));
            }

            return productDtos;
        }

        public async Task<List<ProductR>> GetLatest()
        {
            List<Product> products = await _productRepository.GetLatest();
            List<ProductR> productDtos = new List<ProductR>();

            foreach (var product in products)
                productDtos.Add((await MapToDto(product)));

            return productDtos;
        }

        public async Task<List<ProductR>> Search(string search)
        {
            var products = await _productRepository.Search(search);
            List<ProductR> productDtos = new List<ProductR>();

            foreach (Product product in products)
                productDtos.Add(await MapToDto(product));

            return productDtos;
        }

        public async Task<bool> UpdateAsync(ProductW productDto)
        {
            Product product = await MapFromDto(productDto);

            if (product.Subcategory == null)
                throw new NotFoundException($"Подкатегория {product.Subcategory.Id} не найдена");

            if (await _productRepository.IsExists(product))
                throw new AlreadyExistsException($"Продукт {productDto.Title} уже существует");

            if (productDto.Image != null)
                product.ImageUrl = await _imageService.UploadPhoto(productDto.Image);

            var currentProduct = await _productRepository.GetNoTrackAsync(productDto.Id);

            if (currentProduct == null)
                throw new NotFoundException($"Продукт {product.Id} не найден");

            product.ImageUrl = currentProduct.ImageUrl;

            return await _productRepository.UpdateAsync(product);
        }

        private async Task<Product> MapFromDto(ProductW productDto)
        {
            Product product = _mapper.Map<Product>(productDto);

            product.Subcategory = await _subcategoryRepository.GetAsync(productDto.SubcategoryId);

            foreach (var attribute in productDto.AttributeValues)
            {
                var attributeEntity = await _attributeRepository.GetAsync(attribute.Key);

                if (attributeEntity == null)
                    throw new NotFoundException($"Не удалось найти атрибут {attribute.Key}");

                if (!product.Subcategory.Category.Attributes.Any(a => a.Id == attribute.Key))
                    throw new NotFoundException($"В категории {product.Subcategory.Category.Title} нет атрибута {attribute.Key}");

                product.AttributeValues.Add(new AttributeValue()
                {
                    Product = product,
                    Attribute = attributeEntity,
                    Value = attribute.Value
                });
            }

            return product;
        }

        private async Task<ProductR> MapToDto(Product product)
        {
            ProductR productDto = _mapper.Map<ProductR>(product);

            List<AttributeValue> attributes = await _attributeRepository.GetValuesByProductAsync(product);
            foreach (var attribute in attributes)
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
