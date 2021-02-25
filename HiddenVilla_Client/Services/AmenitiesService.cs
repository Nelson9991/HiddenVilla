using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HiddenVilla_Client.Services.IServices;
using Models;

namespace HiddenVilla_Client.Services
{
    public class AmenitiesService : IAmenitieService
    {
        private readonly HttpClient _httpClient;

        public AmenitiesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AmenitieDto>> GetAllAmenities()
        {
            var resp = await _httpClient.GetAsync(
                "api/amenitie");
            var content = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<AmenitieDto>>(content,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        }
    }
}