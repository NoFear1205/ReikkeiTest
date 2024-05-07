using MediatR;
using System.Reflection;
using Test.Application.Interfaces;


namespace Test.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUser _user;
        private readonly IUserAuthorizationService _authorizationService;

        public AuthorizationBehaviour(
            IUser user, IUserAuthorizationService authorizationService)
        {
            _user = user;
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is IRequireRole)
            {
                var requiredRole = GetRequiredRole(request);

                // Check if the user has the required role
                if (_user.Roles != null && !_authorizationService.IsInRole(_user.Roles, requiredRole))
                {
                    throw new UnauthorizedAccessException("User does not have the required role to perform this action.");
                }
            }

            if (request is IRequirePermission)
            {
                var requiredPermission = GetRequiredPermission(request);

                // Check if the user has the required role
                if (_user.Roles != null && !_authorizationService.IsInPermission(_user.Permissions, requiredPermission))
                {
                    throw new UnauthorizedAccessException("User does not have the required role to perform this action.");
                }
            }


            // Call the next behavior in the pipeline
            return await next();
        }
        private string GetRequiredRole(TRequest request)
        {
            // Get the RequiredRole property value from the request
            var requiredRoleProperty = typeof(TRequest).GetTypeInfo().GetProperty("RequiredRole");
            if (requiredRoleProperty != null)
            {
                return requiredRoleProperty.GetValue(request) as string;
            }

            throw new InvalidOperationException($"Request of type {typeof(TRequest).Name} does not specify required role.");
        }
        private string GetRequiredPermission(TRequest request)
        {
            // Get the RequiredRole property value from the request
            var requiredRoleProperty = typeof(TRequest).GetTypeInfo().GetProperty("RequiredPermission");
            if (requiredRoleProperty != null)
            {
                return requiredRoleProperty.GetValue(request) as string;
            }

            throw new InvalidOperationException($"Request of type {typeof(TRequest).Name} does not specify Required Permission.");
        }
    }

    public interface IUserAuthorizationService
    {
        bool IsInRole(List<string> roles, string role);
        bool IsInPermission(List<string> roles, string role);
    }
    public class UserAuthorizationService : IUserAuthorizationService
    {
        public bool IsInRole(List<string> roles, string role)
        {
            return roles.Contains(role);
        }
        public bool IsInPermission(List<string> permissions, string permission)
        {
            return permissions.Contains(permission);
        }
    }
    public interface IRequireRole
    {
        string RequiredRole { get; }
    }
    public interface IRequirePermission
    {
        string RequiredPermission { get; }
    }

    public enum PermissionEnum
    {
        Create,
        Delete,
        Get,
        Edit
    }
}
