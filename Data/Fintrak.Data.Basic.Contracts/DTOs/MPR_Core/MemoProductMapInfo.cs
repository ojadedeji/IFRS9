using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class MemoProductMapInfo
    {
        public MemoProductMap MemoProductMap { get; set; }
        public Product Product { get; set; }
        public MemoUnits MemoUnits { get; set; }
       
    }
}