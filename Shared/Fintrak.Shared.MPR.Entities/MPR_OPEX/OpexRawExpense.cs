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
    public partial class OpexRawExpense : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int OpexRawExpenseId { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public DateTime PostDate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CheckMisCode { get; set; }

        [DataMember]
        public string MisCode { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string TranID { get; set; }

        [DataMember]
        public string SubGLCode { get; set; }

        [DataMember]
        public double DR { get; set; }

        [DataMember]
        public double CR { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public string CompanyCode { get; set; }

        public int EntityId
        {
            get
            {
                return OpexRawExpenseId;
            }
        }

    }
}
