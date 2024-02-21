using System;
using System.Drawing;
using System.Windows.Forms;
using Acme.Sony9Pin;
using Acme.Sony9Pin.CommandBlocks.Return;
using Acme.Sony9Pin.CommandBlocks.StatusData;
using Acme.Sony9Pin.CommandBlocks.TransportControl;

namespace CueGraphics.Protocol.Sony9Pin
{
    public partial class BVW70ControlPanel : UserControl
    {
        private readonly Sony9PinMaster _master = new Sony9PinMaster();

        /// <summary>
        /// Constructor
        /// </summary>
        public BVW70ControlPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _9PControl_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            var slaves = Sony9PinSlave.Discover();

            if (slaves.Count <= 0) MessageBox.Show("Could not find slave devices");

            cboPorts.Items.Clear();
            foreach (var slave in slaves)
                cboPorts.Items.Add(slave);
            if (cboPorts.Items.Count > 0)
                cboPorts.SelectedIndex = 0;

            DoPowerOff();
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoPowerOn()
        {
            ledPanel.BackColor = Color.FromArgb(255, 45, 0, 0);
            lblTcHours.ForeColor = Color.Red;
            lblTcMinutes.ForeColor = lblTcHours.ForeColor;
            lblTcSeconds.ForeColor = lblTcHours.ForeColor;
            lblTcFrames.ForeColor = lblTcHours.ForeColor;

            labelDolbyNr.ForeColor = Color.LimeGreen;
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoPowerOff()
        {
            txtDeviceName.Text = ".";

            lblTcHours.Text = "0";
            lblTcMinutes.Text = "00";
            lblTcSeconds.Text = "00";
            lblTcFrames.Text = "00";

            ledPanel.BackColor = Color.FromArgb(255, 25, 0, 0);
            lblTcHours.ForeColor = Color.FromArgb(255, 55, 0, 0);
            lblTcMinutes.ForeColor = lblTcHours.ForeColor;
            lblTcSeconds.ForeColor = lblTcHours.ForeColor;
            lblTcFrames.ForeColor = lblTcHours.ForeColor;

            labelDolbyNr.ForeColor = Color.Gray;

            indiStill.ForeColor = Color.Gray;
            //indiForward.ForeColor = Color.Gray;
            //indiStop.ForeColor = indiForward.ForeColor;
            //indiReverse.ForeColor = indiForward.ForeColor;
            //indiRecorder.ForeColor = indiForward.ForeColor;
            //indiPlayer.ForeColor = indiForward.ForeColor;
            //indiJog.ForeColor = indiForward.ForeColor;
            //indiShuttle.ForeColor = indiForward.ForeColor;
            //indiPlay.ForeColor = indiForward.ForeColor;
            //indiRec.ForeColor = indiForward.ForeColor;
        }

        #region Event Reactors
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnMasterDeviceName(object sender, DeviceTypeEventArgs e)
            {
                if (InvokeRequired)
                {
                    BeginInvoke((MethodInvoker)(() => OnMasterDeviceName(sender, e)));
                    return;
                }

                txtDeviceName.Text = e.DeviceName;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnMasterConnectedChanged(object sender, ConnectedEventArgs e)
            {
                if (InvokeRequired)
                {
                    BeginInvoke((MethodInvoker)(() => OnMasterConnectedChanged(sender, e)));
                    return;
                }

                if (e.Connected)
                    DoPowerOn();
                else
                    DoPowerOff();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnMasterTimeDataChanged(object sender, TimeDataEventArgs e)
            {
                if (InvokeRequired)
                {
                    BeginInvoke((MethodInvoker)(() => OnMasterTimeDataChanged(sender, e)));
                    return;
                }

                lblTcHours.Text = e.TimeCode.Hours.ToString("D1");
                lblTcMinutes.Text = e.TimeCode.Minutes.ToString("D2");
                lblTcSeconds.Text = e.TimeCode.Seconds.ToString("D2");
                lblTcFrames.Text = e.TimeCode.Frames.ToString("D2");
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void OnMasterStatusDataChanged(object sender, StatusDataEventArgs ea)
            {
                if (InvokeRequired)
                {
                    BeginInvoke((MethodInvoker)(() => OnMasterStatusDataChanged(sender, ea)));
                    return;
                }

                btnPlay.ButtonColor = (ea.StatusData.Play) ? Color.Yellow : Color.White;
                btnFastFwd.ButtonColor = (ea.StatusData.FastFwd) ? Color.Yellow : Color.White;
                btnRewind.ButtonColor = (ea.StatusData.Rewind) ? Color.Yellow : Color.White;
                btnPreroll.ButtonColor = (ea.StatusData.Preroll) ? Color.Yellow : Color.White;
                btnEdit.ButtonColor = (ea.StatusData.Edit) ? Color.Yellow : Color.White;
                btnVar.ButtonColor = (ea.StatusData.Var) ? Color.Yellow : Color.White;
                btnStop.ButtonColor = (ea.StatusData.Stop) ? Color.Green : Color.DodgerBlue;
                btnEject.ButtonColor = (ea.StatusData.Eject) ? Color.Green : Color.DodgerBlue;

                indiStill.ForeColor = (ea.StatusData.Still) ? Color.Red : Color.Gray;

//                indiReverse.ForeColor = (ea.StatusData.) ? Color.Green : Color.White;
                indiForward.ForeColor = (!ea.StatusData.TapeDir) ? Color.Green : Color.White;

                indiJog.ForeColor = (ea.StatusData.Jog) ? Color.Red : Color.Gray;
                indiShuttle.ForeColor = (ea.StatusData.Shuttle) ? Color.Red : Color.Gray;

                indiRecorder.ForeColor = indiForward.ForeColor;
                indiPlayer.ForeColor = indiForward.ForeColor;
                indiServo.ForeColor = (ea.StatusData.ServoLock) ? Color.Green : Color.White;
                indiRecInhi.ForeColor = (ea.StatusData.RecInhib) ? Color.Red : Color.White;
            }
        #endregion

        #region Buttons clicked

            private void btnStandby_Click(object sender, EventArgs e)
            {
                if (_master.StatusData.Standby)
                    _master.Command(new StandbyOff());
                else
                    _master.Command(new StandbyOn());
            }

            private void btnPreroll_Click(object sender, EventArgs e)
            {
                _master.Command(new Preroll());
            }

            private void btnRecord_Click(object sender, EventArgs e)
            {
                _master.Command(new Record());
            }

            private void btnEdit_Click(object sender, EventArgs e)
            {
                _master.Command(new EditOn());
            }

            private void btnEject_Click(object sender, EventArgs e)
            {
                _master.Command(new Eject());
            }

            private void btnRewind_Click(object sender, EventArgs e)
            {
                _master.Command(new Rewind());
            }

            private void btnPlay_Click(object sender, EventArgs e)
            {
                _master.Command(new Play());
            }

            private void btnFastFwd_Click(object sender, EventArgs e)
            {
                _master.Command(new FastFwd());
            }

            private void btnStop_Click(object sender, EventArgs e)
            {
                _master.Command(new Stop());
            }

            private void btnVar_Click(object sender, EventArgs e)
            {
                if (_master.StatusData.Shuttle)
                    _master.Command(new JogFwd(0));
                else
                    _master.Command(new ShuttleFwd(0));
            }

            private void btnPreview_Click(object sender, EventArgs e)
            {
                _master.Command(new Preview());
            }

            private void btnAutoEdit_Click(object sender, EventArgs e)
            {
                _master.Command(new AutoEdit());
            }

            private void btnReview_Click(object sender, EventArgs e)
            {
                _master.Command(new Review());
            }

            private void btnSearchDial_MouseDown(object sender, MouseEventArgs e)
            {
                Console.WriteLine("btnSearchDial_MouseDown");
            }

            private void btnSearchDial_MouseHover(object sender, EventArgs e)
            {
                Console.WriteLine("btnSearchDial_MouseHover");
            }

            private void btnSearchDial_MouseEnter(object sender, EventArgs e)
            {
                Console.WriteLine("btnSearchDial_MouseEnter");
            }

            private void btnSearchDial_MouseLeave(object sender, EventArgs e)
            {
                Console.WriteLine("btnSearchDial_MouseLeave");
            }

            private void btnSearchDial_MouseMove(object sender, MouseEventArgs e)
            {
                Console.WriteLine("btnSearchDial_MouseMove");
            }




            private void chkPower_CheckedChanged(object sender, EventArgs e)
            {
                if (chkPower.Checked)
                {
                    var selectedItem = cboPorts.SelectedItem as string;
                    if (selectedItem == null) return;

                    _master.DeviceType += OnMasterDeviceName;
                    _master.ConnectedChanged += OnMasterConnectedChanged;
                    _master.TimeDataChanged += OnMasterTimeDataChanged;
                    _master.StatusDataChanged += OnMasterStatusDataChanged;

                    //_master.OnProtocolError += MasterProtocolError;
                    //_master.OnConnected += MasterConnected;
                    //_master.StatusData += MasterStatusData;
                    //_master.DeviceType += MasterDeviceName;
                    //_master.TimeData += MasterTimeData;

                    var portName = selectedItem;
                    _master.Open(portName);
                }
                else
                {
                    _master.DeviceType -= OnMasterDeviceName;
                    _master.ConnectedChanged -= OnMasterConnectedChanged;
                    _master.TimeDataChanged -= OnMasterTimeDataChanged;
                    _master.StatusDataChanged -= OnMasterStatusDataChanged;

                    _master.Close();

                    DoPowerOff();

                    //_master.OnProtocolError -= MasterProtocolError;
                    //_master.OnConnected -= MasterConnected;
                    //_master.StatusData -= MasterStatusData;
                    //_master.DeviceType -= MasterDeviceName;
                    //_master.TimeData -= MasterTimeData;
                }
            }

        #endregion

    }
}
