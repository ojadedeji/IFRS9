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
    [Export(typeof(IMacroNPLRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MacroNPLRepository : DataRepositoryBase<MacroNPL>, IMacroNPLRepository
    {
        protected override MacroNPL AddEntity(IFRSContext entityContext, MacroNPL entity)
        {
            return entityContext.Set<MacroNPL>().Add(entity);
        }

        protected override MacroNPL UpdateEntity(IFRSContext entityContext, MacroNPL entity)
        {

            entity.Approved = true;

           
            return (from e in entityContext.Set<MacroNPL>()
              
            where e.MacroID == entity.MacroID
                    select e).FirstOrDefault();         
        }
        

        protected override IEnumerable<MacroNPL> GetEntities(IFRSContext entityContext)
        {
            return from e in entityContext.Set<MacroNPL>()
                   select e;
        }

        protected override MacroNPL GetEntity(IFRSContext entityContext, int Id)
        {
            var query = (from e in entityContext.Set<MacroNPL>()
                         where e.MacroID == Id
                         select e);

            var results = query.FirstOrDefault();

            return results;
        }

        public IEnumerable<MacroNPL> GetRecordByRefNo(DateTime searchParam)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                var query = (from e in entityContext.Set<MacroNPL>()

                             where e.Period == searchParam
                             //orderby e.date_pmt
                             select e);

                return query.ToArray();
            }
        }
        public IEnumerable<MacroNPL> GetMacroNPLs(int defaultCount, string path)
        {
            using (IFRSContext entityContext = new IFRSContext())
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var query = (from e in entityContext.Set<MacroNPL>()
                                 select new {
                               
                                     Period = e.Period,
                                     Inflation = e.Inflation,
                                     CrudeOilPrice = e.CrudeOilPrice,
                                     UnemploymentRatio = e.UnemploymentRatio,
                                     Scnenario = e.Scenario,
                                     CreatedBy = e.CreatedBy,
                                     UpdatedBy = e.UpdatedBy                               
                                          });
                    var ExportHandler = new ExcelService();
                    var response = ExportHandler.Export(query.ToList(), path);
                    return new List<MacroNPL>().Take(defaultCount).ToArray();
                }
                else
                {
                    var query = (from e in entityContext.Set<MacroNPL>().Take(defaultCount) select e);

                    return query.ToArray();
                }
            }
        }
    }
}
