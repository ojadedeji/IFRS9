using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class MarkovMatrixInfo
    {
        public MarkovMatrix MarkovMatrix { get; set; }
        public Sector Sector { get; set; }
    }
}