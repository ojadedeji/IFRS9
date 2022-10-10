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
    public partial class FairValueBasisMap : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int FairValueBasisMapId { get; set; }

        [DataMember]
        [Required]
        public IFRSInstrument InstrumentType { get; set; }

        [DataMember]
        [Required]
        public FIClassification Classification { get; set; }

        [DataMember]
        [Required]
        public int BasisLevel { get; set; } //1,2,3

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return FairValueBasisMapId;
            }
        }
    }
}
