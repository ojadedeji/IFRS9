using Fintrak.Shared.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;

namespace Fintrak.Shared.Common.ServiceModel
{
    public abstract class UserClientBase<T> : ClientBase<T> where T : class
    {
        public UserClientBase()
        {
            string userName = Thread.CurrentPrincipal.Identity.Name;
            string companyCode = string.Empty;

            if (HttpContext.Current.User != null)
            {
                var user = HttpContext.Current.User as CustomPrincipal;
                if (user != null)
                    companyCode = user.CompanyCode;
            }

            MessageHeader<string> userNameHeader = new MessageHeader<string>(userName);

            MessageHeader<string> companyHeader = new MessageHeader<string>(companyCode);

            OperationContextScope contextScope =  new OperationContextScope(InnerChannel);

            OperationContext.Current.OutgoingMessageHeaders.Add(userNameHeader.GetUntypedHeader("String", "System"));
            OperationContext.Current.OutgoingMessageHeaders.Add(companyHeader.GetUntypedHeader("Company", "System"));
        }
    }
}
