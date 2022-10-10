using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace Fintrak.Services.ProcessService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        private readonly Configuration _config = null;

        public ProjectInstaller()
        {
            InitializeComponent();

            Assembly service = Assembly.GetAssembly(typeof(ProcessService));
            _config = ConfigurationManager.OpenExeConfiguration(service.Location);

            serviceProcessInstaller1.Account = Account;
            serviceProcessInstaller1.Username = UserName;
            serviceProcessInstaller1.Password = Password;
        }

        private ServiceAccount Account
        {
            get
            {
                var account = (_config.AppSettings.Settings["ServiceAccount"].Value == null ? null : _config.AppSettings.Settings["ServiceAccount"].Value);

                if (account == "User")
                    return ServiceAccount.User;
                else if (account == "Local Service")
                    return ServiceAccount.LocalService;
                else if (account == "Local System")
                    return ServiceAccount.LocalSystem;
                else if (account == "Network Service")
                    return ServiceAccount.NetworkService;

                throw new Exception("Uable to read account configuration setting.");

            }
        }

        private string UserName
        {
            get
            {
                var userName = (_config.AppSettings.Settings["ServiceUserName"].Value == null ? null : _config.AppSettings.Settings["ServiceUserName"].Value);

                if (userName == null)
                    throw new Exception("Uable to read service user configuration setting.");
                else
                    return userName;
            }
        }

        private string Password
        {
            get
            {
                var password = (_config.AppSettings.Settings["ServicePassword"].Value == null ? null : _config.AppSettings.Settings["ServicePassword"].Value);

                if (password == null)
                    throw new Exception("Uable to read service password configuration setting.");
                else
                    return password;

            }
        }
    }
}
