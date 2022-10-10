using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Common
{
    public class ModuleException : ApplicationException
    {
        public ModuleException(string message)
            : this(message, null)
        {
        }

        public ModuleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
