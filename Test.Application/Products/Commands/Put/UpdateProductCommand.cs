using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;
using Test.Application.Products.Commands.Create;
using Test.Domain.Entities;

namespace Test.Application.Products.Commands.Put
{
    public class UpdateProductCommand : IRequest<ProductDTO>, IRequirePermission
    {
        public UpdateProductCommand( string name, decimal price, List<int> categoryIds)
        {
            Name = name;
            Price = price;
            CategoryIds = categoryIds;
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();

        public string RequiredPermission => PermissionEnum.Edit.ToString();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UpdateProductCommand, Product>()
                    .ForMember(m => m.ProductCategories, m => m.MapFrom(c => c.CategoryIds.Select(c => new ProductCategory(c))));
            }
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.Include(c => c.ProductCategories).FirstOrDefault(c => c.Id == request.Id);
            if(product == null)
            {
                throw new ArgumentException("This Product is not found");
            }
            product.Modify(request.Name, request.Price, request.CategoryIds);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductDTO>(product);
        }
    }
}
