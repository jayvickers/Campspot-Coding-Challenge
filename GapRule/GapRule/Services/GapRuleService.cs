using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using GapRule.Content;
using GapRule.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GapRule.Services
{
    public class GapRuleService : IGapRuleService
    {
        
        public GapRuleService()
        {
        }

        /// <summary>
        /// Consume json file from constants.
        /// </summary>
        /// <returns>Object modeled after json input files.</returns>
        public JsonTemplate ParseJsonFileIntoObjects(string jsonFile)
        {           
            var result = JsonConvert.DeserializeObject<JsonTemplate>(jsonFile);
            BuildCampsiteReservationList(result);           
            return result;
        }

        /// <summary>
        /// Iterate through campsites, if all base comparisons return true then campsite is valid for new reservation.
        /// </summary>
        /// <param name="inputJson">Input test object.</param>
        /// <returns>List of campsites that can host the new reservation without violating a gap rule.</returns>
        public List<Campsite> FindAvailableCampsites(JsonTemplate inputJson)
        {
            List<Campsite> availableCampsites = new List<Campsite>();
            foreach (Campsite tmpCampSite in inputJson.Campsites)
            {
                if (CompareCampsiteReservations(inputJson, tmpCampSite))
                {
                    availableCampsites.Add(tmpCampSite);
                }
            }
            return availableCampsites;
        }

        /// <summary>
        /// Iterate through each existing reservation and feed to base case comparison methods.
        /// If the new reservation will not fit with one existing reservation, returns false.
        /// </summary>
        /// <param name="inputJson">Input test object.</param>
        /// <param name="tmpCampSite">The current campsite we are trying to insert the new reservation into.</param>        
        /// <returns>bool indicating if a new reservation will fit between all existing reservation for all given gap rules.</returns>        
        public bool CompareCampsiteReservations(JsonTemplate inputJson, Campsite tmpCampSite)
        {
            var canFit = true;
            foreach (var res in tmpCampSite.CampsiteReservationList)
            {
                canFit = CompareInputRangeToReservationForEachGap(inputJson, res) && canFit;
            }
            return canFit;
        }

        /// <summary>
        /// Execute the base case comparison for each provided gap rule.
        /// Use && because the new reservation must fit for all provided gap rules.
        /// If it fails for one gap rule, logical operator will return false for the entire set of gap rules.
        /// </summary>
        /// <param name="inputSearch">The potential new reservation date range.</param>
        /// <param name="existingReservation">The reservation we are comparing the input date range against.</param>
        /// <param name="gapRules">List of all provided gap rules.</param>
        /// <returns>bool indicating if a new reservation will fit before or after a single existing reservation for all given gap rules.</returns>
        public bool CompareInputRangeToReservationForEachGap(JsonTemplate inputJson, Reservation existingReservation)
        {
            var canFit = true;
            foreach (var gap in inputJson.GapRules)
            {
                canFit = BaseCaseCompareInputRangeToReservation(inputJson.Search, existingReservation, gap) && canFit;
            }
            return canFit;
        }

        /// <summary>
        /// This is the most granular comparison. Checks to see
        /// if an input date range will violate a single gap rule for a single reservation.
        /// </summary>
        /// <param name="inputSearch">The potential new reservation date range.</param>
        /// <param name="existingReservation">The reservation we are comparing the input date range against.</param>
        /// <param name="gapRule">current gap rule.</param>
        /// <returns>bool indicating if a new reservation will fit before or after a single existing reservation for a single gap rule.</returns>
        public bool BaseCaseCompareInputRangeToReservation(Search inputSearch, Reservation existingReservation, Models.GapRule gapRule )
        {
            //have to subract 1 from the difference because a reservation includes the end and start dates. not a mathematical difference.
            if (inputSearch.StartDate > existingReservation.EndDate && (inputSearch.StartDate - existingReservation.EndDate).TotalDays - 1 != gapRule.GapSize)
            {
                return true;
            }
            if (inputSearch.EndDate < existingReservation.StartDate && (existingReservation.StartDate - inputSearch.EndDate).TotalDays - 1 != gapRule.GapSize)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add a list of reservations to each campsite.
        /// This makes it easier to keep track of campsites when traversing through reservations.
        /// </summary>
        /// <param name="inputJson">input test object.</param>
        private void BuildCampsiteReservationList(JsonTemplate inputJson)
        {
            foreach (var res in inputJson.Reservations)
            {
                try
                {
                    Campsite tmpCampSite = inputJson.Campsites.Where(x => x.Id == res.CampsiteId).First();
                    tmpCampSite.CampsiteReservationList.Add(res);
                }
                catch (Exception ex)
                {
                    //couldn't find that campsite ID in our list of campsites
                    throw new Exception("Invalid Campsite ID provided with reservation. " + ex.Message);
                }
            }
        }
    }
}