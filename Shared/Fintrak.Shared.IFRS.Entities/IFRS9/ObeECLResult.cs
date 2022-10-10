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
    public partial class ObeECLResult  : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }
        
        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string Producttype { get; set; }

        [DataMember]
        public string SubType { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double FinalECLBest { get; set; }
        [DataMember]
        public int? Stage { get; set; }

        [DataMember]
        public double? AmortizedCost { get; set; }


        [DataMember]
        public double PrincipalOutBal { get; set; }

        [DataMember]
        public double EIR { get; set; }


        [DataMember]
        public double FinalECLOptimistic { get; set; }

        [DataMember]
        public double FinalECLDownTurn { get; set; }

        [DataMember]
        public double FinalECLWeightAvg { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
