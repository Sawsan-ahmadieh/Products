using MediatR;
using Products.Shared.Response;

namespace Products.backend.Handler.Command
{
    public class DeleteProductCommand:IRequest<Result>
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
