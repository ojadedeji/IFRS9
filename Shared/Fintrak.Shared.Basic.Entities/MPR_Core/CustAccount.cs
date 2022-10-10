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
    public partial class CustAccount : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CustAccountId { get; set; }

        [DataMember]
        
        public string CustNo { get; set; }

        [DataMember]
        
        public string AccountNo { get; set; }

        [DataMember]
        
        public string AccountName { get; set; }

        //[DataMember]
        
        //public string CustName { get; set; }

        [DataMember]
        
        public string Sector { get; set; }

        [DataMember]
        
        public string SubSector { get; set; }

        [DataMember]
        
        public string TeamCode { get; set; }

    

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        
        public string ProductCode { get; set; }

        [DataMember]
        
        public string BranchCode { get; set; }

        [DataMember]
        
        public string Currency { get; set; }

        [DataMember]
        
        public DateTime DateOpened { get; set; }

        [DataMember]
        
        public string Status { get; set; }

        [DataMember]
        
        public string SettlementAcct { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return CustAccountId;
            }
        }
    }
}
