using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using Fintrak.Shared.Common.Core;

using systemCore = Fintrak.Business.SystemCore.Bootstrapper;
using core = Fintrak.Business.Core.Bootstrapper;
using ifrs = Fintrak.Business.IFRS.Bootstrapper;
//using mpr = Fintrak.Business.MPR.Bootstrapper;
//using cdqm = Fintrak.Business.CDQM.Bootstrapper;
//using scd = Fintrak.Business.Scorecard.Bootstrapper;
//using budget = Fintrak.Business.Budget.Bootstrapper;

using systemManager = Fintrak.Business.SystemCore.Managers;
using Fintrak.Business.Core.Managers;
using Fintrak.Business.IFRS.Managers;
//using Fintrak.Business.MPR.Managers;
//using Fintrak.Business.CDQM.Managers;
//using Fintrak.Business.Scorecard.Managers;
//using budgetManager = Fintrak.Business.Budget.Managers;

namespace Fintrak.Host.ServicePortal
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new CompositionContainer();
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(systemCore.MEFLoader.Init().Catalog);
            catalog.Catalogs.Add(core.MEFLoader.Init().Catalog);
            catalog.Catalogs.Add(ifrs.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(mpr.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(cdqm.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(scd.MEFLoader.Init().Catalog);
            //catalog.Catalogs.Add(budget.MEFLoader.Init().Catalog);
          
            ObjectBase.Container = new CompositionContainer(catalog);

            //Register Modules
            //System Core
            new systemManager.CoreManager().RegisterModule();

            //Business Core
            new CoreManager().RegisterModule();
            new ExtractionProcessManager().RegisterModule();

            //IFRS
            new IFRSCoreManager().RegisterModule();
            new IFRSLoanManager().RegisterModule();
            new FinancialInstrumentManager().RegisterModule();
            new ExtractedDataManager().RegisterModule();
            new IFRSDataViewManager().RegisterModule();
            new FinstatManager().RegisterModule();
            new IFRS9Manager().RegisterModule();
            
            //MPR
            //new MPRCoreManager().RegisterModule();
            //new MPRBSManager().RegisterModule();
            //new MPRPLManager().RegisterModule();
            //new MPROPEXManager().RegisterModule();

            //CDQM
            //new CDQMManager().RegisterModule();

            //Scorecard
            //new ScorecardManager().RegisterModule();

            //Budget
            //new budgetManager.CoreManager().RegisterModule();
            //new budgetManager.TeamManager().RegisterModule();
            //new budgetManager.StaffExpenseManager().RegisterModule();
            //new budgetManager.CapexManager().RegisterModule();
            //new budgetManager.OpexManager().RegisterModule();
            //new budgetManager.FeeManager().RegisterModule();
            //new budgetManager.RevenueManager().RegisterModule();
        }
    }
}