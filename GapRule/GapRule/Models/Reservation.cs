using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GapRule.Models
{
    /// <summary>
    /// Existing reservation
    /// </summary>
    public class Reservation
    {
        public int CampsiteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}