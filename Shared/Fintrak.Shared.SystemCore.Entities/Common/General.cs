using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fintrak.Shared.SystemCore.Entities
{
    public partial class General : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int GeneralId { get; set; }

        [DataMember]
        public string Host { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool EnableCompanyDefaultLogin { get; set; }

        [DataMember]
        public string DefaultCompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return GeneralId;
            }
        }
    }
}
