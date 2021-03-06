using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductType> productsTypesRepo, IGenericRepository<ProductBrand> productsBrandsRepo, IMapper mapper)
        {
            _productRepo = productsRepo;
            _productTypeRepo = productsTypesRepo;
            _productBrandRepo = productsBrandsRepo;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams ProductParams)
        {
            var spec = new ProductsWithTypesandBrandsSpecification(ProductParams);

            var countSpec = new ProductWithFiltersForCountSpecification(ProductParams);

            var totalItems = await _productRepo.CountAsync(countSpec);

            var products =  await _productRepo.ListAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>
                (ProductParams.PageIndex, ProductParams.PageSize, totalItems, data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesandBrandsSpecification(id);

            var product = await _productRepo.GetEntityWithSpec(spec);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductsBrands()
        {
            var ProductsBrands = await _productBrandRepo.ListAllAsync();

            return Ok(ProductsBrands);
        }

        
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductsTypes()
        {
            var ProductsTypes = await _productTypeRepo.ListAllAsync();

            return Ok(ProductsTypes);
        }

    }
}
