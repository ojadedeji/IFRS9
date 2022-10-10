using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class FeeVolumeBasedSetupInfo
    {
        public FeeVolumeBasedSetup FeeVolumeBasedSetup { get; set; }
        public FeeItem FeeItem { get; set; }
        public FeeCategory FeeCategory { get; set; }
        public Product Product { get; set; }
    }
}