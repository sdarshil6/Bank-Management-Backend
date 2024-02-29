using BankManagement.Repositories;
using System.Runtime.CompilerServices;

namespace BankManagement
{
    public static class DependencyRegistrationFactoryBootstrapper
    {
        public static void RegisterDependencies(this IServiceCollection services) {
            services.AddScoped<AccountTypeCommonInterface, Savings>();
            services.AddScoped<AccountTypeCommonInterface, Current>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
        }
    }

    public interface IServiceFactory {
        AccountTypeCommonInterface GetInstance(string token);
    }

    public class ServiceFactory : IServiceFactory
    {
        private readonly IEnumerable<AccountTypeCommonInterface> services;

        public ServiceFactory(IEnumerable<AccountTypeCommonInterface> services)
        {
            this.services = services;
        }

        public AccountTypeCommonInterface GetInstance(string token)
        {
            return token switch
            {
                "Savings" => this.GetService(typeof(Savings)),
                "Current" => this.GetService(typeof(Current)),
                _ => throw new InvalidOperationException()
            } ; ;
            
        }

        public AccountTypeCommonInterface GetService(Type type) {
            return this.services.FirstOrDefault(x => x.GetType() == type)!;
        }
    }
}
