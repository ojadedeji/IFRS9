namespace Fintrak.Services
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
            this.extractionProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.extractionInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // extractionProcessInstaller
            // 
            this.extractionProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.extractionProcessInstaller.Password = null;
            this.extractionProcessInstaller.Username = null;
            // 
            // extractionInstaller
            // 
            this.extractionInstaller.ServiceName = "Extraction Service";
            this.extractionInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.extractionProcessInstaller,
            this.extractionInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller extractionProcessInstaller;
        private System.ServiceProcess.ServiceInstaller extractionInstaller;
    }
}