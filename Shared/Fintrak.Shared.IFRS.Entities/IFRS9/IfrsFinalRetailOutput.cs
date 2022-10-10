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
    public partial class IfrsFinalRetailOutput : EntityBase, IIdentifiableEntity
    {
        [DataMember]
        [Browsable(false)]
        public int FinalRetailId { get; set; }

        [DataMember]
        public string Account_No { get; set; }

        [DataMember]
        public string CustomerName { get; set; }        

        [DataMember]
        public string InternalRating { get; set; }        

        [DataMember]
        public int STAGE { get; set; }        

        [DataMember]
        public int YTM { get; set; }        

        [DataMember]
        public string FacilityType { get; set; }        

        [DataMember]
        public double OutstandingBalance { get; set; }        

        [DataMember]
        public int Seq { get; set; }        

        [DataMember]
        public double? EAD { get; set; }        

        [DataMember]
        public double LifetimePDBest { get; set; }        

        [DataMember]
        public double LifetimePDOptimistic { get; set; }  

        [DataMember]
        public double LifetimePDDownturn { get; set; }  

        [DataMember]
        public double DiscountFactor { get; set; }  

        [DataMember]
        public double LGD_Best { get; set; }  

        [DataMember]
        public double LGD_Optimistic { get; set; }  

        [DataMember]
        public double LGD_Downturn { get; set; }  

        [DataMember]
        public double ECL_MonthlyBest { get; set; }  

        [DataMember]
        public double ECL_MonthlyOptimistic { get; set; }  

        [DataMember]
        public double ECL_MonthlyDownturn { get; set; } 

        [DataMember]
        public double ECL_Best { get; set; }    

        [DataMember]
        public double ECL_Optimistic { get; set; }  

        [DataMember]
        public double ECL_Downturn { get; set; }  

        [DataMember]
        public double Macroeconomic_Best { get; set; }  

        [DataMember]
        public double Macroeconomic_Optimistic { get; set; }  

        [DataMember]
        public double Macroeconomic_Downturn { get; set; } 

        [DataMember]
        public double Impairment { get; set; } 

        [DataMember]
        public DateTime RunDate { get; set; } 



        [DataMember]
        public bool Active { get; set; }

        public int EntityId
        {
            get
            {
                return FinalRetailId;
            }
        }
    }
}