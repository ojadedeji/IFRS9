using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class AccountTransferPriceInfo
    {
        public AccountTransferPrice AccountTransferPrice { get; set; }
        public CustAccount CustAccount { get; set; }

    }
}