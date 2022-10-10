using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.CDQM.Entities;
using Fintrak.Data.CDQM.Contracts;

namespace Fintrak.Data.CDQM
{
    [Export(typeof(ICDQMCountryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CDQMCountryRepository : DataRepositoryBase<CDQMCountry>, ICDQMCountryRepository
    {

        protected override CDQMCountry AddEntity(CDQMContext entityContext, CDQMCountry entity)
        {
            return entityContext.Set<CDQMCountry>().Add(entity);
        }

        protected override CDQMCountry UpdateEntity(CDQMContext entityContext, CDQMCountry entity)
        {
            return (from e in entityContext.Set<CDQMCountry>() 
                    where e.CountryId == entity.CountryId
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CDQMCountry> GetEntities(CDQMContext entityContext)
        {
            return from e in entityContext.Set<CDQMCountry>()
                   select e;
        }

        protected override CDQMCountry GetEntity(CDQMContext entityContext, int id)
        {
            var query = (from e in entityContext.Set<CDQMCountry>()
                         where e.CountryId == id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }
      
    }
}
