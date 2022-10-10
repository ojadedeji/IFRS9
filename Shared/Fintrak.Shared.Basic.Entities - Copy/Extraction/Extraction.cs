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
    public partial class Extraction : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ExtractionId { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public string PackageName { get; set; }

        [DataMember]
        public string PackagePath { get; set; }

        [DataMember]
        public string ProcedureName { get; set; }

        [DataMember]
        [XmlIgnore]
        public string Script { get; set; }

        [DataMember]
        public int SolutionId { get; set; }

      
        public int EntityId
        {
            get
            {
                return ExtractionId;
            }
        }
    }
}
