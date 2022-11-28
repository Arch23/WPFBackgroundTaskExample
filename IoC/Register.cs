using Busines;
using Busines.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class Register
    {
        public static void Business(IServiceCollection services)
        {
            services.AddSingleton<IFactorial, Factorial>();
        }
    }
}
