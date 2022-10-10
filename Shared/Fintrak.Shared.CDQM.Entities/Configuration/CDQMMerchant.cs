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

namespace Fintrak.Shared.CDQM.Entities
{
    public partial class CDQMMerchant : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int MerchantId { get; set; }

        [DataMember]
        [Required]
        public string SN { get; set; }

        [DataMember]
        [Required]
        public string BranchName { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }


        [DataMember]
        public string TeamCode { get; set; }

        [DataMember]
        public string SBU { get; set; }

        [DataMember]
        public string MerchantCode { get; set; }

        [DataMember]
        public string MerchantName { get; set; }

        [DataMember]
        public string ContactTitle { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string MobilePhone { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string PhysicalAddr { get; set; }

        [DataMember]
        public string TerminalID { get; set; }

        [DataMember]
        public string BankCode { get; set; }

        [DataMember]
        public string BankAccNo { get; set; }

        [DataMember]
        public string BankAccType { get; set; }

        [DataMember]
        public string BusinessOccupationCode { get; set; }

        [DataMember]
        public string MerchantCategoryCode { get; set; }

        [DataMember]
        public string StateCode { get; set; }

        [DataMember]
        public string VisaAcquireIDNumber { get; set; }

        [DataMember]
        public string VerveAcquireIDNumber { get; set; }

        [DataMember]
        public string MasterCardAcquireIDNumber { get; set; }

        [DataMember]
        public string TerminalOwnerCode { get; set; }

        public string LGALCDA { get; set; }
        [DataMember]
        public string PTASP { get; set; }

        public int EntityId
        {
            get
            {
                return MerchantId;
            }
        }
    }
}
