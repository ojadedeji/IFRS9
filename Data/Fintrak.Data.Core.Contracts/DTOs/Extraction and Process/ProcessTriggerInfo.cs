using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ProcessTriggerInfo
    {
        public ProcessTrigger ProcessTrigger { get; set; }
        public ProcessJob ProcessJob { get; set; }
        public Processes Processes { get; set; }
       
    }
}