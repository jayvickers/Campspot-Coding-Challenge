using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GapRule.Services;
using GapRule.Content;
using GapRule.Models;

namespace GapRule.Tests
{
    public class GapRuleServiceTests
    {
        private IGapRuleService _service { get; set; }
        public GapRuleServiceTests()
        {
            _service = new GapRuleService();
        }

        public bool SingleCampsiteTestShouldBeTrue()
        {
            var availableCampSites = ExecuteTestCase(Constants.TestCase2);
            return availableCampSites == 1;
        }

        public bool TwoCampsiteTestShouldBeFalse()
        {
            var availableCampSites = ExecuteTestCase(Constants.TestCase3);
            return availableCampSites == 1;
        }

        public bool TwoCampsiteTestShouldBeTrue()
        {
            var availableCampSites = ExecuteTestCase(Constants.TestCase3);
            return availableCampSites == 2;
        }
        public bool ThreeCampsiteTestShouldBeTrue()
        {
            var availableCampSites = ExecuteTestCase(Constants.TestCase4);
            return availableCampSites == 3;
        }
        public bool ThreeCampsiteTestShouldBeFalse()
        {
            var availableCampSites = ExecuteTestCase(Constants.TestCase4);
            return availableCampSites == 2;
        }
        private int ExecuteTestCase(string jsonString)
        {
            JsonTemplate jsonFile = _service.ParseJsonFileIntoObjects(jsonString);
            List<Campsite> availableCampsites = _service.FindAvailableCampsites(jsonFile);
            return availableCampsites.Count;
        }
    }
}