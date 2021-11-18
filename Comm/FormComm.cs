using DoYs.Framework.Util;

using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Repository.Comm {
    public partial class FormComm : Form {
        #region -- module variable definition --
        private const double TIMEOUT_MAX = 5 * 1000;
        private const double TIMEOUT_QUE = 1 * 1000;
        private const string ERR_PREFIX = "err_";
        private const string ERR_TIMEOUT_MAX = ERR_PREFIX + "timeout_max";        // -- 指令超时，可能是长时间没有返回结果 --
        private const string ERR_TIMEOUT_QUE = ERR_PREFIX + "timeout_que";        // -- 队列超时，可能是数据不完整或不正确 --

        private SerialPort port = null;
        private string commReturn;
        #endregion
        #region -- FormLoad --
        public FormComm() {
            InitializeComponent();
        }

        private void FormComm_Load(object sender, EventArgs e) {
            string portName;
            string[] portNames = SerialPort.GetPortNames();

            if (portNames.Length > 0) {
                for (int i = 0; i < portNames.Length; i++) {
                    portName = portNames[i];
                    cbbPort.Items.Add(portName);

                    if (UtilString.Equals(portName, "COM5")) {
                        cbbPort.Text = portName;
                    }
                }
                if (cbbPort.Text.Equals("")) {
                    cbbPort.Text = portNames[0];
                }
            }
        }
        #endregion
        #region -- controls event --
        private void btnTest_Click(object sender, EventArgs e) {
            Test1();
        }
        #endregion

        #region -- testing --
        private void Test1() {
            string commResult;
            try {
                commResult = SendCommand("01 03 06 06 00 02 24 82");
                txtReturn.Text = commResult;



            } catch (Exception ex) {
                UtilMessage.ShowError(ex.Message);
            }
        }
        #endregion
        #region -- 串口通讯 --
        public void InitCOM(string PortName) {
            if (port != null) {
                throw new Exception("串口已经初始化。");
            }

            port = new SerialPort(PortName) {
                BaudRate = 19200,               // -- 波特率 --
                Parity = Parity.None,           // -- 无奇偶校验位 --
                DataBits = 8,
                StopBits = StopBits.Two,        // -- 1个停止位 --
                ReceivedBytesThreshold = 4      // -- 设置 DataReceived 事件发生前内部输入缓冲区中的字节数 --
            };

            // -- DataReceived事件委托 --
            port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
            port.Open();

            if (!port.IsOpen) {
                throw new Exception("串口打开失败，请检查。");
            }
        }
        private string SendCommand(string commandString) {
            int len;
            double interval;

            byte[] commandBytes;
            // -- 1. 打开端口 -------------------------------
            if (port == null) {
                InitCOM(cbbPort.Text);
            }
            if (port == null) {
                UtilMessage.ShowError("串口端口打开失败，请检查。");
                return "";
            }
            if (!port.IsOpen) {
                port.Open();
            }

            // -- 2. 发送指令 -------------------------------
            commandString = commandString.Replace(" ", "");
            len = commandString.Length / 2;
            commandBytes = new byte[len];
            for (int i = 0; i < len; i++) {
                commandBytes[i] = Convert.ToByte(commandString.Substring(2 * i, 2), 16);
            }

            commReturn = "";
            port.Write(commandBytes, 0, commandBytes.Length);

            // -- 3. 模拟同步，等待串口返回结果 ------------------------
            DateTime dt1 = DateTime.Now;
            while (true) {
                interval = (DateTime.Now - dt1).TotalMilliseconds;
                if (interval > TIMEOUT_MAX) {
                    return ERR_TIMEOUT_MAX;
                }

                Thread.Sleep(100);
                if (commReturn != "") {
                    break;
                }
            }

            // -- 9. 返回结果 -------------------------------
            commReturn = StrToHexFormat(commReturn);
            return commReturn;
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            double interval;

            char ch;
            string strResult, str;
            StringBuilder builder = new StringBuilder();
            // --------------------------------------------
            try {
                DateTime dt1 = DateTime.Now;
                while (true) {
                    strResult = builder.ToString();
                    if (strResult.Length >= 18) {
                        // -- 数据完整 --
                        commReturn = strResult;
                        return;
                    }

                    interval = (DateTime.Now - dt1).TotalMilliseconds;
                    if (interval > TIMEOUT_QUE) {
                        // -- 超时 --
                        commReturn = ERR_TIMEOUT_QUE;
                        port.Close();
                        return;
                    }

                    while (port.BytesToRead > 0) {
                        ch = (char)port.ReadByte();
                        str = Convert.ToString(ch, 16).ToUpper();
                        if (str.Length == 1) {
                            str = "0" + str;
                        }
                        builder.Append(str);

                        dt1 = DateTime.Now;
                    }
                }
            } catch (Exception ex) {
                port.Close();
                Console.WriteLine(ex.ToString());
            } finally {
            }
        }

        private static string StrToHexFormat(string hexString) {
            int len;
            StringBuilder builder = new StringBuilder();

            hexString = hexString.Replace(" ", "");
            len = hexString.Length / 2;
            for (int i = 0; i < len; i++) {
                builder.Append(hexString.Substring(2 * i, 2))
                    .Append(" ");
            }
            return builder.ToString();
        }
        #endregion
    }
}