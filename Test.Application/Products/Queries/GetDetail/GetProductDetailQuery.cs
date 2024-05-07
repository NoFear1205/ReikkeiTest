using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;

namespace Test.Application.Products.Queries.Get
{
    public class GetProductDetailQuery : IRequest<ProductDetailDTO>, IRequirePermission
    {
        public GetProductDetailQuery(int id)
        {
            Id = id;
        }

        public int Id { get; init; }
        public string RequiredPermission => PermissionEnum.Get.ToString();
    }

    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailDTO> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var product = _context.Products.Include(c => c.ProductCategories).ThenInclude(c => c.Category).AsNoTracking().FirstOrDefault(c => c.Id == request.Id);
            if(product == null)
            {
                throw new ArgumentException("This Product is not found");
            }
            return _mapper.Map<ProductDetailDTO>(product);
        }
    }
}
