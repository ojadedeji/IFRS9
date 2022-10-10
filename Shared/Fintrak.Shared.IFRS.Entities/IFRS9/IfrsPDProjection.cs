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
    public partial class IfrsPDProjection : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int  ID { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
        [Required]
        public string Refno { get; set; }


        [DataMember]
        [Required]
        public string ProductName { get; set; }

        [DataMember]
        [Required]
        public string ProductType { get; set; }

        [DataMember]
        [Required]
        public string Sector { get; set; }

        [DataMember]
        [Required]
        public string Scenerio { get; set; }

        [DataMember]
        [Required]
        public double EAD { get; set; }


        [DataMember]
        public double EIR { get; set; }

    
        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public string Stage { get; set; }


        [DataMember]
        public double PD { get; set; }



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
