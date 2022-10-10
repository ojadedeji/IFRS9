using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.IFRS.Framework;
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
    public partial class InstrumentType : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int InstrumentTypeId { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public IFRSInstrument Instrument { get; set; }

         [DataMember]

        public int? ParentId { get; set; }

         [DataMember]
         public string CompanyCode { get; set; }
        
      
        public int EntityId
        {
            get
            {
                return InstrumentTypeId;
            }
        }
    }
}
