
//using HelloWorld.Core.Infrastructure.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorld.Core.Api.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            //NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}