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
    public partial class CollateralDetails : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public double? YOO { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public string HC1 { get; set; }

        [DataMember]
        public string HC2 { get; set; }

        [DataMember]
        public string ColType1 { get; set; }

        [DataMember]
        public string Col_Qualifier1 { get; set; }

        [DataMember]
        public string Perfection_Status1 { get; set; }

        [DataMember]
        public double? ColAmt1 { get; set; }

        [DataMember]
        public string ColType2 { get; set; }

        [DataMember]
        public string Col_Qualifier2 { get; set; }

        [DataMember]
        public string Perfection_Status2 { get; set; }

        [DataMember]
        public double? ColAmt2 { get; set; }

        [DataMember]
        public string ColType3 { get; set; }

        [DataMember]
        public string Col_Qualifier3 { get; set; }

        [DataMember]
        public string Perfection_Status3 { get; set; }

        [DataMember]
        public double? ColAmt3 { get; set; }

        [DataMember]
        public string ColType4 { get; set; }

        [DataMember]
        public string Col_Qualifier4 { get; set; }

        [DataMember]
        public string Perfection_Status4 { get; set; }

        [DataMember]
        public double? ColAmt4 { get; set; }

        [DataMember]
        public DateTime DateLoaded { get; set; }

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