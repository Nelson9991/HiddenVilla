using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bussines.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Bussines.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public HotelRoomRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto hotelRoomDto)
        {
            var hotelRoomDb = _mapper.Map<HotelRoom>(hotelRoomDto);
            hotelRoomDb.CreatedDate = DateTime.Now;
            hotelRoomDb.CreatedBy = "";
            var addedHotelRoom = await _context.HotelRooms.AddAsync(hotelRoomDb);
            await _context.SaveChangesAsync();
            return _mapper.Map<HotelRoomDto>(addedHotelRoom.Entity);
        }

        public async Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto hotelRoomDto)
        {
            try
            {
                if (roomId == hotelRoomDto.Id)
                {
                    var hotelRoomDb = await _context.HotelRooms.FirstOrDefaultAsync(x => x.Id == roomId);
                    _mapper.Map(hotelRoomDto, hotelRoomDb);

                    if (hotelRoomDb is null)
                    {
                        return null;
                    }

                    hotelRoomDb.UpdatedDate = DateTime.Now;
                    hotelRoomDb.UpdatedBy = "";

                    await _context.SaveChangesAsync();

                    return _mapper.Map<HotelRoomDto>(hotelRoomDb);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<HotelRoomDto> GetHotelRoom(int roomId, string checkInDate,
            string checkOutDate)
        {
            try
            {
                var hotelRoomDb = await _context.HotelRooms.Include(x => x.HotelRoomImages)
                    .FirstOrDefaultAsync(x => x.Id == roomId);

                var hotelRoomsDto = _mapper.Map<HotelRoomDto>(hotelRoomDb);

                return hotelRoomsDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int roomId)
        {
            var room = await _context.HotelRooms.FindAsync(roomId);

            if (room is null)
            {
                return 0;
            }

            var allImages = await _context.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

            _context.HotelRoomImages.RemoveRange(allImages);
            _context.HotelRooms.Remove(room);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoomDto>> GetAllHotelRooms(string checkInDate,
            string checkOutDate)
        {
            try
            {
                var hotelRoomsDtos = _mapper.Map<List<HotelRoomDto>>(
                    await _context.HotelRooms
                        .Include(x => x.HotelRoomImages)
                        .ToListAsync());

                return hotelRoomsDtos;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RoomExists(string name, int roomId = 0)
        {
            if (roomId == 0)
            {
                return await _context.HotelRooms.AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower());
            }

            return await _context.HotelRooms.AnyAsync(x =>
                x.Name.ToLower() == name.ToLower() && x.Id != roomId);
        }
    }
}