using System;
using System.Linq;
using Fintrak.Shared.Budget.Entities;

namespace Fintrak.Data.Budget.Contracts
{
    public class ProductVolumeBasedSetupInfo
    {
        public ProductVolumeBasedSetup ProductVolumeBasedSetup { get; set; }
        public Product Product { get; set; }
        public Product MakeUp { get; set; }
    }
}