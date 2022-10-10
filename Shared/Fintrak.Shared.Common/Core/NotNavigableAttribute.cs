using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Shared.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNavigableAttribute : Attribute
    {
    }
}
