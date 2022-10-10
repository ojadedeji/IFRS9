using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class CompanyModuleInfo
    {
        public CompanyModule CompanyModule { get; set; }
        public Module Module { get; set; }
        public Company Company { get; set; }
    }
}