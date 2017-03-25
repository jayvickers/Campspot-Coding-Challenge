using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GapRule.Models
{
    /// <summary>
    /// Input start and end date for a new reservation
    /// </summary>
    public class Search
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}