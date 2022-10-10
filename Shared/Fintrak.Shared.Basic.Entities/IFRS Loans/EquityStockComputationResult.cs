using Fintrak.Shared.Basic.Framework;
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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class EquityStockComputationResult : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int Id { get; set; }

        [DataMember]
        [Required]
        public int RefNo { get; set; }

        [DataMember]
        [Required]
        public string PortfolioID { get; set; }

        [DataMember]
        public string Portfolio { get; set; }

        [DataMember]
        public string ID_PortfolioGroup { get; set; }

        [DataMember]
        public string ID_PortfolioGroupName { get; set; }

        [DataMember]
        public string StockDescription { get; set; }

        [DataMember]
        public double Cost { get; set; }

        [DataMember]
        public double MarketQty { get; set; }

        [DataMember]
        public double MarketPrice { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public bool  Quoted { get; set; }

        [DataMember]
        public double FairValue { get; set; }

        [DataMember]
        public double FairValueGainLoss { get; set; }

        [DataMember]
        public int FairValueBasis { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public int Period { get; set; }

        [DataMember]
        public int Year { get; set; }


        
        [DataMember]
        public DateTime RunDate { get; set; }


        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
