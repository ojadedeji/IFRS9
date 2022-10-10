using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class NonProductMapInfo
    {
        public NonProductMap NonProductMap { get; set; }
        public Product NonProduct { get; set; }
        public Product Product { get; set; }
        public BSCaption BSCaption { get; set; }

    }
}