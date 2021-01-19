using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.CosmosRepository.DataAccess;
using System;
using System.IO;
using System.Threading.Tasks;

namespace POC.CosmosRepository
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", false, true);

            var config = configBuilder.Build();


            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddScoped<ICosmosClientFactory, CosmosClientFactory>();
            services.AddScoped<IDummieRepository, DummieRepository>();

            var serviceProvider = services.BuildServiceProvider();

            var dummieRepo = serviceProvider.GetService<IDummieRepository>();

            var dummie = await dummieRepo.AddAsync(new DataAccess.DataEntities.DummieDataEntity() { Name = "joe", ID = Guid.NewGuid().ToString(), id = Guid.NewGuid().ToString() }, "ID");

        }
    }
}
