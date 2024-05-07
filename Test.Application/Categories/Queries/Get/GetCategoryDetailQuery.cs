using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;

namespace Test.Application.Categories.Queries.Get
{
    public class GetCategoryDetailQuery : IRequest<CategoryDetailDTO>, IRequirePermission
    {
        public GetCategoryDetailQuery(int id)
        {
            Id = id;
        }

        public int Id { get; init; }

        public string RequiredPermission => PermissionEnum.Get.ToString();
    }

    public class GetCategoryDetailQueryHandler : IRequestHandler<GetCategoryDetailQuery, CategoryDetailDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoryDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDetailDTO> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == request.Id);
            if (category == null)
            {
                throw new ArgumentException("This category is not found");
            }
            return _mapper.Map<CategoryDetailDTO>(category);
        }
    }
}
