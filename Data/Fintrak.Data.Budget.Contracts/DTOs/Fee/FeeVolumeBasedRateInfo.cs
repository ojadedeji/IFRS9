using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class FeeVolumeBasedRateInfo
    {
        public FeeVolumeBasedRate FeeVolumeBasedRate { get; set; }
        public FeeItem FeeItem { get; set; }
        public FeeCategory FeeCategory { get; set; }
        public TeamDefinition TeamDefinition { get; set; }
        public Team Team { get; set; }
    }
 
}