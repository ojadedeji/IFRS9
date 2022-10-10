using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.Core.Contracts
{
    public class ProcessRoleInfo
    {
        public ProcessRole ProcessRole { get; set; }
        public Processes Processes { get; set; }
    }
}