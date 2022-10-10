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
    public partial class MacroEconomicForeCast : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public DateTime Period { get; set; }

        [DataMember]
        [Required]
        public int  Coefficient { get; set; }

        [DataMember]
        [Required]
        public double Baseline_InflationRate { get; set; }

       [DataMember]
        [Required]
        public double Baseline_ExchangeRate { get; set; }


        [DataMember]
        [Required]
        public double Best_InflationRate { get; set; }

        [DataMember]
        [Required]
        public double Best_ExchangeRate { get; set; }


        [DataMember]
       public double Worst_InflationRate { get; set; }

       [DataMember]
        public double Worst_ExchangeRate { get; set; }

  

   



    //[DataMember]
    //public string CompanyCode { get; set; }


    [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ID;
            }
        }
    }
}
