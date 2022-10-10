//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using System.Linq;
//using Fintrak.Shared.Common.Extensions;
//using Fintrak.Shared.IFRS.Entities;
//using Fintrak.Data.IFRS.Contracts;
//using Fintrak.Shared.Common.Contracts;

//namespace Fintrak.Data.IFRS
//{
//    [Export(typeof(IIfrsLoanClassificationRepository))]
//    [PartCreationPolicy(CreationPolicy.NonShared)]
//    public class IfrsLoanClassificationRepository : DataRepositoryBase<IfrsLoanClassification>, IIfrsLoanClassificationRepository
//    {
//        protected override IfrsLoanClassification AddEntity(IFRSContext entityContext, IfrsLoanClassification entity)
//        {
//            return entityContext.Set<IfrsLoanClassification>().Add(entity);
//        }

//        protected override IfrsLoanClassification UpdateEntity(IFRSContext entityContext, IfrsLoanClassification entity)
//        {
//            return (from e in entityContext.Set<IfrsLoanClassification>()
//                    where e.ID == entity.ID
//                    select e).FirstOrDefault();
//        }

//        protected override IEnumerable<IfrsLoanClassification> GetEntities(IFRSContext entityContext)
//        {
//            return from e in entityContext.Set<IfrsLoanClassification>()
//                   select e;
//        }

//        protected override IfrsLoanClassification GetEntity(IFRSContext entityContext, int ID)
//        {
//            var query = (from e in entityContext.Set<IfrsLoanClassification>()
//                         where e.ID == ID
//                         select e);

//            var results = query.FirstOrDefault();

//            return results;
//        }
//  }
//}
