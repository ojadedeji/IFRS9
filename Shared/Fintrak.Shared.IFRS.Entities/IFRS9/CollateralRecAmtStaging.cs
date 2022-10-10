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
    public partial class CollateralRecAmtStaging  : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public string CollateralDescription { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public double ColAmt1 { get; set; }

        [DataMember]
        public double ColAmt2 { get; set; }

        [DataMember]
        public double ColAmt3 { get; set; }

        [DataMember]
        public double ColAmt4 { get; set; }

        [DataMember]
        public string MappedColType1 { get; set; }

        [DataMember]
        public string MappedColType2 { get; set; }

        [DataMember]
        public string MappedColType3 { get; set; }

        [DataMember]
        public string MappedColType4 { get; set; }

        [DataMember]
        public double ColRecAmt1 { get; set; }

        [DataMember]
        public double ColRecAmt2 { get; set; }

        [DataMember]
        public double ColRecAmt3 { get; set; }

        [DataMember]
        public double ColRecAmt4 { get; set; }

        [DataMember]
        public double growthrate1 { get; set; }

        [DataMember]
        public double growthrate2 { get; set; }

        [DataMember]
        public double growthrate3 { get; set; }

        [DataMember]
        public double growthrate4 { get; set; }

        [DataMember]
        public double finCol1 { get; set; }

        [DataMember]
        public double finCol2 { get; set; }

        [DataMember]
        public double finCol3 { get; set; }

        [DataMember]
        public double finCol4 { get; set; }

        [DataMember]
        public double TotalColRecAmt { get; set; }

        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
