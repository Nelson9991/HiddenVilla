using AutoMapper;
using DataAccess.Data;
using Models;

namespace Bussines.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDto, HotelRoom>().ReverseMap();
            CreateMap<HotelRoomImageDto, HotelRoomImage>().ReverseMap();
            CreateMap<AmenitieDto, Amenitie>().ReverseMap();
        }
    }
}