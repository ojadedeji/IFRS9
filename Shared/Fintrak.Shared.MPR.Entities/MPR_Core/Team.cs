using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.MPR.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fintrak.Shared.MPR.Entities
{
    public partial class Team : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int TeamId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public string ParentCode { get; set; }

        [DataMember]
        [Required]
        public string DefinitionCode { get; set; }

        [DataMember]
        public string StaffId { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public ModuleOwnerType ModuleOwnerType { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        public int EntityId
        {
            get
            {
                return TeamId;
            }
        }
    }
}
