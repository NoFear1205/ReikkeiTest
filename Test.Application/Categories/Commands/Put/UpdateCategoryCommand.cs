using AutoMapper;
using MediatR;
using Test.Application.Categories.Commands.Create;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;
using Test.Domain.Entities;

namespace Test.Application.Categories.Commands.Put
{
    public class UpdateCategoryCommand : IRequest<CategoryDTO>, IRequirePermission
    {
        public UpdateCategoryCommand(string name)
        {
            Name = name;
        }
        public int? Id { get; set; }
        public string Name { get; set; }

        public string RequiredPermission => PermissionEnum.Edit.ToString();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateCategoryCommand, Category>();
            }
        }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == request.Id);
            if (category == null)
            {
                throw new ArgumentException("This category is not found");
            }
            category.Modify(request.Name);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
