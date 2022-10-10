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
    public partial class HistoricalDefaultFreq : EntityBase, IIdentifiableEntity
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

        [DataMember]
        public double OutBalAfter_Yr1 { get; set; }

        [DataMember]
        public double OutBalAfter_Yr2 { get; set; }

        [DataMember]
        public double OutBalAfter_Yr3 { get; set; }

        [DataMember]
        public double OutBalAfter_Yr4 { get; set; }

        [DataMember]
        public double OutBalAfter_Yr5 { get; set; }


        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
