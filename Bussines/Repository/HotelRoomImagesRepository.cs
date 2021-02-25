using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bussines.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Bussines.Repository
{
    public class HotelRoomImagesRepository : IHotelRoomImagesRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public HotelRoomImagesRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> CreateHotelRoomImage(HotelRoomImageDto imageDto)
        {
            var imageDb = _mapper.Map<HotelRoomImage>(imageDto);
            await _context.HotelRoomImages.AddAsync(imageDb);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageById(int imageId)
        {
            var image = await _context.HotelRoomImages.FindAsync(imageId);

            _context.HotelRoomImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByRoomId(int roomId)
        {
            var images = await _context.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

            _context.HotelRoomImages.RemoveRange(images);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteHotelRoomImageByImageUrl(string imageUrl)
        {
            var imageToDelete =
                await _context.HotelRoomImages.FirstOrDefaultAsync(x => x.RoomImageUrl.ToLower() == imageUrl.ToLower());
            if (imageToDelete is null)
            {
                return 0;
            }

            _context.HotelRoomImages.Remove(imageToDelete);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<HotelRoomImageDto>> GetHotelRoomImages(int roomId)
        {
            var images = await _context.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync();

            return _mapper.Map<List<HotelRoomImageDto>>(images);
        }
    }
}