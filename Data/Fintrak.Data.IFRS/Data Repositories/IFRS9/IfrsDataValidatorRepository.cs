using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Fintrak.Shared.Common.Extensions;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Data.IFRS.Contracts;
using Fintrak.Shared.Common.Services;

namespace Fintrak.Data.IFRS
{
    [Export(typeof(IIfrsDataValidatorRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class IfrsDataValidatorRepository : DataRepositoryBase<IfrsDataValidator>, IIfrsDataValidatorRepository
    {
        protected override IfrsDataValidator AddEntity(IFRSContext entityContext, IfrsDataValidator entity)
        {
            return entityContext.Set<IfrsDataValidator>().Add(entity);
        }

        protected override IfrsDataValidator UpdateEntity(IFRSContext entityContext, IfrsDataValidator entity)
        {
            return (from e in entityContext.Set<IfrsDataValidator>()
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<IfrsDataValidator> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<IfrsDataValidator>()
                   select e;
        }

        protected override IfrsDataValidator GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<IfrsDataValidator>()
                         where e.ID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<IfrsDataValidator> GetRecordByRefNo(string searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<IfrsDataValidator>()
                             where e.RefNo == searchParam 
                           
                             select e);

                return query.ToArray();
            }
        }









        public IEnumerable<IfrsDataValidator> GetIfrsDataValidators(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<IfrsDataValidator>()
                                 select new
                                 {
                                     ID = e.ID,
                                   
                                     RefNo = e.RefNo,
                                     TableName = e.TableName,
                                     Description= e.Description,
                                     Rundate = e.Rundate


                                 });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);

                    return new List<IfrsDataValidator>().Take(defaultCount).ToArray();

                    //var query = (from e in entityContext.Set<IfrsDataValidator>() select e);
                    //var ExportHandler = new ExcelService();
                    //var response = ExportHandler.Export(query.ToList(), path);

                    //return query.Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<IfrsDataValidator>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
