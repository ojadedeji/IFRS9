using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class DatabaseInfo
    {
        public Database Database { get; set; }

        public Solution Solution { get; set; }
    }
}