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
    public partial class IfrsCustomer : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int CustomerId { get; set; }

        [DataMember]
        public string CustomerNo { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustType { get; set; }

        [DataMember]
        public string CreditRating { get; set; }

        [DataMember]
        public string Country { get; set; }
       
        [DataMember]
        public string CustCategory { get; set; }


        [DataMember]
        public string LGD_Type { get; set; }
        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return CustomerId;
            }
        }
    }
}
