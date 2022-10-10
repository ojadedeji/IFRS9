using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Contracts;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsPdTermStructureRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsPdTermStructureRepository : DataRepositoryBase<IfrsPdTermStructure>, IIfrsPdTermStructureRepository
    {
        protected override IfrsPdTermStructure AddEntity(IFRSContext entityContext, IfrsPdTermStructure entity)
        {
            return entityContext.Set<IfrsPdTermStructure>().Add(entity);
        }

        protected override IfrsPdTermStructure UpdateEntity(IFRSContext entityContext, IfrsPdTermStructure entity)
        {
            return (from e in entityContext.Set<IfrsPdTermStructure>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsPdTermStructure> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsPdTermStructure>()
                   select e;
        }

        protected override IfrsPdTermStructure GetEntity(IFRSContext entityContext, int ID)
        {
            var query = (from e in entityContext.Set<IfrsPdTermStructure>()
                         where e.ID == ID
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
  }
}
