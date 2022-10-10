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
    public partial class AccountMIS : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int AccountMISId { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string TeamDefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string TeamCode { get; set; }

        [DataMember]
        public string AccountOfficerDefinitionCode { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

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
                return AccountMISId;
            }
        }
    }
}
