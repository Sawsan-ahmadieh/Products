using MediatR;
using Products.backend.Models;
using Products.Shared.Response;

namespace Products.backend.Handler.Query
{
    public class GetProductsQuery:IRequest<Result>
    {
    }
}
