using MediatR;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;

namespace Test.Application.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest, IRequirePermission
    {
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string RequiredPermission => PermissionEnum.Delete.ToString();
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == request.Id);
            if (category == null)
            {
                throw new ArgumentException("This category is not found");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
