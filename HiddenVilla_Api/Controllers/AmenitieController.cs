using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussines.Repository.IRepository;
using Models;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitieController : ControllerBase
    {
        private readonly IAmenitieRepository _amenitieRepository;

        public AmenitieController(IAmenitieRepository amenitieRepository)
        {
            _amenitieRepository = amenitieRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<AmenitieDto>>> GetHotelAmenitiesAsync()
        {
            return await _amenitieRepository.GetAllAmenities();
        }
    }
}
