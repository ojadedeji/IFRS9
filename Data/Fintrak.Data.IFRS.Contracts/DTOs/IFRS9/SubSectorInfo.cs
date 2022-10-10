using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class SubSectorInfo
    {
        public Sector Sector { get; set; }
        public SubSector SubSector { get; set; }
        }
}