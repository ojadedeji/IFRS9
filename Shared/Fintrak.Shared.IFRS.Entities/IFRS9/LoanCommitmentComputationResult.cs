﻿using Fintrak.Shared.IFRS.Framework;
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
    public partial class LoanCommitmentComputationResult : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int LoanCommitmentComputationResult_Id { get; set; }

        [DataMember]
        public string Refno { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public DateTime date_pmt { get; set; }

        [DataMember]
        public int YTM { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string FacilityType { get; set; }

        [DataMember]
        public int Stage { get; set; }

        [DataMember]
        public int Seq { get; set; }

        [DataMember]
        public double AmortizedCost { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public double Exchange { get; set; }

        [DataMember]
        public double AmortizedCost_Trans { get; set; }

        [DataMember]
        public double LifetimePD { get; set; }

        [DataMember]
        public double DiscountFactor { get; set; }

        [DataMember]
        public double DiscountedCollateralValue { get; set; }

        [DataMember]
        public double LGD { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string CollateralStatus { get; set; }

        [DataMember]
        public double ECLOutput { get; set; }

        [DataMember]
        public double MacroEco_ECLOutput { get; set; }

        [DataMember]
        public double FinalECLWeightAvg { get; set; }

        [DataMember]
        public double MEcoECLBest { get; set; }

        [DataMember]
        public double MEcoECLOptimistic { get; set; }

        [DataMember]
        public double MEcoECLDownTurn { get; set; }

        [DataMember]
        public double FinalECLBest { get; set; }

        [DataMember]
        public double FinalECLOptimistic { get; set; }

        [DataMember]
        public double FinalECLDownTurn { get; set; }

        [DataMember]
        public DateTime Rundate { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return LoanCommitmentComputationResult_Id;
            }
        }
    }
}

