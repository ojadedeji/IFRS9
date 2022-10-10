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
    public partial class ChartOfAccount : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int ChartOfAccountId { get; set; }

        [DataMember]
        [Required]
        public AccountTypeEnum AccountType { get; set; }

        [DataMember]
        [Required]
        public string AccountCode { get; set; }

        [DataMember]
        [Required]
        public string AccountName { get; set; }

        [DataMember]
        [Required]
        public int FinancialTypeId { get; set; }

        [DataMember]
        
        public int? ParentId { get; set; }

        [DataMember]
        [Required]
        public string IFRS { get; set; }

        [DataMember]
        [Required]
        public int Position { get; set; }

      
        public int EntityId
        {
            get
            {
                return ChartOfAccountId;
            }
        }
    }
}
