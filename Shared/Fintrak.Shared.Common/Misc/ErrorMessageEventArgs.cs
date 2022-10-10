using System;
using System.Collections.Generic;
using System.Linq;

namespace Fintrak.Shared.Common
{
    public class ErrorMessageEventArgs : EventArgs
    {
        public ErrorMessageEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}
