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
    public partial class SetUp : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int SetupId { get; set; }

        [DataMember]
        [Required]
        public string ExcoDefinitionCode { get; set; }

        [DataMember]
        [Required]
        public string ExcoTeamCode { get; set; }

        [DataMember]
        [Required]
        public int AccountLenght { get; set; }

        [DataMember]
        [Required]
        public PoolOption PoolOption { get; set; }

        [DataMember]
        [Required]
        public string Year { get; set; }

        [DataMember]
        public Nullable<int> Period { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public string SwithMode { get; set; }

        [DataMember]
        public Nullable<int> LevelId { get; set; }

        public int EntityId
        {
            get
            {
                return SetupId;
            }
        }
    }
}
