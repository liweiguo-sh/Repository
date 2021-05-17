using Repository.Util;

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Repository {
    public partial class FormTcpIp : Form {
        #region -- module variable definition --
        private const int receiveTimeout = 5 * 1000;
        private const int receiveBufferSize = 512;
        private const int receiveMaxTimes = 3;

        private TcpClient client;
        #endregion
        #region -- FormLoad --
        public FormTcpIp() {
            InitializeComponent();
        }
        private void FormTcpIp_Load(object sender, EventArgs e) {
            txtSocketServerIp.Text = "";
            txtSocketServerPort.Text = "9301";
        }
        private void FormTcpIp_FormClosing(object sender, FormClosingEventArgs e) {
            CloseSocketClient();
        }
        #endregion
        #region -- controls event --
        private void btnSend_Click(object sender, EventArgs e) {
            txtResponse.Text = "";

            SendCommand(CommandText);
        }
        #endregion

        #region -- tcp/ip --
        private string SocketServerIp {
            get {
                txtSocketServerIp.Text = txtSocketServerIp.Text.Trim();
                if (txtSocketServerIp.Text.Equals("")) {
                    txtSocketServerIp.Text = "127.0.0.1";
                }
                return txtSocketServerIp.Text;
            }
        }
        private int SocketServerPort {
            get {
                try {
                    txtSocketServerPort.Text = txtSocketServerPort.Text.Trim();
                    if (txtSocketServerPort.Text.Equals("")) {
                        txtSocketServerPort.Text = "9001";
                    }
                    return Convert.ToInt32(txtSocketServerPort.Text);
                } catch (Exception e) {
                    UtilMessage.ShowError(e);
                    return -1;
                }
            }
        }
        private string CommandText {
            get {
                txtCommand.Text = txtCommand.Text.Trim();
                if (txtCommand.Text.Equals("")) {
                    txtCommand.Text = "empty string";
                }
                return txtCommand.Text;
            }
        }

        private bool OpenSocketClient() {
            try {
                if (client != null) {
                    if (!client.Connected) {
                        client.Close();
                        client = null;
                    }
                }

                if (client == null) {
                    client = new TcpClient();
                    client.Connect(SocketServerIp, SocketServerPort);
                    client.ReceiveTimeout = receiveTimeout;
                    client.ReceiveBufferSize = 255;
                }
            } catch (Exception e) {
                UtilMessage.ShowError(e);
                return false;
            }
            return true;
        }
        private bool CloseSocketClient() {
            try {
                if (client != null) {
                    if (client.Connected) {
                        client.Close();
                    }
                    client = null;
                }

                return true;
            } catch (Exception e) {
                UtilMessage.ShowError(e);
                return false;
            }
        }

        private void SendCommand(string commandString) {
            byte[] bytes;
            // --------------------------------------------
            try {
                OpenSocketClient();

                bytes = Encoding.Default.GetBytes(commandString);
                client.GetStream().Write(bytes, 0, bytes.Length);

                new Thread(DataListiner).Start();
            } catch (Exception e) {
                UtilMessage.ShowError(e);
            }
        }
        private void DataListiner() {
            string response = ReceiveData();

            Console.WriteLine(response);

            Action action = () => {
                txtResponse.Text = response;
            };
            Invoke(action);
        }
        private string ReceiveData() {
            int times = 1, idx;

            string dataString = "", data;
            string tag;
            // -- 1. 接收数据 -------------------------------
            try {
                while (true) {
                    byte[] bytes = new byte[receiveBufferSize];
                    int length = client.GetStream().Read(bytes, 0, bytes.Length);

                    if (length > 0) {
                        data = Encoding.Default.GetString(bytes);
                        idx = data.IndexOf('\0');
                        if (idx >= 0) {
                            data = data.Substring(0, idx);
                        }
                        dataString += data.TrimEnd();
                    }
                    // -- 判断是否数据接收结束(每个项目具体情况不同，如果说有通用方法，就是超时判断) --
                    if (dataString.StartsWith("<")) {
                        idx = dataString.IndexOf(" ");
                        if (idx >= 2) {
                            tag = dataString.Substring(0, idx);
                            tag = "</" + tag.Substring(1) + ">";

                            idx = dataString.IndexOf(tag) + tag.Length;
                            dataString = dataString.Substring(0, idx);
                            break;
                        }
                    }

                    // -- 控制最大接收次数 --
                    if (times++ >= receiveMaxTimes) break;
                    Thread.Sleep(10);
                }
            } catch (Exception e) {
                // -- 一般是超时错误 --
                dataString = e.Message;
            }

            // -- 9. 返回数据 --
            return dataString;
        }
        #endregion
    }
}