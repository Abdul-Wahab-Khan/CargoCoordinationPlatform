using System.Reflection;
using CargoCoordinationPlatform.Application.Common.Behaviours;
using CargoCoordinationPlatform.Application.Common.Interfaces;
using CargoCoordinationPlatform.Application.Common.Mappings;
using CargoCoordinationPlatform.Application.Common.Services;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(Mapping));

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenRequestPreProcessor(typeof(LoggingBehaviour<>));
            cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });

        builder.Services.AddTransient<IBidsLoadValidatorService, BidsLoadValidatorService>();
    }
}
