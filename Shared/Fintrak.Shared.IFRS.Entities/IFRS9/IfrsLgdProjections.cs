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
    public partial class IfrsLgdProjections : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int  ID { get; set; }

        [DataMember]
        [Required]
        public string AccountNo { get; set; }

        [DataMember]
   
        public string Refno { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string Scenerio { get; set; }

        [DataMember]
        public DateTime Processdate  { get; set; }

        [DataMember]
        public double EAD { get; set; }

        [DataMember]
        public string  Stage { get; set; }


        [DataMember]
        public double CollateralForecast { get; set; }

        [DataMember]
        public double CureRate { get; set; }

        [DataMember]
        public double RecoveryRate { get; set; }

        [DataMember]
        public double LGDProjection { get; set; }
        


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
