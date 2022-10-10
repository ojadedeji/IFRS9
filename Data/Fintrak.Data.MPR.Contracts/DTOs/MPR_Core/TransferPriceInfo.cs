using System;
using System.Linq;
using Fintrak.Shared.MPR.Entities;
using Fintrak.Shared.Core.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class TransferPriceInfo
    {
        public TransferPrice TransferPrice { get; set; }
        public Product Product { get; set; }
        public BSCaption BSCaption { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }

    }
}