using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Core.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.Core.Entities
{
    public partial class Processes : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ProcessId { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }

        [DataMember]
        [Required]
        public PackageRunType RunType { get; set; }

        [DataMember]
        [Required]
        public string PackageName { get; set; }

        [DataMember]
        [Required]
        public string PackagePath { get; set; }

  

        [DataMember]
        [Required]
        public int ModuleId { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

      

        public int EntityId
        {
            get
            {
                return ProcessId;
            }
        }
    }
}
