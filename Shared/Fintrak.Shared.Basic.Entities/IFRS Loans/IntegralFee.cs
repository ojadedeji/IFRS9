using Fintrak.Shared.Basic.Framework;
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
    public partial class IntegralFee : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        public string AccountNo { get; set; }
   
        [DataMember]
        public string RefNo { get; set; }

 
        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public double FeeAmount { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int? FeeCircle { get; set; }

        [DataMember]
        public int? ExpiredPeriod { get; set; }

        [DataMember]
        public int? UnExpiredPeriod { get; set; }       

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public int? Period { get; set; }

        [DataMember]
        public DateTime? RunDate { get; set; } 

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
