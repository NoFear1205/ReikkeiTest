using AutoMapper;
using MediatR;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;
using Test.Domain.Entities;

namespace Test.Application.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<ProductDTO>, IRequirePermission
    {
        public CreateProductCommand(string name, decimal price, List<int> categoryIds)
        {
            Name = name;
            Price = price;
            CategoryIds = categoryIds;
        }

        public string Name { get; init; }
        public decimal Price { get; init; }
        public List<int> CategoryIds { get; init; } = new List<int>();

        public string RequiredPermission => PermissionEnum.Create.ToString();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CreateProductCommand, Product>()
                    .ForMember(m => m.ProductCategories, m => m.MapFrom(c => c.CategoryIds.Select(c => new ProductCategory(c))));
            }
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
