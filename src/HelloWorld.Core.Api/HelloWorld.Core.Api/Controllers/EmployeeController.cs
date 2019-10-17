using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.ApplicationService;
using HelloWorld.Core.Domain.Resources;
using HelloWorld.Core.Api.Security;

namespace HelloWorld.Core.Api.Controllers
{
    [Authorize]
    [Route("v1/api/employee")]
    [EnableCors("AllowPolicy")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeApplicationService _employeeApplicationService;

        public EmployeeController(IEmployeeApplicationService employeeApplicationService)
        {
            _employeeApplicationService = employeeApplicationService;
        }

        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost, Route("authentication")]
        public async Task<IActionResult> Authentication([FromBody] dynamic request)
        {
            if (request == null)
                return BadRequest(Messages.ERROR_BAD_REQUEST);

            Employee employeeAuth = await _employeeApplicationService.AuthenticationAsync(request);

            if (employeeAuth == null)
                return await Response(string.Empty, _employeeApplicationService.ListNotifications());

            var token = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create(Runtime.SecurityKey))
                .AddSubject("Authentication")
                .AddIssuer(Runtime.Issuer)
                .AddAudience(Runtime.Audience)
                .AddClaim("Employee", employeeAuth.EmployeeNumber.ToString())
                .AddClaim("UserName", employeeAuth.UserName)
                .AddClaim("FirstName", employeeAuth.FirstName)
                .AddClaim(ClaimTypes.Name, employeeAuth.UserName)
                .AddExpiry(1)
                .Build();

            var response = new
            {
                access_token = token.Value,
                expires_in = token.ValidTo.ToLongDateString(),
                employee = new
                {
                    employeenumber = employeeAuth.EmployeeNumber,
                    username = employeeAuth.UserName,
                    firstname = employeeAuth.FirstName,
                    lastname = employeeAuth.LastName,
                    barcode = employeeAuth.Barcode
                }
            };

            return await Response(response, _employeeApplicationService.ListNotifications());
        }

   
        [HttpPost, Route("logout")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}