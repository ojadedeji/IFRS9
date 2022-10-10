using Fintrak.Shared.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class ModuleInfo
    {
        public Module Module { get; set; }
        public Solution Solution { get; set; }
    }
}