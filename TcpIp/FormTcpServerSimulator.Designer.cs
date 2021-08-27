
namespace Repository.TcpIp {
    partial class FormTcpServerSimulator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTcpServerSimulator));
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.lblConfig = new System.Windows.Forms.Label();
            this.lblSimulator = new System.Windows.Forms.Label();
            this.txtSimulatorFile = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.timerResize = new System.Windows.Forms.Timer(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Location = new System.Drawing.Point(84, 6);
            this.txtConfigFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.Size = new System.Drawing.Size(206, 26);
            this.txtConfigFile.TabIndex = 0;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Location = new System.Drawing.Point(12, 9);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(65, 20);
            this.lblConfig.TabIndex = 1;
            this.lblConfig.Text = "配置文件";
            // 
            // lblSimulator
            // 
            this.lblSimulator.AutoSize = true;
            this.lblSimulator.Location = new System.Drawing.Point(358, 9);
            this.lblSimulator.Name = "lblSimulator";
            this.lblSimulator.Size = new System.Drawing.Size(65, 20);
            this.lblSimulator.TabIndex = 2;
            this.lblSimulator.Text = "模拟文件";
            // 
            // txtSimulatorFile
            // 
            this.txtSimulatorFile.Location = new System.Drawing.Point(430, 6);
            this.txtSimulatorFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSimulatorFile.Name = "txtSimulatorFile";
            this.txtSimulatorFile.Size = new System.Drawing.Size(206, 26);
            this.txtSimulatorFile.TabIndex = 3;
            this.txtSimulatorFile.DoubleClick += new System.EventHandler(this.txtSimulatorFile_DoubleClick);
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Courier New", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(313, 144);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(411, 148);
            this.txtLog.TabIndex = 4;
            this.txtLog.Text = "";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(12, 59);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(160, 36);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(12, 139);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(160, 36);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清空日志";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FormTcpServerSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 420);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtSimulatorFile);
            this.Controls.Add(this.lblSimulator);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.txtConfigFile);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormTcpServerSimulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tcp服务端模拟器";
            this.Load += new System.EventHandler(this.FormTcpServerSimulator_Load);
            this.Resize += new System.EventHandler(this.FormTcpServerSimulator_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.Label lblSimulator;
        private System.Windows.Forms.TextBox txtSimulatorFile;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Timer timerResize;
        private System.Windows.Forms.Button btnClear;
    }
}