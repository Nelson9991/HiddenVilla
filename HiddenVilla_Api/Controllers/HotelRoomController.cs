using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bussines.Repository.IRepository;
using Common;
using Microsoft.AspNetCore.Authorization;
using Models;

namespace HiddenVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomController : ControllerBase
    {
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
        {
            _hotelRoomRepository = hotelRoomRepository;
        }

        [HttpGet]
        //[Authorize(Roles = Sd.RoleAdmin)]
        public async Task<ActionResult<List<HotelRoomDto>>> GetHotelRoomsAsync(string checkInDate = null,
            string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var convertedInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var convertedOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });
            }

            return await _hotelRoomRepository.GetAllHotelRooms(checkInDate, checkOutDate);
        }

        [HttpGet("{id:int}")]
        //[Authorize]
        public async Task<ActionResult<HotelRoomDto>> GetHotelRoomAsync(int id, string checkInDate = null,
            string checkOutDate = null)
        {
            if (string.IsNullOrEmpty(checkInDate) || string.IsNullOrEmpty(checkOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "All parameters need to be supplied"
                });
            }

            if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var convertedInDate))
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });
            }

            if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var convertedOutDate))
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid CheckIn date format. valid format will be MM/dd/yyyy"
                });
            }

            var hotelRoom = await _hotelRoomRepository.GetHotelRoom(id, checkInDate, checkOutDate);

            if (hotelRoom is null)
            {
                return BadRequest(new ErrorModel
                {
                    Title = "Error!",
                    ErrorStatusCode = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid room id"
                });
            }

            return hotelRoom;
        }
    }
}
