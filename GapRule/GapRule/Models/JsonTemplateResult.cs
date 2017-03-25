using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GapRule.Models
{
    public class JsonTemplateResult
    {
        public JsonTemplate ExecutionTemplate { get; set; }
        public string TestCaseName { get; set; }
        public int Id { get; set; }
        public List<Campsite> ResultingCampsites { get; set; }
    }
}