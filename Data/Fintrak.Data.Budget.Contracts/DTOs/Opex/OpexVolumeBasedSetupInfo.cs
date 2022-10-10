using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class OpexVolumeBasedSetupInfo
    {
        public OpexVolumeBasedSetup OpexVolumeBasedSetup { get; set; }
        public OpexItem OpexItem { get; set; }
        public OpexCategory OpexCategory { get; set; }
        public Product Product { get; set; }
    }
}