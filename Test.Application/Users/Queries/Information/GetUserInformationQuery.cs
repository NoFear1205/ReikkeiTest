using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Application.Common.Behaviours;
using Test.Application.Interfaces;

namespace Test.Application.Users.Queries.Information
{
    public class GetUserInformationQuery : IRequest<UserDTO>, IRequireRole
    {
        public GetUserInformationQuery()
        {
        }
        public string RequiredRole => "Reader";
    }

    public class GetUserInformationQueryHandler : IRequestHandler<GetUserInformationQuery, UserDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public GetUserInformationQueryHandler(IApplicationDbContext context, IUser user, IMapper mapper)
        {
            _context = context;
            _user = user;
            _mapper = mapper;
        }


        public async Task<UserDTO> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Include(c => c.UserRoles).ThenInclude(c => c.Role).FirstOrDefaultAsync(p => p.Email == _user.Id);
            if (user == null)
            {
                throw new ArgumentException("User is not found.");
            }
            return _mapper.Map<UserDTO>(user);
        }
    }
}
