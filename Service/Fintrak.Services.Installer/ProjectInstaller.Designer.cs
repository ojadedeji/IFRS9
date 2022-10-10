namespace Fintrak.Services.Installer
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.installerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.installerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // installerProcessInstaller
            // 
            this.installerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.installerProcessInstaller.Password = null;
            this.installerProcessInstaller.Username = null;
            // 
            // installerInstaller
            // 
            this.installerInstaller.DisplayName = "Fintrak Extraction Installer Service";
            this.installerInstaller.ServiceName = "FintrakExtractionInstallerService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.installerProcessInstaller,
            this.installerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller installerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller installerInstaller;
    }
}