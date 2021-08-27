using System;
using System.Windows.Forms;

namespace Repository.Dialog {
    public partial class FormDialogTest : Form {
        #region -- FormLoad --
        public FormDialogTest() {
            InitializeComponent();
        }
        private void FormDialogTest_Load(object sender, EventArgs e) {

        }
        #endregion
        #region -- Form controls event --
        private void btnFolderBrowserDialog_Click(object sender, EventArgs e) {
            string foldPath = "";
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            dialog.SelectedPath = @"C:\Users\volant\Desktop";
            //dialog.RootFolder = Environment.SpecialFolder.Programs;
            if (dialog.ShowDialog() == DialogResult.OK) {
                foldPath = dialog.SelectedPath;
            }
            Console.WriteLine(foldPath);
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件类型
            saveFileDialog.Filter = "BMP Files(*.*)|*.*";
            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;
            var DialogResult = saveFileDialog.ShowDialog();
            //点了保存按钮进入
            if (DialogResult == DialogResult.OK) {
                //获得文件路径
                string localFilePath = saveFileDialog.FileName.ToString();
            } 
        }
        #endregion 
    }
}