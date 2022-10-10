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
using System.Xml.Serialization;

namespace Fintrak.Shared.Basic.Entities
{
    public partial class AccountOfficerDetail : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int AccountOfficerDetailId { get; set; }

        [DataMember]
        [Required]
        public string MisCode { get; set; }

        [DataMember]
        public string StaffID { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return AccountOfficerDetailId;
            }
        }
    }
}
