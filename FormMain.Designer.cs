
namespace Repository {
    partial class FormMain {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnInvokeDLabel = new System.Windows.Forms.Button();
            this.btnTcpServerSimulator = new System.Windows.Forms.Button();
            this.btnFormDialog = new System.Windows.Forms.Button();
            this.btnComm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInvokeDLabel
            // 
            this.btnInvokeDLabel.Location = new System.Drawing.Point(12, 12);
            this.btnInvokeDLabel.Name = "btnInvokeDLabel";
            this.btnInvokeDLabel.Size = new System.Drawing.Size(200, 48);
            this.btnInvokeDLabel.TabIndex = 0;
            this.btnInvokeDLabel.Text = "打印控件调用样例";
            this.btnInvokeDLabel.UseVisualStyleBackColor = true;
            this.btnInvokeDLabel.Click += new System.EventHandler(this.btnInvokeDLabel_Click);
            // 
            // btnTcpServerSimulator
            // 
            this.btnTcpServerSimulator.Location = new System.Drawing.Point(279, 12);
            this.btnTcpServerSimulator.Name = "btnTcpServerSimulator";
            this.btnTcpServerSimulator.Size = new System.Drawing.Size(200, 48);
            this.btnTcpServerSimulator.TabIndex = 1;
            this.btnTcpServerSimulator.Text = "Tcp服务端模拟器";
            this.btnTcpServerSimulator.UseVisualStyleBackColor = true;
            this.btnTcpServerSimulator.Click += new System.EventHandler(this.btnTcpServerSimulator_Click);
            // 
            // btnFormDialog
            // 
            this.btnFormDialog.Location = new System.Drawing.Point(616, 12);
            this.btnFormDialog.Name = "btnFormDialog";
            this.btnFormDialog.Size = new System.Drawing.Size(141, 48);
            this.btnFormDialog.TabIndex = 2;
            this.btnFormDialog.Text = "对话框";
            this.btnFormDialog.UseVisualStyleBackColor = true;
            this.btnFormDialog.Click += new System.EventHandler(this.btnFormDialog_Click);
            // 
            // btnComm
            // 
            this.btnComm.Location = new System.Drawing.Point(279, 100);
            this.btnComm.Name = "btnComm";
            this.btnComm.Size = new System.Drawing.Size(200, 48);
            this.btnComm.TabIndex = 3;
            this.btnComm.Text = "串口通讯测试";
            this.btnComm.UseVisualStyleBackColor = true;
            this.btnComm.Click += new System.EventHandler(this.btnComm_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 411);
            this.Controls.Add(this.btnComm);
            this.Controls.Add(this.btnFormDialog);
            this.Controls.Add(this.btnTcpServerSimulator);
            this.Controls.Add(this.btnInvokeDLabel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInvokeDLabel;
        private System.Windows.Forms.Button btnTcpServerSimulator;
        private System.Windows.Forms.Button btnFormDialog;
        private System.Windows.Forms.Button btnComm;
    }
}