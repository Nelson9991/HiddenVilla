using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Bussines.Repository.IRepository
{
    public interface IAmenitieRepository
    {
        public Task<AmenitieDto> CreateAmenitie(AmenitieDto amenitieDto);
        public Task<AmenitieDto> UpdateAmenitie(int amenitieId, AmenitieDto amenitieDto);
        public Task<AmenitieDto> GetAmenitie(int amenitieId);
        public Task<int> DeleteAmenitie(int amenitieId);
        public Task<List<AmenitieDto>> GetAllAmenities();
        public Task<bool> AmenitieExists(string name, int amenitieId = 0);
    }
}