using Fintrak.Shared.Common.Contracts;
using Fintrak.Shared.Common.Core;
using System;
using Fintrak.Shared.MPR.Framework;
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
    public partial class MPRCommFee : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable (false)]
        public int CommFee_Id { get; set; }

        [DataMember]
        public string Miscode { get; set; }

        [DataMember]
        public string AccountOfficer_Code { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string Inc_Exp { get; set; }

        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public string CurrencyType { get; set; }

        [DataMember]
        public string GL_Code { get; set; }

        [DataMember]
        public string RelatedAccount { get; set; }

        [DataMember]
        public string Narrative { get; set; }

        [DataMember]
        public int? Period { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public DateTime? P_Date { get; set; }

        [DataMember]
        public string Caption { get; set; }

        [DataMember]
        public string GroupCaption { get; set; }

        [DataMember]
        public string Tran_ID { get; set; }

        [DataMember]
        public DateTime? Tran_Date { get; set; }

        [DataMember]
        public string GLName { get; set; }

        [DataMember]
        public string EntryStatus { get; set; }

        public decimal? Rate { get; set; }

        [DataMember]

        public decimal? Raw_Amt { get; set; }

        [DataMember]

        public string Sub_Head_GL_Code { get; set; }

        [DataMember]

        public string ProductCode { get; set; }

        [DataMember]

        public string Trans_Code { get; set; }

        [DataMember]

        public string Ref_Num { get; set; }

        [DataMember]

        public string Rcre_User_Id { get; set; }

        [DataMember]

        public string Entry_User_Id { get; set; }

        [DataMember]

        public string BrokerCode { get; set; }

        [DataMember]

        public string RemapCaption { get; set; }

        [DataMember]

        public string RemapGroupName { get; set; }

        [DataMember]

        public int? IsMT { get; set; }

        [DataMember]

        public string MainCaption { get; set; }

        [DataMember]

        public int? ID { get; set; }

        [DataMember]

        public int EntityId
        {
            get
            {
                return CommFee_Id;
            }
        }
    }
}
