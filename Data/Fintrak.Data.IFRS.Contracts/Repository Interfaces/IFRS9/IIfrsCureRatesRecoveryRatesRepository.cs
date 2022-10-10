using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IIfrsCureRatesRecoveryRatesRepository : IDataRepository<IfrsCureRatesRecoveryRates>
    {
        //IEnumerable<IfrsCureRatesRecoveryRates> GetIfrsCureRatesRecoveryRatesBySource(string Source);
        IEnumerable<IfrsCureRatesRecoveryRates> GetRecordByRefNo(string RefNo);
        IEnumerable<IfrsCureRatesRecoveryRates> GetIfrsCureRatesRecoveryRates(int defaultCount, string path);
    }
}
