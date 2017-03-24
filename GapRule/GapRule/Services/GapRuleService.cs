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
        public JsonTemplate ParseJsonFile()
        {
            var jsonFile = Constants.TestCase1;
            var result = JsonConvert.DeserializeObject<JsonTemplate>(jsonFile);
            result.Reservations = result.Reservations.OrderBy(x => x.CampsiteId).ToList();
            return result;
        }

        public int Test(JsonTemplate inputJson)
        {
            foreach (var res in inputJson.Reservations)
            {
                if (res.EndDate )
                {
                    
                }
            }
            return 0;
        }
        //TODO: make the reservations specific to each campsite, run the method below for the list of reservations on each campsite. if you get TRUEs back then add that campsite to return list.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSearch"></param>
        /// <param name="existingReservation"></param>
        /// <param name="gapRules"></param>
        /// <returns></returns>
        public bool BaseCaseForEachGap(Search inputSearch, List<Reservation> existingReservation, List<Models.GapRule> gapRules)
        {
            var canFit = true;
            foreach (var res in existingReservation)
            {
                canFit = BaseCaseForEachGap(inputSearch, res, gapRules) && canFit;
            }
            return canFit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputSearch"></param>
        /// <param name="existingReservation"></param>
        /// <param name="gapRules"></param>
        /// <returns></returns>
        public bool BaseCaseForEachGap(Search inputSearch, Reservation existingReservation, List<Models.GapRule> gapRules)
        {
            var canFit = true;
            foreach (var gap in gapRules)
            {
                canFit = BaseCase(inputSearch, existingReservation, gap) && canFit;
            }
            return canFit;
        }

        /// <summary>
        /// This is the most granular comparison. Checks to see
        /// if an input date range will violate a single gap rule for a single reservation.
        /// </summary>
        /// <param name="inputSearch">The potential reservation date range.</param>
        /// <param name="existingReservation">The reservation we are comparing the input date range against.</param>
        /// <param name="gapRule">current gap rule.</param>
        /// <returns></returns>
        public bool BaseCase(Search inputSearch, Reservation existingReservation, Models.GapRule gapRule )
        {
            if (inputSearch.StartDate > existingReservation.EndDate && (inputSearch.StartDate - existingReservation.EndDate).TotalDays < gapRule.GapSize)
            {
                return true;
            }
            if (inputSearch.EndDate < existingReservation.StartDate && (existingReservation.EndDate - inputSearch.EndDate).TotalDays < gapRule.GapSize)
            {
                return true;
            }
            return false;
        }
    }
}