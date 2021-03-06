﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GapRule.Models
{
    /// <summary>
    /// Existing campsites
    /// </summary>
    public class Campsite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Reservation> CampsiteReservationList = new List<Reservation>();
    }
}