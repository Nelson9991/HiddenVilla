using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace HiddenVilla_Client.Services.IServices
{
    public interface IHotelRoomService
    {
        public Task<IEnumerable<HotelRoomDto>> GetHotelRooms(string checkInDate, string checkOutDate);
        public Task<HotelRoomDto> GetHotelRoomDetail(int roomId, string checkInDate, string checkOutDate);
    }
}