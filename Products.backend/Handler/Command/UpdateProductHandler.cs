using Mapster;
using MediatR;
using Products.backend.Models;
using Products.backend.Repo;
using Products.backend.Repo.IRepo;
using Products.Shared.Response;

namespace Products.backend.Handler.Command
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IProductRepo productRepo;
        private readonly ILogger<UpdateProductHandler> logger;

        public UpdateProductHandler(IProductRepo productRepo,ILogger<UpdateProductHandler> logger)
        {
            this.productRepo = productRepo;
            this.logger = logger;
        }
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var prod = request.ProductDto.Adapt<Product>();
            var errorUpdate = await productRepo.UpdateProduct(prod);
            if (errorUpdate != null) 
            {
                logger.LogError("Failed to update product {@product} Error Details {@error}",request.ProductDto, errorUpdate);
                Response.Failed(errorUpdate, errorUpdate.status); 
            }

            var result = await productRepo.SaveChangesAsync();
            if (result != 0)
                return Response.Failed(ErrorList<Product>.UpdateFailed(prod.Name),ErrorList<Product>.UpdateFailed(prod.Name).status);

            return Response.Success(request.ProductDto);
            
        }
    }
}
