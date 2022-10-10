using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.Basic.Entities;

namespace Fintrak.Data.Basic.Contracts
{
    public class MemoAccountMapInfo
    {
        public MemoAccountMap MemoAccountMap { get; set; }
        public CustAccount CustAccount { get; set; }
        public MemoUnits MemoUnits { get; set; }
       
    }
}