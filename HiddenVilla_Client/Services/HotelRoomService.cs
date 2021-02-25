using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using HiddenVilla_Client.Services.IServices;
using Models;

namespace HiddenVilla_Client.Services
{
    public class HotelRoomService : IHotelRoomService
    {
        private readonly HttpClient _httpClient;

        public HotelRoomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<HotelRoomDto>> GetHotelRooms(string checkInDate, string checkOutDate)
        {
            var resp = await _httpClient.GetAsync(
                $"api/hotelRoom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
            var content = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<HotelRoomDto>>(content,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        }

        public async Task<HotelRoomDto> GetHotelRoomDetail(int roomId, string checkInDate, string checkOutDate)
        {
            throw new System.NotImplementedException();
        }
    }
}