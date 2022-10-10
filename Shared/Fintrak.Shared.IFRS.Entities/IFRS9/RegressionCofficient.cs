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
    public partial class RegressionCofficient : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember]
        [Required]
        public string ProductType { get; set; }

        [DataMember]
        [Required]
        public string ParameterType { get; set; }

        [DataMember]
        [Required]
        public double Alpa { get; set; }

        [DataMember]
        [Required]
        public double Beta1 { get; set; }


        [DataMember]
        [Required]
        public double Beta2 { get; set; }

        [DataMember]
        [Required]
        public double  Beta3 { get; set; }

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
