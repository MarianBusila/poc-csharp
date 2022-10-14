﻿using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
                var platforms = grpcClient.GetAllPlatforms();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepository>(), platforms);
            }
        }

        private static void SeedData(ICommandRepository commandRepository, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("--> Seeding new platforms");
            foreach(Platform platform in platforms)
            {
                if (!commandRepository.ExternalPlatformExists(platform.ExternalID))
                {
                    commandRepository.CreatePlatform(platform);
                }
                commandRepository.SaveChanges();
            }
        }
    }
}
