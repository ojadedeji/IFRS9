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
    public partial class TransactionDetail : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int TransactionDetailId { get; set; }


        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
       
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public decimal Amount { get; set; }


        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

  

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

  

        public int EntityId
        {
            get
            {
                return TransactionDetailId;
            }
        }
    }
}
