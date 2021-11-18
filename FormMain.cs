using Repository.Comm;
using Repository.Dialog;
using Repository.DLabelExample;
using Repository.TcpIp;

using System;
using System.Windows.Forms;

namespace Repository {
    public partial class FormMain : Form {
        #region -- module variable definition --
        private readonly string autoLoadForm = "FormComm";
        #endregion
        #region -- FormLoad --
        public FormMain() {
            InitializeComponent();
        }
        private void FormMain_Load(object sender, EventArgs e) {
            if (autoLoadForm.Equals("invokeDLabel")) {
                btnInvokeDLabel_Click(null, null);
                Close();
            }
            else if (autoLoadForm.Equals("tcpServerSimulator")) {
                btnTcpServerSimulator_Click(null, null);
                Close();
            }
            else if (autoLoadForm.Equals("FormDialog")) {
                btnFormDialog_Click(null, null);
                Close();
            }
            else if (autoLoadForm.Equals("FormComm")) {
                btnComm_Click(null, null);
                Close();
            }
            else {

            }
        }
        #endregion

        #region -- Controls event --
        private void btnInvokeDLabel_Click(object sender, EventArgs e) {
            (new FormInvokeDLabel()).ShowDialog();
        }
        private void btnTcpServerSimulator_Click(object sender, EventArgs e) {
            (new FormTcpServerSimulator()).ShowDialog();
        }

        private void btnFormDialog_Click(object sender, EventArgs e) {
            new FormDialogTest().ShowDialog();
        }

        private void btnComm_Click(object sender, EventArgs e) {
            new FormComm().ShowDialog();
        }
        #endregion
    }
}