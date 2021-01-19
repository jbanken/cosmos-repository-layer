using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.CosmosRepository.DataAccess;
using System;
using System.Threading.Tasks;

namespace POC.CosmosRepository
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var configBuilder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", true, true);

            var config = configBuilder.Build();


            var services = new ServiceCollection();
            services.AddSingleton(config);
            services.AddScoped<ICosmosClientFactory, CosmosClientFactory>();
            services.AddScoped<IDummieRepository, DummieRepository>();

            var serviceProvider = services.BuildServiceProvider();

            var dummieRepo = serviceProvider.GetService<IDummieRepository>();

            var dummie = await dummieRepo.AddAsync(new DataAccess.DataEntities.DummieDataEntity() { Name = "joe" });

        }
    }
}
