using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MemoProductMapInfo
    {
        public MemoProductMap MemoProductMap { get; set; }
        public Product Product { get; set; }
        public MemoUnits MemoUnits { get; set; }
       
    }
}