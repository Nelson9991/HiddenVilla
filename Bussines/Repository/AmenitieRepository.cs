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
    public class AmenitieRepository : IAmenitieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AmenitieRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AmenitieDto> CreateAmenitie(AmenitieDto amenitieDto)
        {
            var ameniteDb = _mapper.Map<Amenitie>(amenitieDto);

            var addedAmenite = await _context.Amenities.AddAsync(ameniteDb);
            await _context.SaveChangesAsync();
            return _mapper.Map<AmenitieDto>(addedAmenite.Entity);
        }

        public async Task<AmenitieDto> UpdateAmenitie(int amenitieId, AmenitieDto amenitieDto)
        {
            try
            {
                if (amenitieId == amenitieDto.Id)
                {
                    var ameniteDb = await _context.Amenities.FirstOrDefaultAsync(x => x.Id == amenitieId);

                    if (ameniteDb is null)
                    {
                        return null;
                    }

                    _mapper.Map(amenitieDto, ameniteDb);

                    await _context.SaveChangesAsync();

                    return _mapper.Map<AmenitieDto>(ameniteDb);
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

        public async Task<AmenitieDto> GetAmenitie(int amenitieId)
        {
            try
            {
                var amenitieDb = await _context.Amenities.FirstOrDefaultAsync(x => x.Id == amenitieId);

                var amenitieDto = _mapper.Map<AmenitieDto>(amenitieDb);

                return amenitieDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> DeleteAmenitie(int amenitieId)
        {
            var amenite = await _context.Amenities.FindAsync(amenitieId);

            if (amenite is null)
            {
                return 0;
            }

            _context.Amenities.Remove(amenite);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<AmenitieDto>> GetAllAmenities()
        {
            try
            {
                return _mapper.Map<List<AmenitieDto>>(
                    await _context.Amenities.ToListAsync());
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> AmenitieExists(string name, int amenitieId = 0)
        {
            if (amenitieId == 0)
            {
                return await _context.Amenities.AnyAsync(x =>
                    x.Name.ToLower() == name.ToLower());
            }

            return await _context.Amenities.AnyAsync(x =>
                x.Name.ToLower() == name.ToLower() && x.Id != amenitieId);
        }
    }
}