using Mapster;
using MediatR;
using Products.backend.Models;
using Products.backend.Repo;
using Products.backend.Repo.IRepo;
using Products.Shared;
using Products.Shared.Response;

namespace Products.backend.Handler.Command
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IProductRepo productRepo;
        private readonly ICategoryRepo categoryRepo;
        private readonly ILogger<CreateProductHandler> logger;

        public CreateProductHandler(IProductRepo productRepo,
                                    ICategoryRepo categoryRepo,
                                    ILogger<CreateProductHandler> logger)
        {
            this.productRepo = productRepo;
            this.categoryRepo = categoryRepo;
            this.logger = logger;
        }
        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var Cat = await categoryRepo.FindCategory(request.product.CategoryId, request.product.CategoryName);

            var prod = request.product.Adapt<Product>();
            prod.CategoryId = Cat.CategoryId;
            var errorCreate = await productRepo.CreateProduct(prod);
            if (errorCreate != null)
            {
                logger.LogError("Failed to Create new product {@product} Erro details {@error} ", request.product, errorCreate);
                return Response.Failed(errorCreate, errorCreate.status);
            }
            var result = await productRepo.SaveChangesAsync();
            if (result == 0)
                return Response.Failed(ErrorList<Product>.CreationFailed(request.product.Name), ErrorList<Product>.CreationFailed(request.product.Name).status);

            logger.LogInformation("product {@product} created successfully", request.product);
            return Response.Success(request.product);
        }
    }
}
