using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HelloWorld.Core.Domain.DTO.Request;
using HelloWorld.Core.Domain.Entities;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Domain.Interfaces.Service;
using HelloWorld.Core.Domain.Resources;
using HelloWorld.Core.Domain.Util;

namespace HelloWorld.Core.Service
{
    public class HelloWorldService : IHelloWorldService
    {
        private readonly string HelloWorldMessage = "Hello World!";

        public async Task<string> HelloWorld()
            => await Task.FromResult( HelloWorldMessage );
    }
}