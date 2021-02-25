using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Bussines.Repository.IRepository
{
    public interface IHotelRoomImagesRepository
    {
        public Task<int> CreateHotelRoomImage(HotelRoomImageDto hotelRoomImageDto);
        public Task<int> DeleteHotelRoomImageById(int imageId);
        public Task<int> DeleteHotelRoomImageByRoomId(int roomId);
        public Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl);
        public Task<List<HotelRoomImageDto>> GetHotelRoomImages(int roomId);
    }
}