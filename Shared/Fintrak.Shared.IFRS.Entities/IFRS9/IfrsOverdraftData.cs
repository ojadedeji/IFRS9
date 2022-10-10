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
    public partial class IfrsOverdraftData : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
    
         public string AccountNo { get; set; }

        [Required]
        public string RefNo { get; set; }


        [DataMember]
        [Required]
        public string CustomerName { get; set; }

        [DataMember]
        [Required]
        public string ProductType { get; set; }


        [DataMember]
        [Required]
        public string SubType { get; set; }


        [DataMember]
        [Required]
        public DateTime ValueDate { get; set; }



        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }


        [DataMember]
        [Required]
        public string Currency { get; set; }



        [DataMember]
        [Required]
        public double ExchangeRate { get; set; }

        [DataMember]
        [Required]
        public double ODLimit { get; set; }


        [DataMember]
        [Required]
        public double  DrawnAmount { get; set; }


        [DataMember]
        [Required]
        public double Rate { get; set; }

        [DataMember]
        [Required]
        public int Stage { get; set; }


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
