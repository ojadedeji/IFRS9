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
    public partial class CollateralRealizationPeriod : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CollateralRealizationPeriodId { get; set; }

        [DataMember]
        [Required]
        public string TypeCode { get; set; }

        [DataMember]
        public int Duration { get; set; }

        [DataMember]
        public double AvgRecoveryCost { get; set; }

        [DataMember]
        public double HairCut { get; set; }

        [DataMember]
        public double Inflation { get; set; }

        [DataMember]
        public double Depreciation { get; set; }

        [DataMember]
        public double? GrowthRate { get; set; }

        [DataMember]
        public double TimeToRecovery { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return CollateralRealizationPeriodId;
            }
        }
    }
}
