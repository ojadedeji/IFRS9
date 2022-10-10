using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Core.Framework
{
    public enum PackageStatus 
    {
        Done = 1,
        Pending = 2,
        Running = 3,
        Cancel = 4,
        New = 5,
        Fail = 6,
        Stop = 7,
        Removed = 8
    }
}
