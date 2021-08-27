using DoYs.Framework.Util;

using Repository.Common;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Repository.TcpIp {
    public partial class FormTcpServerSimulator : Form {
        #region -- module variable definition --
        private float logFontWidth;                                     // -- 日志字体宽度 --

        private string pathModule;                                      // -- 模块物理路径 --
        private string pathModuleRelative = "TcpServerSimulator";       // -- 模块相对路径 --
        private string fileModuleIni = "module.ini";                    // -- 模块配置文件 --       

        private string[,] simulationDATA;                               // -- 模拟数据 --

        IniFile iniModule;                                              // -- 模块配置类实例 --

        private Socket tcpServer;                                       // -- socket服务端实例 --
        private Thread threadWatch;                                     // -- 监听客户端新连接线程 --
        private Dictionary<string, Socket> dicClient;                   // -- socket客户端连接集合 --
        private Dictionary<string, Thread> dicThread;                   // -- 监听客户端消息线程集合 --

        private string receiveMode = "HEX";                             // -- 接收模式(HEX/ASCII) --
        private string sendMode = "HEX";                                // -- 接收模式(HEX/ASCII) --
        #endregion
        #region -- FormLoad --
        public FormTcpServerSimulator() {
            InitializeComponent();
        }
        private void FormTcpServerSimulator_Load(object sender, EventArgs e) {
            Control.CheckForIllegalCrossThreadCalls = false;

            Width = Screen.AllScreens[0].WorkingArea.Width / 2;
            Height = Screen.AllScreens[0].WorkingArea.Height / 2;

            InitParameter();
        }
        private void FormTcpServerSimulator_Resize(object sender, EventArgs e) {
            FormResize();
        }

        private void FormResize() {
            lblConfig.Top = 15;
            txtConfigFile.Top = lblConfig.Top - 3;
            txtConfigFile.Left = btnOpen.Left + btnOpen.Width + 12;
            txtConfigFile.Width = Width / 2 - txtConfigFile.Left - 36;
            lblConfig.Left = txtConfigFile.Left - lblConfig.Width - 12;

            lblSimulator.Top = lblConfig.Top;
            lblSimulator.Left = Width / 2 + lblConfig.Left;
            txtSimulatorFile.Top = txtConfigFile.Top;
            txtSimulatorFile.Left = lblSimulator.Left + (txtConfigFile.Left - lblConfig.Left);
            txtSimulatorFile.Width = txtConfigFile.Width;

            txtLog.Top = btnOpen.Top;
            txtLog.Left = btnOpen.Left + btnOpen.Width + 12;
            txtLog.Width = txtSimulatorFile.Left + txtSimulatorFile.Width - txtLog.Left;
            txtLog.Height = Height - txtLog.Top - 60;
        }
        #endregion

        #region -- controls event --
        private void txtSimulatorFile_DoubleClick(object sender, EventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog {
                Multiselect = true,
                Title = "请选择 TcpServer 模拟数据文件...",
                Filter = "所有文件(*.*)|*.*"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) {
                return;
            }
            txtSimulatorFile.Text = fileDialog.FileName;
            InitSimulationData(txtSimulatorFile.Text);

            iniModule.setValue("simulationDataFile", txtSimulatorFile.Text);
            iniModule.Save();
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            try {
                if (tcpServer == null) {
                    OpenTcpServer("127.0.0.1", 10);

                    btnOpen.Text = "关闭";
                }
                else {
                    CloseTcpServer();

                    btnOpen.Text = "打开";
                }
            } catch (Exception ex) {
                WriteLog(ex.ToString(), Color.DarkRed, 0, -1);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            txtLog.Clear();
        }
        #endregion

        #region -- init --
        private void InitParameter() {
            // --------------------------------------------
            Graphics g = this.CreateGraphics();
            Font font = new Font(txtLog.Font.Name, txtLog.Font.Size, txtLog.Font.Style);
            SizeF sizeF = g.MeasureString("A", font);
            logFontWidth = sizeF.Width;

            // --------------------------------------------
            pathModule = Config.GetPath(pathModuleRelative);
            UtilFile.CheckDirectory(pathModule, true);

            string iniFile = UtilFile.Combine(pathModule, fileModuleIni);
            iniModule = new IniFile(iniFile, 0);


            string simulationDataFile = iniModule.getValue("simulationDataFile");
            if (!simulationDataFile.Equals("")) {
                if (File.Exists(simulationDataFile)) {
                    txtSimulatorFile.Text = simulationDataFile;
                    InitSimulationData(simulationDataFile);
                }
            }
        }

        private void InitSimulationData(string simulationDataFile) {
            int idx = 0;
            int reqLine = 0, resLine = 0;

            string text, reqString = "", resString = "";
            ArrayList list = UtilFile.FileToArray(simulationDataFile);

            simulationDATA = new string[list.Count / 2, 2];
            for (int i = 0; i < list.Count; i++) {
                text = ((string)list[i]).Trim();
                if (text.StartsWith("@")) {
                    reqLine = i;
                    reqString = text;
                }
                else if (text.StartsWith("$")) {
                    resLine = i;
                    resString = text;
                }

                if (resLine == i && resLine > reqLine) {
                    simulationDATA[idx, 0] = reqString.Substring(1).Trim();
                    simulationDATA[idx, 1] = resString.Substring(1).Trim();
                    idx++;
                }
            }

            for (int i = 0; i < simulationDATA.Length; i++) {
                reqString = (string)simulationDATA[i, 0];
                resString = (string)simulationDATA[i, 1];
                if (reqString == null || reqString.Equals("")) break;

                WriteLog(reqString, Color.Black, 0, 1);
                WriteLog(resString, Color.Red, 2, 2);
            }
        }
        private string GetResByReq(string reqString) {
            string key;

            for (int i = 0; i < simulationDATA.Length; i++) {
                key = simulationDATA[i, 0];
                if (key == null || key.Equals("")) {
                    return "";
                }


                key = key.Replace(" ", "");
                reqString = reqString.Replace(" ", "");
                if (UtilString.Equals(key, reqString)) {
                    return (string)simulationDATA[i, 1];
                }
            }
            return "";
        }
        #endregion
        #region -- log --
        private void WriteLog(string text, Color color, int indent, int newLine) {
            if (newLine == -1) {
                txtLog.Clear();
            }

            txtLog.SelectionColor = color;
            txtLog.SelectionIndent = (int)(logFontWidth * indent);
            txtLog.AppendText(text);

            for (int i = 0; i < newLine; i++) {
                txtLog.AppendText("\r\n");
            }

            txtLog.Select(txtLog.TextLength, 0);
            txtLog.ScrollToCaret();
        }
        #endregion

        #region -- comm --
        private void OpenTcpServer(string ip, int port) {
            dicClient = new Dictionary<string, Socket>();
            dicThread = new Dictionary<string, Thread>();

            tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse(ip), port));  // -- 绑定IP和申请端口 --
            tcpServer.Listen(10);                                       // -- 设置客户端最大连接数 --
            WriteLog("TcpServer started", Color.Green, 0, -1);


            threadWatch = new Thread(WatchClientConnecting) {
                IsBackground = true
            };
            threadWatch.Start();
            WriteLog("开始监听客户端请求...", Color.Green, 0, 1);
        }
        private void WatchClientConnecting() {
            while (true) {
                Socket socketClient = tcpServer.Accept();
                dicClient.Add(socketClient.RemoteEndPoint.ToString(), socketClient);
                WriteLog("客户端" + socketClient.RemoteEndPoint.ToString() + "连接成功...", Color.Blue, 0, 1);

                // -- 创建一个通信线程 --
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveClientMessage);
                Thread threadComm = new Thread(pts) {
                    IsBackground = true
                };

                threadComm.Start(socketClient);
                dicThread.Add(socketClient.RemoteEndPoint.ToString(), threadComm);
            }
        }
        private void ReceiveClientMessage(object obj) {
            string reqString, clientId;

            Socket socket;
            // --------------------------------------------
            while (true) {
                socket = (Socket)obj;

                byte[] arrServerRecMsg = new byte[1024];
                try {
                    int length = socket.Receive(arrServerRecMsg);
                    if (length == 0) {
                        clientId = socket.RemoteEndPoint.ToString();

                        socket.Close();
                        dicClient.Remove(clientId);
                        dicThread.Remove(clientId);

                        WriteLog("客户端" + clientId + "连接已关闭", Color.Red, 0, 1);
                        return;
                    }

                    if (UtilString.Equals(receiveMode, "HEX")) {
                        reqString = UtilString.BytesToHexString(arrServerRecMsg, true);
                    }
                    else {
                        // -- ascii --
                        reqString = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    }
                    WriteLog("收到客户端" + socket.RemoteEndPoint.ToString() + "数据：" + reqString, Color.Red, 0, 1);

                    string response = GetResByReq(reqString);
                    if (response.Equals("")) {
                        WriteLog("不认识的指令" + reqString, Color.Red, 2, 1);
                    }
                    else {
                        WriteLog("返回客户端数据：" + response, Color.Green, 0, 2);


                        byte[] arrSendMsg;
                        if (UtilString.Equals(sendMode, "HEX")) {
                            arrSendMsg = UtilNumber.HexStringToBytes(response, 2);
                        }
                        else {
                            arrSendMsg = Encoding.UTF8.GetBytes(response.Replace(" ", ""));
                        }
                        socket.Send(arrSendMsg);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                    continue;
                }
            }
        }

        private void CloseTcpServer() {
            for (int i = dicClient.Count - 1; i >= 0; i--) {
                KeyValuePair<string, Socket> item = dicClient.ElementAt(i);
                Socket socket = item.Value;

                socket.Close();
                dicClient.Remove(item.Key);
            }

            for (int i = dicThread.Count - 1; i >= 0; i--) {
                KeyValuePair<string, Thread> item = dicThread.ElementAt(i);
                Thread thread = item.Value;

                thread.Abort();
                dicThread.Remove(item.Key);
            }

            tcpServer.Close();
            tcpServer = null;
        }
        #endregion
    }
}