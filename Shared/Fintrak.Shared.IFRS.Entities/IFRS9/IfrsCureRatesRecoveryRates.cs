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
    public partial class IfrsCureRatesRecoveryRates : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int  ID { get; set; }

        [DataMember]
        [Required]
        public string ProductType { get; set; }



        [DataMember]
        [Required]
        public double CureRate { get; set; }


        [DataMember]
        [Required]
        public double RecoveryRate { get; set; }

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }


        //[DataMember]
        //[Required]
        //public string Rating { get; set; }

        //[DataMember]
        //[Required]
        //public double CRRG1 { get; set; }


        //[DataMember]
        //[Required]
        //public double CRRG2 { get; set; }


        //[DataMember]
        //[Required]
        //public double CRRG3 { get; set; }


        //[DataMember]
        //[Required]
        //public double CRRG4 { get; set; }


        //[DataMember]
        //[Required]
        //public double RRRG1 { get; set; }


        //[DataMember]
        //[Required]
        //public double RRRG2 { get; set; }


        //[DataMember]
        //[Required]
        //public double RRRG3 { get; set; }



        //[DataMember]
        //[Required]
        //public double RRRG4 { get; set; }



        //[DataMember]
        //[Required]
        //public double CUMCRRG { get; set; }


        //[DataMember]
        //[Required]
        //public double CUMRRG34  { get; set; }


        //[DataMember]
        //[Required]
        //public double CUMRRG { get; set; }





        //[DataMember]
        //[Required]
        //public double ADJCureRate { get; set; }


        //[DataMember]
        //[Required]
        //public double ADJRecoveryRate { get; set; }



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
