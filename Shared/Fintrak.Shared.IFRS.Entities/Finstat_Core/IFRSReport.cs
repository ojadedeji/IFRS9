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
    public partial class IFRSReport : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int IFRSReportId { get; set; }


        [DataMember]
        [Required]
        public string GLCode { get; set; }

        [DataMember]
        public string BranchCode { get; set; }



        [DataMember]
        [Required]
        public string Currency { get; set; }

        [DataMember]
        [Required]
        public double Amount { get; set; }



        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

  

        [DataMember]
        [Required]
        public DateTime RunDate { get; set; }

  

        public int EntityId
        {
            get
            {
                return IFRSReportId;
            }
        }
    }
}
