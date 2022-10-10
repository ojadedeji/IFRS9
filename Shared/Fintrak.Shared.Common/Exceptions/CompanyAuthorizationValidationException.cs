using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.Common
{
    public class CompanyAuthorizationValidationException : ApplicationException
    {
        public CompanyAuthorizationValidationException(string message)
            : this(message, null)
        {
        }

        public CompanyAuthorizationValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
