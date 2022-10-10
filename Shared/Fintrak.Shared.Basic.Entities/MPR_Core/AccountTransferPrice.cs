using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
using Fintrak.Shared.Core.Framework;
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
    public partial class AccountTransferPrice : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int AccountTransferPriceId { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        [Required]
        public double Rate { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        [Required]
        public string Period { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return AccountTransferPriceId;
            }
        }

    }
}
