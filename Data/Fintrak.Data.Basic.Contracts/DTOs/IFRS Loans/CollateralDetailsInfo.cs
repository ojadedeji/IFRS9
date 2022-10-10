using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class CollateralDetailsInfo
    {
        public CollateralInformation CollateralInformation { get; set; }
        public CollateralType CollateralType { get; set; }
        public CollateralCategory CollateralCategory { get; set; }

    }
}