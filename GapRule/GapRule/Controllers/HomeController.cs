using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GapRule.Services;
using GapRule.Tests;
using GapRule.Models;

namespace GapRule.Controllers
{
    public class HomeController : Controller
    {
        IGapRuleService _gapRuleService = new GapRuleService();
        GapRuleServiceTests _testService = new GapRuleServiceTests();
        // GET: Home
        public ActionResult Index()
        {           
            return View(IndexSetup());
        }

        /// <summary>
        /// Build view models to feed the Index view data.
        /// </summary>
        /// <returns>Populated view model.</returns>
        private JsonTemplateResultViewModel IndexSetup()
        {
            JsonTemplateResultViewModel allResults = new JsonTemplateResultViewModel();
            allResults.Results = GetTestResults();
            return allResults;
        }

        private List<JsonTemplateResult> GetTestResults()
        {
            JsonTemplate jsonFile = _gapRuleService.ParseJsonFileIntoObjects("test-case.json");
            List<Campsite> availableCampsites = _gapRuleService.FindAvailableCampsites(jsonFile);

            JsonTemplateResult results = new JsonTemplateResult
            {
                ExecutionTemplate = jsonFile,
                TestCaseName = "Provided Test Case",
                Id = 1,
                ResultingCampsites = availableCampsites
            };

            JsonTemplate jsonFileTestCase2 = _gapRuleService.ParseJsonFileIntoObjects("test-case2.json");
            List<Campsite> availableCampsitesTestCase2 = _gapRuleService.FindAvailableCampsites(jsonFileTestCase2);

            JsonTemplateResult resultsTestCase2 = new JsonTemplateResult
            {
                ExecutionTemplate = jsonFileTestCase2,
                TestCaseName = "Custom Test Case 2",
                Id = 2,
                ResultingCampsites = availableCampsitesTestCase2
            };

            JsonTemplate jsonFileTestCase3 = _gapRuleService.ParseJsonFileIntoObjects("test-case3.json");
            List<Campsite> availableCampsitesTestCase3 = _gapRuleService.FindAvailableCampsites(jsonFileTestCase3);

            JsonTemplateResult resultsTestCase3 = new JsonTemplateResult
            {
                ExecutionTemplate = jsonFileTestCase3,
                TestCaseName = "Custom Test Case 3",
                Id = 3,
                ResultingCampsites = availableCampsitesTestCase3
            };

            JsonTemplate jsonFileTestCase4 = _gapRuleService.ParseJsonFileIntoObjects("test-case4.json");
            List<Campsite> availableCampsitesTestCase4 = _gapRuleService.FindAvailableCampsites(jsonFileTestCase4);

            JsonTemplateResult resultsTestCase4 = new JsonTemplateResult
            {
                ExecutionTemplate = jsonFileTestCase4,
                TestCaseName = "Custom Test Case 4",
                Id = 4,
                ResultingCampsites = availableCampsitesTestCase4
            };

            JsonTemplate jsonFileTestCase5 = _gapRuleService.ParseJsonFileIntoObjects("test-case5.json");
            List<Campsite> availableCampsitesTestCase5 = _gapRuleService.FindAvailableCampsites(jsonFileTestCase5);

            JsonTemplateResult resultsTestCase5 = new JsonTemplateResult
            {
                ExecutionTemplate = jsonFileTestCase5,
                TestCaseName = "Custom Test Case 5",
                Id = 5,
                ResultingCampsites = availableCampsitesTestCase5
            };

            return new List<JsonTemplateResult> {
                results,
                resultsTestCase2,
                resultsTestCase3,
                resultsTestCase4,
                resultsTestCase5
            };            
        }
        /// <summary>
        /// Custom test cases to enforce business logic
        /// </summary>
        private void ExecuteTestCases()
        {
            var test1 = _testService.SingleCampsiteTestShouldBeTrue();
            var test2 = _testService.TwoCampsiteTestShouldBeFalse();
            var test3 = _testService.TwoCampsiteTestShouldBeTrue();
            var test4 = _testService.ThreeCampsiteTestShouldBeTrue();
            var test5 = _testService.ThreeCampsiteTestShouldBeFalse();
        }
    }
}