using Fintrak.Shared.IFRS.Framework;
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

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class IfrsLoanMissPayment : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string DefaultType { get; set; }

        [DataMember]
        [Required]
        public int DefaultParam { get; set; }

        [DataMember]
        [Required]
        public int DaysPastDue { get; set; }

      
 



    //[DataMember]
    //public string CompanyCode { get; set; }


    [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
