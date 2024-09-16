using Mapster;
using MediatR;
using Products.backend.Models;
using Products.backend.Repo.IRepo;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Query;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result>
{
    private readonly IProductRepo productRepo;
    private readonly ILogger<GetProductsQueryHandler> logger;

    public GetProductsQueryHandler(IProductRepo productRepo,ILogger<GetProductsQueryHandler> logger)
    {
        this.productRepo = productRepo;
        this.logger = logger;
    }
    public async Task<Result> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {       
        var products = await productRepo.GetProducts();        
        if(products == null)  
        {
            logger.LogInformation("no products available");
            return Response.Success(Enumerable.Empty<Result>()); 
        }
        List<ProductDto> productdtos = products.Adapt<List<ProductDto>>();
        logger.LogInformation("Return {Product.Counts} products", productdtos.Count());

        return Response.Success(productdtos);
    }
}
