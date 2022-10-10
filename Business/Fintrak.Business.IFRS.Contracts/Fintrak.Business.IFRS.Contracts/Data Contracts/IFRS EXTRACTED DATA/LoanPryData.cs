using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Fintrak.Shared.Common.ServiceModel;


namespace Fintrak.Business.IFRS.Contracts
{
    [DataContract]
    public class LoanPryData : DataContractBase
    {

        [DataMember]
        public int PryId { get; set; }

        [DataMember]
        public string RefNo { get; set; }

        [DataMember]
        public string AccountNo { get; set; }

        [DataMember]
        public string ProductCategory { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public DateTime ValueDate { get; set; }

        [DataMember]
        public string Sector { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        public Nullable<DateTime> FirstRepaymentdate { get; set; }

        [DataMember]
        public Nullable<DateTime> InterestFirstRepayDate { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public Nullable<double> PeriodicRepaymentAmount { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public decimal Rate { get; set; }

        [DataMember]
        public int? Tenor { get; set; }

        [DataMember]
        public int InterestRepayFreq { get; set; }

        [DataMember]
        public int PrincipalRepayFreq { get; set; }

        [DataMember]
        public string Schedule_Type { get; set; }

        [DataMember]
        public string ScheduleName { get; set; }


        [DataMember]
        public string CompanyCode { get; set; }

        [DataMember]
        public bool Active { get; set; }



        //ACCESS START

        [DataMember]
        public string custid { get; set; }

        [DataMember]
        public Nullable<double> OutstandingBal { get; set; }

        [DataMember]
        public int? PastDue { get; set; }

        [DataMember]
        public string InitialCreditRating { get; set; }

        [DataMember]
        public string current_staging { get; set; }

        [DataMember]
        public string rating { get; set; }

        [DataMember]
        public string Classification { get; set; }

        [DataMember]
        public string CollateralType { get; set; }

        [DataMember]
        public Nullable<double> CollateralValue { get; set; }

        [DataMember]
        public string ForbearanceFlag { get; set; }

        [DataMember]
        public Nullable<double> ForcedSaleValue { get; set; }

        [DataMember]
        public Nullable<double> CashOMV { get; set; }

        [DataMember]
        public Nullable<double> CashFSV { get; set; }

        [DataMember]
        public Nullable<double> CostRecovery { get; set; }

        [DataMember]
        public int? MissedPaymentStage { get; set; }

        [DataMember]
        public int? ClassificationStage { get; set; }

        [DataMember]
        public int? ForbearanceStage { get; set; }

        [DataMember]
        public int? CreditStaging { get; set; }

        [DataMember]
        public int? ModelClassification { get; set; }

        [DataMember]
        public int? ClassificationOverride { get; set; }

        [DataMember]
        public int? ClassificationStageFinal { get; set; }

        //ACCESS END
    }
}
