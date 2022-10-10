using Fintrak.Shared.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Fintrak.Shared.IFRS.Entities;
using Fintrak.Shared.IFRS.Framework;


namespace Fintrak.Data.IFRS.Contracts
{
    public interface IMacroNPLRepository : IDataRepository<MacroNPL>
    {
        IEnumerable<MacroNPL> GetRecordByRefNo(DateTime Period);
        IEnumerable<MacroNPL> GetMacroNPLs(int defaultCount, string path);
        ///;
        //UpdateLog GetLogById(int ID);
        //IEnumerable<UpdateLog> GetAllLogs();
        //IEnumerable<UpdateLog> CreateLog(UpdateLog log);

        //IEnumerable<UpdateLog> CreateUpdateLog(Object OldObject, Object NewObject);
      //  MacroNPL[] Add(List<MacroNPL> rslt);
        //IEnumerable<MacroNPL> Update(List<MacroNPL> rslt);
        //IEnumerable<UpdateLog> UpdateLog(int ID, UpdateLog log);
        //bool DeleteLog(int ID);
    }
}
