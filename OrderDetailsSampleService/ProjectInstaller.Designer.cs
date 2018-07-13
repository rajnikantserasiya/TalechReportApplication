namespace OrderDetailsSampleService
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
            this.OrderDetailsSampleProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.OrderDetailsSampleServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // OrderDetailsSampleProcessInstaller
            // 
            this.OrderDetailsSampleProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.OrderDetailsSampleProcessInstaller.Password = null;
            this.OrderDetailsSampleProcessInstaller.Username = null;
            // 
            // OrderDetailsSampleServiceInstaller
            // 
            this.OrderDetailsSampleServiceInstaller.ServiceName = "OrderDetailsSampleService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.OrderDetailsSampleProcessInstaller,
            this.OrderDetailsSampleServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller OrderDetailsSampleProcessInstaller;
        private System.ServiceProcess.ServiceInstaller OrderDetailsSampleServiceInstaller;
    }
}