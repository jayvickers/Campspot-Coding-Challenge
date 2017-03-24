// ************************************************************************************
// ***                                                                              ***
// *** Copyright 2001-2016 Envision Technology Partners, Inc. All Rights Reserved.  ***
// ***                                                                              ***
// ************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GapRule.Models;

namespace GapRule.Services
{
    public interface IGapRuleService
    {
        JsonTemplate ParseJsonFile();
    }
}
