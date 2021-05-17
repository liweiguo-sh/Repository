using Repository.DLabelExample;

using System;
using System.Windows.Forms;

namespace Repository {
    public partial class FormMain : Form {
        #region -- module variable definition --
        private string autoLoadForm = "invokeDLabel";
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
            else {

            }
        }
        #endregion

        #region -- Controls event --
        private void btnInvokeDLabel_Click(object sender, EventArgs e) {
            (new FormInvokeDLabel()).ShowDialog();
        }
        #endregion
    }
}