using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AnalyticsDataReader.Domain
{
    public static class RegisterServices
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}