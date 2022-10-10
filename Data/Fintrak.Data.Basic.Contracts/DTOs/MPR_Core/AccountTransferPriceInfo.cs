using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class AccountTransferPriceInfo
    {
        public AccountTransferPrice AccountTransferPrice { get; set; }
        public CustAccount CustAccount { get; set; }

    }
}