using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.backend.Handler.Command;
using Products.backend.Handler.Query;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<Result> GetProducts()
        {
            GetProductsQuery getProductsQuery = new GetProductsQuery();
            var result = await mediator.Send(getProductsQuery);
            return result;
        }
        [HttpGet("id")]
        public async Task<Result> GetProductById(int id)
        {
            GetProductByIDQuery getProductByID = new GetProductByIDQuery(id);
            var result = await mediator.Send(getProductByID);
            return result;
        }
        [HttpPost]
        public async Task<Result> CreateProduct(ProductDto productDto)
        {

            CreateProductCommand createProductCommand = new CreateProductCommand(productDto);
            var result = await mediator.Send(createProductCommand);
            return result;
        }
        [HttpPut]
        public async Task<Result> UpdateProduct(ProductDto productDto)
        {
            UpdateProductCommand updateProductCommand = new UpdateProductCommand(productDto);
            var result  = await mediator.Send(updateProductCommand);
            return result;
        }
        [HttpDelete("id")]
        public async Task<Result> DeleteProduct(ProductDto productDto)
        {
            DeleteProductCommand deleteProductCommand = new DeleteProductCommand(productDto.ID);
            var result = await mediator.Send(deleteProductCommand);
            return result;
        }
    }
}
