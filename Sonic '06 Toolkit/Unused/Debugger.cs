using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Debugger : Form
    {
        public Debugger()
        {
            InitializeComponent();
        }

        void Debugger_Load(object sender, EventArgs e)
        {
            tm_Update.Start();
        }

        void Tm_Update_Tick(object sender, EventArgs e)
        {
            #region Global
            if (Global.versionNumber != null) versionNumber.Text = Global.versionNumber; else versionNumber.Text = "None";
            if (Global.latestVersion != null) latestVersion.Text = Global.latestVersion; else latestVersion.Text = "None";
            if (Global.serverStatus != null) serverStatus.Text = Global.serverStatus; else serverStatus.Text = "None";
            if (Global.currentPath != null) currentPath.Text = Global.currentPath; else currentPath.Text = "None";
            if (Global.updateState != null) updateState.Text = Global.updateState; else updateState.Text = "None";
            if (Global.exisoState != null) exisoState.Text = Global.exisoState; else exisoState.Text = "None";
            if (Global.arcState != null) arcState.Text = Global.arcState; else arcState.Text = "None";
            if (Global.adxState != null) adxState.Text = Global.adxState; else adxState.Text = "None";
            if (Global.at3State != null) at3State.Text = Global.at3State; else at3State.Text = "None";
            if (Global.csbState != null) csbState.Text = Global.csbState; else csbState.Text = "None";
            if (Global.ddsState != null) ddsState.Text = Global.ddsState; else ddsState.Text = "None";
            if (Global.lubState != null) lubState.Text = Global.lubState; else lubState.Text = "None";
            if (Global.setState != null) setState.Text = Global.setState; else setState.Text = "None";
            if (Global.mstState != null) mstState.Text = Global.mstState; else mstState.Text = "None";
            if (Global.xnoState != null) xnoState.Text = Global.xnoState; else xnoState.Text = "None";
            if (Global.applicationData != null) applicationData.Text = Global.applicationData; else applicationData.Text = "None";
            sessionID.Text = Global.sessionID.ToString();
            getIndex.Text = Global.getIndex.ToString();
            javaCheck.Text = Global.javaCheck.ToString();
            gameChanged.Text = Global.gameChanged.ToString();
            #endregion

            #region Settings
            showSessionID.Text = Properties.Settings.Default.showSessionID.ToString();
            if (Properties.Settings.Default.theme != "") theme.Text = Properties.Settings.Default.theme; else theme.Text = "None";
            if (Properties.Settings.Default.rootPath != "") rootPath.Text = Properties.Settings.Default.rootPath; else rootPath.Text = "None";
            if (Properties.Settings.Default.toolsPath != "") toolsPath.Text = Properties.Settings.Default.toolsPath; else toolsPath.Text = "None";
            if (Properties.Settings.Default.archivesPath != "") archivesPath.Text = Properties.Settings.Default.archivesPath; else archivesPath.Text = "None";
            disableSoftwareUpdater.Text = Properties.Settings.Default.disableSoftwareUpdater.ToString();
            if (Properties.Settings.Default.unlubPath != "") unlubPath.Text = Properties.Settings.Default.unlubPath; else unlubPath.Text = "None";
            if (Properties.Settings.Default.xnoPath != "") xnoPath.Text = Properties.Settings.Default.xnoPath; else xnoPath.Text = "None";
            if (Properties.Settings.Default.unpackFile != "") unpackFile.Text = Properties.Settings.Default.unpackFile; else unpackFile.Text = "None";
            if (Properties.Settings.Default.repackFile != "") repackFile.Text = Properties.Settings.Default.repackFile; else repackFile.Text = "None";
            if (Properties.Settings.Default.xnoFile != "") xnoFile.Text = Properties.Settings.Default.xnoFile; else xnoFile.Text = "None";
            if (Properties.Settings.Default.csbFile != "") csbFile.Text = Properties.Settings.Default.csbFile; else csbFile.Text = "None";
            if (Properties.Settings.Default.adx2wavFile != "") adx2wavFile.Text = Properties.Settings.Default.adx2wavFile; else adx2wavFile.Text = "None";
            if (Properties.Settings.Default.criconverterFile != "") criconverterFile.Text = Properties.Settings.Default.criconverterFile; else criconverterFile.Text = "None";
            showLogo.Text = Properties.Settings.Default.showLogo.ToString();
            unpackAndLaunch.Text = Properties.Settings.Default.unpackAndLaunch.ToString();
            if (Properties.Settings.Default.gamePath != "") gamePath.Text = Properties.Settings.Default.gamePath; else gamePath.Text = "None";
            if (Properties.Settings.Default.exisoFile != "") exisoFile.Text = Properties.Settings.Default.exisoFile; else exisoFile.Text = "None";
            if (Properties.Settings.Default.at3File != "") at3File.Text = Properties.Settings.Default.at3File; else at3File.Text = "None";
            if (Properties.Settings.Default.xeniaFile != "") xeniaFile.Text = Properties.Settings.Default.xeniaFile; else xeniaFile.Text = "None";
            if (Properties.Settings.Default.directXFile != "") directXFile.Text = Properties.Settings.Default.directXFile; else directXFile.Text = "None";
            #endregion

            foreach (Control x in this.Controls)
            {
                if (x is Label)
                {
                    ((Label)x).Update();
                }
            }
        }

        void Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
        }
    }
}
