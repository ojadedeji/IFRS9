using Fintrak.Shared.IFRS.Framework;
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
using System.Xml.Serialization;

namespace Fintrak.Shared.IFRS.Entities
{
    public partial class ConsolidatedLoans : EntityBase, IIdentifiableEntity
    {

        [DataMember]
        [Browsable(false)]
        public int ID { get; set; }

        [DataMember] public string AcctNo { get; set; }
        [DataMember] public string Classification { get; set; }
        [DataMember] public string ProductName { get; set; }
        [DataMember] public DateTime ValueDate { get; set; }
        [DataMember] public DateTime MaturityDate { get; set; }
        [DataMember] public string Sector { get; set; }
        [DataMember] public string SubSector { get; set; }
        [DataMember] public string ProductType { get; set; }
        [DataMember] public double Amount { get; set; }
        [DataMember] public double CurrentBalance { get; set; }
        [DataMember] public double AdjustedBalances { get; set; }
        [DataMember] public string LoanType { get; set; }
        [DataMember] public double Rate { get; set; }
        [DataMember] public string HC1 { get; set; }
        [DataMember] public string HC2 { get; set; }
        [DataMember] public string PAY1 { get; set; }
        [DataMember] public double OB1 { get; set; }
        [DataMember] public string RR1 { get; set; }
        [DataMember] public string PAY2 { get; set; }
        [DataMember] public double OB2 { get; set; }
        [DataMember] public string RR2 { get; set; }
        [DataMember] public string PAY3 { get; set; }
        [DataMember] public double OB3 { get; set; }
        [DataMember] public string RR3 { get; set; }
        [DataMember] public string PAY4 { get; set; }
        [DataMember] public double OB4 { get; set; }
        [DataMember] public string RR4 { get; set; }
        [DataMember] public string PAY5 { get; set; }
        [DataMember] public double OB5 { get; set; }
        [DataMember] public string RR5 { get; set; }
        [DataMember] public string PAY6 { get; set; }
        [DataMember] public double OB6 { get; set; }
        [DataMember] public string RR6 { get; set; }
        [DataMember] public DateTime RunDate { get; set; }


        public bool Active { get; set; }

            public int EntityId{
                get{
                    return ID;
                }
            }
        }
    }
