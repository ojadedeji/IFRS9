using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ExtractionTriggerInfo
    {
        public ExtractionTrigger ExtractionTrigger { get; set; }
        public ExtractionJob ExtractionJob { get; set; }
        public Extraction Extraction { get; set; }
       
    }
}