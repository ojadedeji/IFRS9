﻿using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ClosedPeriodTemplateInfo
    {
        public ClosedPeriodTemplate ClosedPeriodTemplate { get; set; }
        public Solution Solution { get; set; }
    }
}