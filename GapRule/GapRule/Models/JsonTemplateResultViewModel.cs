using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GapRule.Models
{
    /// <summary>
    /// View Model to contain results of an input file. Used only for displaying
    /// </summary>
    public class JsonTemplateResultViewModel
    {
        public List<JsonTemplateResult> Results { get; set; }
    }
}