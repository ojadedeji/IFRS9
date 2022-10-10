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

namespace Fintrak.Shared.Core.Entities
{
    public partial class PayGrade : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int PayGradeId { get; set; }

        [DataMember]
        [Required]
        public string Code { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        public decimal GrossPay { get; set; }

        [DataMember]
        public decimal NetPay { get; set; }

        [DataMember]
        public decimal ThirteenthMonth { get; set; }

       
        public int EntityId
        {
            get
            {
                return PayGradeId;
            }
        }
    }
}
