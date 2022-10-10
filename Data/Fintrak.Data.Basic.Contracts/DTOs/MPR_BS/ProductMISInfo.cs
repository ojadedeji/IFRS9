using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
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