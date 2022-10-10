using System;
using System.Linq;
using Fintrak.Shared.Core.Entities;
using Fintrak.Shared.MPR.Entities;

namespace Fintrak.Data.MPR.Contracts
{
    public class MemoAccountMapInfo
    {
        public MemoAccountMap MemoAccountMap { get; set; }
        public CustAccount CustAccount { get; set; }
        public MemoUnits MemoUnits { get; set; }
       
    }
}