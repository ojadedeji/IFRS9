using System;
using System.Linq;
using Fintrak.Shared.SystemCore.Entities;

namespace Fintrak.Data.SystemCore.Contracts
{
    public class CompanyUserInfo
    {
        public CompanyUser CompanyUser { get; set; }

        public UserSetup UserSetup  { get; set; }
    }
}