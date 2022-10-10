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

namespace Fintrak.Shared.SystemCore.Entities
{
    public partial class Database : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int DatabaseId { get; set; }

        [DataMember]
        [Required]
        public string Title { get; set; }


        [DataMember]
        [Required]
        public string DatabaseName { get; set; }


        [DataMember]
        [Required]
        public string ServerName { get; set; }

        [DataMember]
        [Required]
        public string ServiceServerName { get; set; }

        [DataMember]
        [Required]
        public string UserName { get; set; }

        [DataMember]
        [Required]
        public string Password { get; set; }

        [DataMember]
        [Required]
        public string IntegratedSecurity { get; set; }

        [DataMember]
        [Required]
        public string CompanyCode { get; set; }

        [DataMember]
        public int? SolutionId { get; set; }

        public int EntityId
        {
            get
            {
                return DatabaseId;
            }
        }

    }
}
