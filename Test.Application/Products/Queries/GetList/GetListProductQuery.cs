using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq.Expressions;
using Test.Application.Common.Behaviours;
using Test.Application.Common.Mapping;
using Test.Application.Common.Models;
using Test.Application.Interfaces;
using Test.Application.Products.Commands.Create;
using Test.Domain.Entities;
using Test.Domain.Extensions;

namespace Test.Application.Products.Queries.GetList
{
    public class GetListProductQuery : IRequest<PaginatedList<ProductDTO>>, IRequirePermission
    {
        public GetListProductQuery(string? categoryIds, int pageNumber, int pageSize) 
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            CategoryIds = categoryIds;
        }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? CategoryIds { get; init; }
        public ICollection<int>? Categories => CategoryIds?.Split(',').Select(id =>
        {
            if (int.TryParse(id, out var parsedId))
            {
                return parsedId;
            }
            else
            {
                throw new ArgumentException($"Invalid category ID: {id}");
            }
        }).ToList();

        public string RequiredPermission => PermissionEnum.Get.ToString();
    }

    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, PaginatedList<ProductDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetListProductQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProductDTO>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Product, bool>> whereClause = c => true;
            if(request.Categories != null && request.Categories.Any())
            {
                whereClause = whereClause.And(x => x.ProductCategories.Any(c => request.Categories.Contains(c.CategoryId)));
            }
            return await _context.Products
                .Where(whereClause)
                .OrderBy(x => x.Id)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
