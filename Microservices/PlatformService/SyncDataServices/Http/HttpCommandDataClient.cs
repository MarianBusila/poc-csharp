using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        async Task ICommandDataClient.SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["CommandsService"]}/api/c/platforms", httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.Write("--> Sync POST to CommandsService was OK!");
            }
            else
            {
                Console.Write("--> Sync POST to CommandsService was NOT OK!");
            }
        }
    }
}
