using System;

namespace HiddenVilla_Client.Models.ViewModels
{
    public class HomeVm
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int NoOfNights { get; set; } = 1;
    }
}