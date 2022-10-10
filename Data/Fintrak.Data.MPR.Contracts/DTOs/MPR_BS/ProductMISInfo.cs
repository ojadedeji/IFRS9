using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class ProductMISInfo
    {
        public ProductMIS ProductMIS { get; set; }
        public BSCaption Caption { get; set; }
        public Product Product { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
        public TeamDefinition AccountOfficerDefinition { get; set; }
        public Team AccountOfficer { get; set; }

    }
}