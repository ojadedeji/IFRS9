using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class NonProductRateInfo
    {
        public NonProductRate NonProductRate { get; set; }
        public Product Product { get; set; }

    }
}