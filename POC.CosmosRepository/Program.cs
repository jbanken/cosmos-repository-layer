using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.CosmosRepository.DataAccess;
using POC.CosmosRepository.DataAccess.DataEntities;
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

            var id = Guid.NewGuid().ToString();
            var dummie = await dummieRepo.AddAsync(new DataAccess.DataEntities.DummieDataEntity() { Name = "testname", id = id, ID=id }, id);
            dummie = await dummieRepo.GetByIDAsync<DummieDataEntity>("ID", id);
            dummie.Name = "testname2";
            dummie = await dummieRepo.UpdateAsync(dummie, dummie.id, dummie.ID);
            dummie = await dummieRepo.GetByIDAsync<DummieDataEntity>("ID", dummie.ID);
            await dummieRepo.DeleteAsync<DummieDataEntity>(dummie.id, dummie.ID);



        }
    }
}
