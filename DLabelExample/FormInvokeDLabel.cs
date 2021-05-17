using Repository.Util;

using System;
using System.Collections;
using System.Text.Json;
using System.Windows.Forms;

namespace Repository.DLabelExample {
    public partial class FormInvokeDLabel : Form {
        #region -- FormLoad and Controls event --
        public FormInvokeDLabel() {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            Sample1();
        }
        #endregion

        #region -- print --
        private void Sample1() {
            string url = "http://127.0.0.1:4195/WebPrint/PrintDLabel";
            string urlImage = "http://www.vinda.com/upload/2019/1203/1606fh6dzt.png";
            string labelFile = UtilFile.Combine(Application.StartupPath, @"\res\vinda.dlb");
            string labelString = UtilFile.ReadFile(labelFile);
            string poststring, response;

            HttpClientHelper client = new HttpClientHelper();
            ArrayList list = new ArrayList();
            Hashtable post, data;

            // -- 1. 准备标签数据 --
            for (int i = 1; i <= 5; i++) {
                data = new Hashtable();
                data.Add("element_1", "Deluxe棉韧-" + i);
                data.Add("element_2", new Hashtable {
                    { "text","(01)06901236344033(11)210506(17)240505(10)2119W"},
                    { "value", "01069012363440331121050617240505102119W" }
                });
                data.Add("element_5", urlImage);

                list.Add(data);
            }

            // -- 2. 发送打印数据 --
            post = new Hashtable {
                { "labelString", labelString },
                { "dataString", list },
                { "printerName", "" },
                { "copies", 1 }
            };
            poststring = JsonSerializer.Serialize(post);

            response = client.Post(url, poststring);
            txtResponse.Text = response;
        }
        #endregion
    }
}