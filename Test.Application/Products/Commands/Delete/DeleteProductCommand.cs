using AutoMapper;
using MediatR;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;

namespace Test.Application.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest, IRequirePermission
    {
        public DeleteProductCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string RequiredPermission => PermissionEnum.Delete.ToString();
    }

    public class CreateProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == request.Id);
            if(product == null)
            {
                throw new ArgumentException("This product is not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
