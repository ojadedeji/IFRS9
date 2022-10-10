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
    public partial class ProcessData : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int ProcessDataId { get; set; }

        [DataMember]
        public string AccountOfficerCode { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        [ReadOnly(true)]
        public string CurrencyCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string DRCR { get; set; }

        [DataMember]
        public string EntryUser { get; set; }

        [DataMember]
        public double ExchangeRate { get; set; }

        [DataMember]
        public string GLCode { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal LCYAmount { get; set; }

        [DataMember]
        public string MISCode { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string RCREUser { get; set; }

        [DataMember]
        public string RelatedAccount { get; set; }

        [DataMember]
        public string SUBGLCode { get; set; }

        [DataMember]
        public string TransCode { get; set; }

        [DataMember]
        public DateTime TransDate { get; set; }

        [DataMember]
        public string TransReference { get; set; }

        [DataMember]
        public string EventNo { get; set; }

        [DataMember]
        public string BatchNo { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public string TransClassification { get; set; }

        [DataMember]
        public string EntryStatus { get; set; }

        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return ProcessDataId;
            }
        }

    }
}
