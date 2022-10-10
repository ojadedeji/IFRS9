using Fintrak.Shared.SystemCore.Entities;
using System;
using System.Linq;

namespace Fintrak.Data.SystemCore.Contracts
{
  public  class MenuInfo
    {
        public Menu Menu { get; set; }
        public Module Module { get; set; }
        public Menu Parent { get; set; }
    }
}
