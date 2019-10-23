using System;
using System.Collections.Generic;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.ApplicationService;
using HelloWorld.Core.Domain.Interfaces.Service;
using HelloWorld.Core.Domain.Interfaces.UnitOfWork;
using HelloWorld.Core.Domain.Resources;
using HelloWorld.Core.Domain.Scopes;
using HelloWorld.Core.Domain.Util;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using HelloWorld.Core.Domain.DTO.Request;

namespace HelloWorld.Core.ApplicationService
{
    public class HelloWorldApplicationService : BaseApplicationService, IHelloWorldApplicationService
    {
        private readonly IHelloWorldService  _helloWorldService;

        public HelloWorldApplicationService(IHelloWorldService helloWorldService, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _helloWorldService = helloWorldService;
        }

        public async Task<string> HelloWorld()
        {
            var result = await _helloWorldService.HelloWorld();

            if (string.IsNullOrWhiteSpace(result))
            {
                AddNotification("Hello World", Messages.SUCCESS_NO_RECORDS);
                return string.Empty;
            }

            return result;
        }


    }
}