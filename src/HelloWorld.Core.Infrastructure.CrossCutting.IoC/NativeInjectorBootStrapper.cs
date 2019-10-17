using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using HelloWorld.Core.ApplicationService;
using HelloWorld.Core.Domain.Interfaces.ApplicationService;
using HelloWorld.Core.Domain.Interfaces.Repository;
using HelloWorld.Core.Domain.Interfaces.Service;
using HelloWorld.Core.Domain.Interfaces.UnitOfWork;
using HelloWorld.Core.Infrastructure.Data.Context;
using HelloWorld.Core.Infrastructure.Data.Repository;
using HelloWorld.Core.Infrastructure.Data.UnitOfWork;
using HelloWorld.Core.Service;

namespace HelloWorld.Core.Infrastructure.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region [ APPLICATION SERVICE ]

            services.AddScoped<IBaseApplicationService, BaseApplicationService>();

            services.AddScoped<IEmployeeApplicationService, EmployeeApplicationService>();
            services.AddScoped<IExpenseApplicationService, ExpenseApplicationService>();


            #endregion

            #region [ SERVICE ]
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            #endregion

            #region [ DATA ] 

            services.AddScoped<SerkoCoreDataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();


            #endregion

        }
    }
}