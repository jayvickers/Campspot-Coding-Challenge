using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Index(bool executeTests = false)
        {           
            return View(IndexSetup(executeTests));
        }
        
        public ActionResult GetTestResults()
        {
            return RedirectToAction("Index", new { executeTests = true });
        }

        /// <summary>
        /// Build view models to feed the Index view data.
        /// </summary>
        /// <returns>Populated view model.</returns>
        private JsonTemplateResultViewModel IndexSetup(bool ExecuteTests = false)
        {
            JsonTemplateResultViewModel allResults = new JsonTemplateResultViewModel();
            allResults.Results = GetTestTemplates(ExecuteTests);
            return allResults;
        }

        private JsonTemplateResult GetTestResults(string fileName, int Id)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/");
            JsonTemplate jsonFile = _gapRuleService.ParseJsonFileIntoObjects(fileName);
            List<Campsite> availableCampsites = _gapRuleService.FindAvailableCampsites(jsonFile);
            JsonTemplateResult result = new JsonTemplateResult
            {
                ExecutionTemplate = jsonFile,
                TestCaseName = fileName,
                Id = Id,
                ResultingCampsites = availableCampsites
            };
            return result;
        }

        /// <summary>
        /// Get all json files from Content directory and prepare them to be executed.
        /// </summary>
        /// <returns>List containing templat of each json file.</returns>
        private List<JsonTemplateResult> GetTestTemplates(bool executeTests = false)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~/Content/");
            DirectoryInfo d = new DirectoryInfo(path);
            int i = 0;
            var returnResultList = new List<JsonTemplateResult>();
            foreach (var file in d.GetFiles("*.json"))
            {
                i += 1;
                JsonTemplate jsonFile = _gapRuleService.ParseJsonFileIntoObjects(file.Name);                
                List<Campsite> availableCampsites = executeTests ? _gapRuleService.FindAvailableCampsites(jsonFile) : new List<Campsite>();
                JsonTemplateResult results = new JsonTemplateResult
                {
                    ExecutionTemplate = jsonFile,
                    TestCaseName = file.Name,
                    Id = i,
                    ResultingCampsites = availableCampsites
                };
                returnResultList.Add(results);
            }
            return returnResultList;               
        }
        /// <summary>
        /// Additional custom test cases to enforce business logic
        /// </summary>
        private void ExecuteAdditionalTestCases()
        {
            var test1 = _testService.SingleCampsiteTestShouldBeTrue();
            var test2 = _testService.TwoCampsiteTestShouldBeFalse();
            var test3 = _testService.TwoCampsiteTestShouldBeTrue();
            var test4 = _testService.ThreeCampsiteTestShouldBeTrue();
            var test5 = _testService.ThreeCampsiteTestShouldBeFalse();
        }
    }
}