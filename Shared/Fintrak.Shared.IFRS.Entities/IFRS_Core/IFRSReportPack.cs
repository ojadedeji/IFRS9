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
    public partial class IFRSReportPack : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ReportPackId { get; set; }

        [DataMember]
        [Required]
        public string ReportName { get; set; }

        [DataMember]
        [Required]
        public string ReportDescription { get; set; }

        [DataMember]
        [Required]
        public int SolutionId { get; set; }

         [DataMember]
         public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return ReportPackId;
            }
        }
    }
}
