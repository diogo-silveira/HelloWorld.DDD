using HelloWorld.Core.Domain.Interfaces.ApplicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Core.Api.Controllers
{
    [Authorize]
    [Route("v1/api/helloworld")]
    [EnableCors("AllowPolicy")]
    public class HelloWorldController : BaseController
    {
        private readonly IHelloWorldApplicationService _helloWorldApplicationService;

        public HelloWorldController(IHelloWorldApplicationService helloWorldApplicationService)
        {
            _helloWorldApplicationService = helloWorldApplicationService;
        }

        [AllowAnonymous]
        [HttpGet, Route("helloworld")]
        public async Task<IActionResult> HelloWorld()
        {
            string response = await _helloWorldApplicationService.HelloWorld();

            return await Response(response, _helloWorldApplicationService.ListNotifications());
        }

        [Authorize]
        [HttpGet, Route("helloworldauthorised")]
        public async Task<IActionResult> HelloWorldAuthorised()
        {
            string response = await _helloWorldApplicationService.HelloWorld();

            return await Response(response, _helloWorldApplicationService.ListNotifications());
        }
    }
}
