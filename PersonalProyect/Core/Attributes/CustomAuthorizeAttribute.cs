using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Core.Attributes
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(string permission, string module) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { module, permission };

        }
    }

    public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly string _module;
        private readonly IUserService _userService;

        public CustomAuthorizeFilter(string module, string permission, IUserService userService)
        {
            _module = module;
            _permission = permission;
            _userService = userService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!_userService.CurrentUserIsAuthenticated())
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "Controller", "Account" },
                { "Action", "Login" },
                { "ReturnUrl", context.HttpContext.Request.Path }
            });
                return;
            }

            if (!await _userService.CurrentUserHasPermissionAsync(_permission, _module))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}