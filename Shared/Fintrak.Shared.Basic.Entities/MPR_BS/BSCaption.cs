using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using Fintrak.Shared.Basic.Framework;
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

namespace Fintrak.Shared.Basic.Entities
{
    public partial class BSCaption : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int CaptionId { get; set; }

        [DataMember]
        [Required]
        public string CaptionCode { get; set; }

        [DataMember]
        [Required]
        public string CaptionName { get; set; }

        [DataMember]
        [Required]
        public AccountTypeEnum Category { get; set; }

        [DataMember]
        [Required]
        public CurrencyType CurrencyType { get; set; }

        [DataMember]
        [Required]
        public BalanceSheetType BalanceSheetType { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public string PLCaption { get; set; }

        [DataMember]
        public int? ParentId { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }
      
        public int EntityId
        {
            get
            {
                return CaptionId;
            }
        }

    }
}
