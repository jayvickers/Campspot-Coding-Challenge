using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GapRule.Services;
using GapRule.Models;

namespace GapRule.Tests
{
    /// <summary>
    /// Custom test cases ensuring execution meets hard coded business logic results.
    /// </summary>
    public class GapRuleServiceTests
    {
        private IGapRuleService _service { get; set; }
        public GapRuleServiceTests()
        {
            _service = new GapRuleService();
        }

        public bool SingleCampsiteTestShouldBeTrue()
        {
            var availableCampSites = ExecuteTestCase("test-case2.json");
            return availableCampSites == 1;
        }

        public bool TwoCampsiteTestShouldBeFalse()
        {
            var availableCampSites = ExecuteTestCase("test-case3.json");
            return availableCampSites == 1;
        }

        public bool TwoCampsiteTestShouldBeTrue()
        {
            var availableCampSites = ExecuteTestCase("test-case3.json");
            return availableCampSites == 2;
        }
        public bool ThreeCampsiteTestShouldBeTrue()
        {
            var availableCampSites = ExecuteTestCase("test-case4.json");
            return availableCampSites == 3;
        }
        public bool ThreeCampsiteTestShouldBeFalse()
        {
            var availableCampSites = ExecuteTestCase("test-case4.json");
            return availableCampSites == 2;
        }
        private int ExecuteTestCase(string jsonName)
        {
            JsonTemplate jsonFile = _service.ParseJsonFileIntoObjects(jsonName);
            List<Campsite> availableCampsites = _service.FindAvailableCampsites(jsonFile);
            return availableCampsites.Count;
        }
    }
}