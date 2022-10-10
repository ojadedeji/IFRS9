using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class NonProductRateInfo
    {
        public NonProductRate NonProductRate { get; set; }
        public Product Product { get; set; }

    }
}