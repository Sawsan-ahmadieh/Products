using MediatR;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Command
{
    public class UpdateProductCommand:IRequest<Result>
    {
        public UpdateProductCommand(ProductDto productDto)
        {
            ProductDto = productDto;
        }

        public ProductDto ProductDto { get; }
    }
}
