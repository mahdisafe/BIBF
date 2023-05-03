using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Specification;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
  
    public class ProductsController : BaseApiController
    {
        
        public readonly IMapper _mapper ;
        public IGenericRepository<Product> ProductRepo { get; set; }
        public IGenericRepository<ProductBrand> ProductBrandRepo { get; set; }
        public IGenericRepository<ProductType> ProducttypeRepo { get; set; }

        public ProductsController(IGenericRepository<Product> productRepo,
        IGenericRepository<ProductBrand> 
        productBrandRepo ,
        IGenericRepository<ProductType> producttypeRepo
        ,IMapper mapper)
        {
            this.ProducttypeRepo = producttypeRepo;
            _mapper = mapper;
            this.ProductBrandRepo = productBrandRepo;
            this.ProductRepo = productRepo;
           
        }

        [HttpGet]
       public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec =new ProductsWithTypesAndBrandSpecification();
            var product = await ProductRepo.ListAsync(spec);
            
            return  Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(product));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
             var spec =new ProductsWithTypesAndBrandSpecification(id);

            var product = await ProductRepo.GetEntityWithSpec(spec);
            return  _mapper.Map<Product,ProductToReturnDto>(product);
           
        }


       [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrand()
        {
            var productbrand = await ProductBrandRepo.ListAllAsync();
            return Ok(productbrand);
        }


       [HttpGet("Types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var producttypes = await ProducttypeRepo.ListAllAsync();
            return Ok(producttypes);
        }



    }
}