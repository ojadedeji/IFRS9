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
    public partial class MarginalCCFPivotSTRLB : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember] public string OBEType { get; set; }
        [DataMember] public string COL { get; set; }

        [DataMember] public double MCCF0 { get; set; }
        [DataMember] public double MCCF1 { get; set; }
        [DataMember] public double MCCF2 { get; set; }
        [DataMember] public double MCCF3 { get; set; }
        [DataMember] public double MCCF4 { get; set; }
        [DataMember] public double MCCF5 { get; set; }
        [DataMember] public double MCCF6 { get; set; }
        [DataMember] public double MCCF7 { get; set; }
        [DataMember] public double MCCF8 { get; set; }
        [DataMember] public double MCCF9 { get; set; }
        [DataMember] public double MCCF10 { get; set; }
        [DataMember] public double MCCF11 { get; set; }
        [DataMember] public double MCCF12 { get; set; }
        [DataMember] public double MCCF13 { get; set; }
        [DataMember] public double MCCF14 { get; set; }
        [DataMember] public double MCCF15 { get; set; }
        [DataMember] public double MCCF16 { get; set; }
        [DataMember] public double MCCF17 { get; set; }
        [DataMember] public double MCCF18 { get; set; }
        [DataMember] public double MCCF19 { get; set; }
        [DataMember] public double MCCF20 { get; set; }
        [DataMember] public double MCCF21 { get; set; }
        [DataMember] public double MCCF22 { get; set; }
        [DataMember] public double MCCF23 { get; set; }
        [DataMember] public double MCCF24 { get; set; }
        [DataMember] public double MCCF25 { get; set; }
        [DataMember] public double MCCF26 { get; set; }
        [DataMember] public double MCCF27 { get; set; }
        [DataMember] public double MCCF28 { get; set; }
        [DataMember] public double MCCF29 { get; set; }
        [DataMember] public double MCCF30 { get; set; }
        [DataMember] public double MCCF31 { get; set; }
        [DataMember] public double MCCF32 { get; set; }
        [DataMember] public double MCCF33 { get; set; }
        [DataMember] public double MCCF34 { get; set; }
        [DataMember] public double MCCF35 { get; set; }
        [DataMember] public double MCCF36 { get; set; }
        [DataMember] public double MCCF37 { get; set; }
        [DataMember] public double MCCF38 { get; set; }
        [DataMember] public double MCCF39 { get; set; }
        [DataMember] public double MCCF40 { get; set; }
        [DataMember] public double MCCF41 { get; set; }
        [DataMember] public double MCCF42 { get; set; }
        [DataMember] public double MCCF43 { get; set; }
        [DataMember] public double MCCF44 { get; set; }
        [DataMember] public double MCCF45 { get; set; }
        [DataMember] public double MCCF46 { get; set; }
        [DataMember] public double MCCF47 { get; set; }
        [DataMember] public double MCCF48 { get; set; }
        [DataMember] public double MCCF49 { get; set; }
        [DataMember] public double MCCF50 { get; set; }
        [DataMember] public double MCCF51 { get; set; }
        [DataMember] public double MCCF52 { get; set; }
        [DataMember] public double MCCF53 { get; set; }
        [DataMember] public double MCCF54 { get; set; }
        [DataMember] public double MCCF55 { get; set; }
        [DataMember] public double MCCF56 { get; set; }
        [DataMember] public double MCCF57 { get; set; }
        [DataMember] public double MCCF58 { get; set; }
        [DataMember] public double MCCF59 { get; set; }
        [DataMember] public double MCCF60 { get; set; }
        [DataMember] public double MCCF61 { get; set; }
        [DataMember] public double MCCF62 { get; set; }
        [DataMember] public double MCCF63 { get; set; }
        [DataMember] public double MCCF64 { get; set; }
        [DataMember] public double MCCF65 { get; set; }
        [DataMember] public double MCCF66 { get; set; }
        [DataMember] public double MCCF67 { get; set; }
        [DataMember] public double MCCF68 { get; set; }
        [DataMember] public double MCCF69 { get; set; }
        [DataMember] public double MCCF70 { get; set; }
        [DataMember] public double MCCF71 { get; set; }
        [DataMember] public double MCCF72 { get; set; }
        [DataMember] public double MCCF73 { get; set; }
        [DataMember] public double MCCF74 { get; set; }
        [DataMember] public double MCCF75 { get; set; }
        [DataMember] public double MCCF76 { get; set; }
        [DataMember] public double MCCF77 { get; set; }
        [DataMember] public double MCCF78 { get; set; }
        [DataMember] public double MCCF79 { get; set; }
        [DataMember] public double MCCF80 { get; set; }
        [DataMember] public double MCCF81 { get; set; }
        [DataMember] public double MCCF82 { get; set; }
        [DataMember] public double MCCF83 { get; set; }
        [DataMember] public double MCCF84 { get; set; }
        [DataMember] public double MCCF85 { get; set; }
        [DataMember] public double MCCF86 { get; set; }
        [DataMember] public double MCCF87 { get; set; }
        [DataMember] public double MCCF88 { get; set; }
        [DataMember] public double MCCF89 { get; set; }
        [DataMember] public double MCCF90 { get; set; }


        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
