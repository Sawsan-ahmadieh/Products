using MediatR;
using Products.backend.Models;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Command;

public class CreateProductCommand:IRequest<Result>
{
    public ProductDto product;

    public CreateProductCommand(ProductDto product)
    {
        this.product = product;
    }

    
}
