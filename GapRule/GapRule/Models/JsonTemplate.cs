﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GapRule.Models
{
    public class JsonTemplate
    {
        public Search Search { get; set; }
        public List<Campsite> Campsites { get; set; }
        public List<GapRule> GapRules { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}