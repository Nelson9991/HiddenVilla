using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace HiddenVilla_Client.Services.IServices
{
    public interface IAmenitieService
    {
        public Task<IEnumerable<AmenitieDto>> GetAllAmenities();
    }
}