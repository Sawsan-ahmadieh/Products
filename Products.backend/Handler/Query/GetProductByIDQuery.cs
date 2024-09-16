using MediatR;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Query
{
    public class GetProductByIDQuery:IRequest<Result>
    {
        public GetProductByIDQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
