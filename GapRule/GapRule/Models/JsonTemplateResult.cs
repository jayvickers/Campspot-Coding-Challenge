using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GapRule.Models
{
    /// <summary>
    /// Helper class, designed to aid in displaying results
    /// </summary>
    public class JsonTemplateResult
    {
        public JsonTemplate ExecutionTemplate { get; set; }
        public string TestCaseName { get; set; }
        public int Id { get; set; }
        public List<Campsite> ResultingCampsites { get; set; }
    }
}