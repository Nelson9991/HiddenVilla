using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Bussines.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        public Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto);
        public Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto);
        public Task<HotelRoomDto> GetHotelRoom(int roomId, string checkInDate = null, string checkOutDate = null);
        public Task<int> DeleteHotelRoom(int roomId);
        public Task<List<HotelRoomDto>> GetAllHotelRooms(string checkInDate = null, string checkOutDate = null);
        public Task<bool> RoomExists(string name, int roomId = 0);
    }
}