using System;
using Toolkit.Text;
using Toolkit.EnvironmentX;
using System.Windows.Forms;

namespace Toolkit.Logs
{
    public partial class ToolkitSessionLog : Form
    {
        public ToolkitSessionLog() {
            InitializeComponent();
            Properties.Settings.Default.log_Startup = true;

            list_Logs.Items.Clear();
            list_Logs.Items.Add($"{SystemMessages.tl_DefaultTitleVersion} - Session ID: {Program.sessionID}");
            foreach (string log in Main.sessionLog) list_Logs.Items.Add(log);

            nud_RefreshTimer.Value = Convert.ToDecimal(Properties.Settings.Default.log_refreshTimer) / 1000;
            if (nud_RefreshTimer.Value == 1) lbl_RefreshText.Text = "Refresh every:                      second";
            else lbl_RefreshText.Text = "Refresh every:                      seconds";
            tm_RefreshLogs.Interval = Properties.Settings.Default.log_refreshTimer;
            tm_RefreshLogs.Start();
        }

        private void List_Logs_SelectedIndexChanged(object sender, EventArgs e) { list_Logs.ClearSelected(); }

        private void Btn_Close_Click(object sender, EventArgs e) { Close(); }

        private void Tm_RefreshLogs_Tick(object sender, EventArgs e) {
            list_Logs.Items.Clear();
            list_Logs.Items.Add($"{SystemMessages.tl_DefaultTitleVersion} - Session ID: {Program.sessionID}");
            list_Logs.Items.Add("");
            foreach (string log in Main.sessionLog) list_Logs.Items.Add(log);
        }

        private void nud_RefreshTimer_ValueChanged(object sender, EventArgs e) {
            if (nud_RefreshTimer.Value == 1) lbl_RefreshText.Text = "Refresh every:                      second";
            else lbl_RefreshText.Text = "Refresh every:                      seconds";
            Properties.Settings.Default.log_refreshTimer = tm_RefreshLogs.Interval = decimal.ToInt32(nud_RefreshTimer.Value) * 1000;
        }

        private void ToolkitSessionLog_FormClosing(object sender, FormClosingEventArgs e) {
            Properties.Settings.Default.log_Startup = false;
            Properties.Settings.Default.Save();
        }

        private void btn_TimerEnabled_Click(object sender, EventArgs e) {
            if (tm_RefreshLogs.Enabled) { btn_TimerEnabled.Text = "Resume"; tm_RefreshLogs.Stop(); }
            else { btn_TimerEnabled.Text = "Pause"; tm_RefreshLogs.Start(); }
        }
    }
}
