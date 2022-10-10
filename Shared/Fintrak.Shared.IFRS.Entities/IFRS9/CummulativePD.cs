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
    public partial class CummulativePD : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string Sub_type { get; set; }

        [DataMember]
        public int Origin_Yr { get; set; }

        [DataMember]
        public double AdjBal { get; set; }

        [DataMember]
        public double OutBalAfter_Yr { get; set; }

        [DataMember] public double OutBalAfter_Yr1 { get; set; }
        [DataMember] public double OutBalAfter_Yr2 { get; set; }
        [DataMember] public double OutBalAfter_Yr3 { get; set; }
        [DataMember] public double OutBalAfter_Yr4 { get; set; }
        [DataMember] public double OutBalAfter_Yr5 { get; set; }
        [DataMember] public double OutBalAfter_Yr6 { get; set; }
        [DataMember] public double OutBalAfter_Yr7 { get; set; }
        [DataMember] public double OutBalAfter_Yr8 { get; set; }
        [DataMember] public double OutBalAfter_Yr9 { get; set; }
        [DataMember] public double OutBalAfter_Yr10 { get; set; }
        [DataMember] public double OutBalAfter_Yr11 { get; set; }
        [DataMember] public double OutBalAfter_Yr12 { get; set; }
        [DataMember] public double OutBalAfter_Yr13 { get; set; }
        [DataMember] public double OutBalAfter_Yr14 { get; set; }
        [DataMember] public double OutBalAfter_Yr15 { get; set; }
        [DataMember] public double OutBalAfter_Yr16 { get; set; }
        [DataMember] public double OutBalAfter_Yr17 { get; set; }
        [DataMember] public double OutBalAfter_Yr18 { get; set; }
        [DataMember] public double OutBalAfter_Yr19 { get; set; }
        [DataMember] public double OutBalAfter_Yr20 { get; set; }
        [DataMember] public double OutBalAfter_Yr21 { get; set; }
        [DataMember] public double OutBalAfter_Yr22 { get; set; }
        [DataMember] public double OutBalAfter_Yr23 { get; set; }
        [DataMember] public double OutBalAfter_Yr24 { get; set; }
        [DataMember] public double OutBalAfter_Yr25 { get; set; }
        [DataMember] public double OutBalAfter_Yr26 { get; set; }
        [DataMember] public double OutBalAfter_Yr27 { get; set; }
        [DataMember] public double OutBalAfter_Yr28 { get; set; }
        [DataMember] public double OutBalAfter_Yr29 { get; set; }
        [DataMember] public double OutBalAfter_Yr30 { get; set; }
        [DataMember] public double OutBalAfter_Yr31 { get; set; }
        [DataMember] public double OutBalAfter_Yr32 { get; set; }
        [DataMember] public double OutBalAfter_Yr33 { get; set; }
        [DataMember] public double OutBalAfter_Yr34 { get; set; }
        [DataMember] public double OutBalAfter_Yr35 { get; set; }
        [DataMember] public double OutBalAfter_Yr36 { get; set; }
        [DataMember] public double OutBalAfter_Yr37 { get; set; }
        [DataMember] public double OutBalAfter_Yr38 { get; set; }
        [DataMember] public double OutBalAfter_Yr39 { get; set; }
        [DataMember] public double OutBalAfter_Yr40 { get; set; }


        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
