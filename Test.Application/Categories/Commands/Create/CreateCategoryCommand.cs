using AutoMapper;
using MediatR;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;
using Test.Domain.Entities;

namespace Test.Application.Categories.Commands.Create
{
    public class CreateCategoryCommand : IRequest<CategoryDTO>, IRequirePermission
    {
        public CreateCategoryCommand(string name)
        {
            Name = name;
        }

        public string Name { get; init; }

        public string RequiredPermission => PermissionEnum.Create.ToString();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateCategoryCommand, Category>();
            }
        }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CategoryDTO>(category);
        }
    }
}
