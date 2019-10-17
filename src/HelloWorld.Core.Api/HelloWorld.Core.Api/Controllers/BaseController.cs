using FluentValidator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld.Core.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected new async Task<IActionResult> Response(object response, IEnumerable<Notification> notifications)
        {
            try
            {
                var enumerable = notifications as Notification[] ?? notifications.ToArray();

                if (!enumerable.Any())
                {
                    return Ok(await Task.Factory.StartNew(() => new { success = true, data = response }));
                }
                else
                {
                    return Ok(await Task.Factory.StartNew(() => new { success = false, errors = enumerable }));
                }
            }
            catch (Exception e)
            {
                return BadRequest(await Task.Factory.StartNew(() =>
                    new {success = false, errors = new[] {e.InnerException}}));
            }
        }
    }
}