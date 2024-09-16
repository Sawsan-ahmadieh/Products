using Mapster;
using MediatR;
using Products.backend.Models;
using Products.backend.Repo;
using Products.backend.Repo.IRepo;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Command;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepo productRepo;
    private readonly ILogger<DeleteProductHandler> logger;

    public DeleteProductHandler(IProductRepo productRepo,ILogger<DeleteProductHandler> logger)
    {
        this.productRepo = productRepo;
        this.logger = logger;
    }
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var prod = (await productRepo.GetProducts()).FirstOrDefault(c => c.ID == request.Id);

        ProductDto productDto = prod.Adapt<ProductDto>();

        var ErrorDelete = await productRepo.DeleteProduct(request.Id);

        if(ErrorDelete != null)
        {
            logger.LogError("Failed to Create new product {@Error}", ErrorDelete);
            return Response.Failed(ErrorDelete, ErrorDelete.status);
        }

        var result = await productRepo.SaveChangesAsync();
        if (result != 0)
        {
            logger.LogError("Failed to Create new product {@product} Error Details {@Error}",productDto, ErrorDelete);
            Response.Failed(ErrorList<Product>.DeleteFailed(request.Id), ErrorList<Product>.DeleteFailed(request.Id).status); 
        }

        logger.LogInformation("Product {@product} deleted successfully", productDto);
        return Response.Success(productDto);

    }
}
