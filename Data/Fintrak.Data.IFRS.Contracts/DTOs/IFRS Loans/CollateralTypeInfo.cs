using System;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;

namespace Fintrak.Data.IFRS.Contracts
{
    public class CollateralTypeInfo
    {
        public CollateralType CollateralType { get; set; }
        public CollateralCategory CollateralCategory { get; set; }

    }
}