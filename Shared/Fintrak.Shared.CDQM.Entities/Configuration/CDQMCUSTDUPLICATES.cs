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
    public partial class CDQMCustomerDuplicate : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CUST_DUPLICATES_ID { get; set; }

        [DataMember]
        public string COD_CUST_ID { get; set; }

        [DataMember]
        public string COD_GROUP_ID { get; set; }

        [DataMember]
        public string NAM_GROUP_NAME { get; set; }

        [DataMember]
        public string NAM_CUST_FULL { get; set; }

        [DataMember]
        public string DAT_BIRTH_CUST { get; set; }

        [DataMember]
        public string TXT_CUST_SEX { get; set; }

        [DataMember]
        public double Score { get; set; }

        [DataMember]
        public bool NotDuplicate { get; set; }

        public int EntityId
        {
            get
            {
                return CUST_DUPLICATES_ID;
            }
        }
    }
}
