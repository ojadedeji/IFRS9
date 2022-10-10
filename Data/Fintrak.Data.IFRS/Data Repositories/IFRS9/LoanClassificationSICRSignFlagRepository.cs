using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(ILoanClassificationSICRFlagRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoanClassificationSICRSignFlagRepository : DataRepositoryBase<LoanClassificationSICRSignFlag>, ILoanClassificationSICRFlagRepository
    {
        protected override LoanClassificationSICRSignFlag AddEntity(IFRSContext entityContext, LoanClassificationSICRSignFlag entity)
        {
            return entityContext.Set<LoanClassificationSICRSignFlag>().Add(entity);
        }

        protected override LoanClassificationSICRSignFlag UpdateEntity(IFRSContext entityContext, LoanClassificationSICRSignFlag entity)
        {
            return (from e in entityContext.Set<LoanClassificationSICRSignFlag>()
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<LoanClassificationSICRSignFlag> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<LoanClassificationSICRSignFlag>()
                   select e;
        }

        protected override LoanClassificationSICRSignFlag GetEntity(IFRSContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<LoanClassificationSICRSignFlag>()
                         where e.Id == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }


        public IEnumerable<loanClassSignFlagInfo> GetAllLoanClassificationSICRSignFlagData()
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = from a in entityContext.LoanClassificationSICRSignFlagSet
                            join b in entityContext.SICRParametersSet on a.SICR_ParamID equals b.ID
                            select new loanClassSignFlagInfo()
                            {
                                loanClassSignFlag = a,
                                SICRParameter = b
                            };

                return query.ToFullyLoaded();
            }
        }
       
    }
}