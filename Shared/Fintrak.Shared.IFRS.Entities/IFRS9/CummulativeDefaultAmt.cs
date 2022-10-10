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
    public partial class CummulativeDefaultAmt  : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string sub_type { get; set; }

        [DataMember]
        public int Origin_yr { get; set; }

        [DataMember]
        public double AdjBal { get; set; }

        [DataMember]
        public double OutBalAfter_yr { get; set; }

        [DataMember]
        public double OutBalAfter_yr1 { get; set; }

        [DataMember]
        public double OutBalAfter_yr2 { get; set; }

        [DataMember]
        public double OutBalAfter_yr3 { get; set; }

        [DataMember]
        public double OutBalAfter_yr4 { get; set; }

        [DataMember]
        public double OutBalAfter_yr5 { get; set; }

        [DataMember]
        public double OutBalAfter_yr6 { get; set; }

        public bool Active { get; set; }

            public int EntityId{
                get{
                    return Id;
                }
            }
        }
    }
