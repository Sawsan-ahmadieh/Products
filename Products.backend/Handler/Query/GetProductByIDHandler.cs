using Mapster;
using MediatR;
using Products.backend.Models;
using Products.backend.Repo.IRepo;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Query
{
    public class GetProductByIDHandler : IRequestHandler<GetProductByIDQuery, Result>
    {
        private readonly IProductRepo productRepo;

        public GetProductByIDHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<Result> Handle(GetProductByIDQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepo.GetProductById(request.Id);
            if (product == null) return Response.Failed(ErrorList<Product>.NotFound(request.Id), ErrorList<Product>.NotFound(request.Id).status);

            ProductDto productdto = product.Adapt<ProductDto>();
            return Response.Success(productdto);
        }
    }
}
