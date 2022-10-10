using System;
using System.Linq;
using System.Security.Principal;

namespace Fintrak.Shared.Common.Security
{
    public interface ICustomPrincipal : IPrincipal
    {
        string CompanyCode { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role) { return false; }

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email);
        }

        public string CompanyCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
