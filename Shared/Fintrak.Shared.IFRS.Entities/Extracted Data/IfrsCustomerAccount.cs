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
    public partial class IfrsCustomerAccount : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int CustAccountId { get; set; }

        [DataMember]
        public string CustomerNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string SubSector { get; set; }
       
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public bool IsDormant { get; set; }
        
        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return CustAccountId;
            }
        }
    }
}
