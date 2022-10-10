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
    public partial class CcfAnalysisOverDraftSTRLB : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember] public int Seq { get; set; }
        [DataMember] public string CCFType { get; set; }
        [DataMember] public double InitialCCF { get; set; }
        [DataMember] public double CCF { get; set; }
        [DataMember] public double MonthlyCCF { get; set; }
        [DataMember] public double CumMonthlyCCF { get; set; }
        [DataMember] public double MarginalCCF { get; set; }

        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
