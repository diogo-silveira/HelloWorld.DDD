using System.Runtime.CompilerServices;
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
    [Route("v1/api/expense")]
    [EnableCors("AllowPolicy")]
    public class ExpenseController : BaseController
    {
        private readonly IExpenseApplicationService _expenseApplicationService;

        public ExpenseController(IExpenseApplicationService expenseApplicationService)
        {
            _expenseApplicationService = expenseApplicationService;
        }

        [AllowAnonymous]
        [HttpPost, Route("InvokeImporterEmailData")]
        public async Task<IActionResult> InvokeImporterEmailData([FromBody] dynamic request)
        {
            if (request == null)
                return BadRequest(Messages.ERROR_BAD_REQUEST);

            var result = _expenseApplicationService.InvokeImporterEmailData(request);

            var response = new
            {
                data = result
            };

            return await Response(response, _expenseApplicationService.ListNotifications());
        }

        [HttpPost, Route("add")]
        public async Task<IActionResult> Add([FromBody] dynamic request)
        {
            if (request == null)
                return BadRequest(Messages.ERROR_BAD_REQUEST);

            var response = _expenseApplicationService.Add(request);

            return await Response(response, _expenseApplicationService.ListNotifications());
        }
        [AllowAnonymous]
        [HttpGet, Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var response = _expenseApplicationService.GetAll();

            return await Response(response, _expenseApplicationService.ListNotifications());
        }
        [AllowAnonymous]
        [HttpGet, Route("getbyid")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            if (id == 0)
                return BadRequest(Messages.ERROR_BAD_REQUEST);

            var response = _expenseApplicationService.GetById(id);

            return await Response(response, _expenseApplicationService.ListNotifications());
        }

    }
}