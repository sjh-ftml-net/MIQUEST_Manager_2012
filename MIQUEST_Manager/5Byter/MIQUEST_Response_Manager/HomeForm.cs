using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UTILS;

namespace MIQUEST_Response_Manager
{
    public partial class HomeForm : Form
    {
        private Parser cParser;
        private Utils cUtils = new Utils();

        public GlobalData cGlobals = new GlobalData();

        public StreamWriter fLogFile;

        // ------------------------------------------------------------------------------------------
        // Main Loop
        // ------------------------------------------------------------------------------------------
        private void MQ_bt_Run_Click(object sender, EventArgs e)
        {
            MQ_ProgressBar.Value = 0;
            MQ_tb_status.Text = "";

            DateTime dtTimer = DateTime.Now;

            cParser = new Parser(this);

            cGlobals.sInPath = MQ_tb_ResponseFolderPath.Text;
            cGlobals.sOutPath = MQ_tb_OutputFolderPath.Text;


            if (!cGlobals.sInPath.EndsWith("\\")) cGlobals.sInPath = cGlobals.sInPath + "\\";
            if (!cGlobals.sOutPath.EndsWith("\\")) cGlobals.sOutPath = cGlobals.sOutPath + "\\";

            fLogFile = new StreamWriter(cGlobals.sOutPath + String.Format("{0:yyyyMMdd}", DateTime.Today) + "_log.txt");
            cUtils.LogMessage(fLogFile, "Started");

            // Parse input files
            cUtils.LogMessage(fLogFile, "Parsing files");
            cUtils.LogMessage(fLogFile, "Input Folder: " + cGlobals.sInPath);
            cParser.ProcessResponseFiles(cGlobals.sInPath);


            MQ_ProgressBar.Value = MQ_ProgressBar.Maximum;
            cUtils.LogMessage(fLogFile,"Finished");
            cUtils.LogMessage(fLogFile,"Processed in " + DateTime.Now.Subtract(dtTimer).Seconds + " secs (" + DateTime.Now.Subtract(dtTimer).Milliseconds + " ms)");
            fLogFile.Close();
        }

        public HomeForm()
        {
            InitializeComponent();
            SetInterface();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            string app_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!app_path.EndsWith("\\")) app_path = app_path + "\\";
            app_path = app_path + "MIQUESTTEST.ini";
            string sLine;
            string[] sFields;

            if (File.Exists(app_path))
            {
                StreamReader ini_file = new StreamReader(app_path);

                sLine = ini_file.ReadLine();
                while (sLine != null)
                {
                    sLine = sLine.Trim();
                    if (!sLine.StartsWith("#"))
                    {
                        sFields = sLine.Split('=');
                        if (sFields[0].Trim() == "INPUT_PATH")
                            MQ_tb_ResponseFolderPath.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "OUTPUT_PATH")
                            MQ_tb_OutputFolderPath.Text = sFields[1].Trim();

                    }
                    sLine = ini_file.ReadLine();
                }
                ini_file.Close();
            }
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string app_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!Directory.Exists(app_path))
            {
                Directory.CreateDirectory(app_path);
            }

            if (!app_path.EndsWith("\\")) app_path = app_path + "\\";
            app_path = app_path + "MIQUESTTEST.ini";

            StreamWriter ini_file = new StreamWriter(app_path);

            ini_file.WriteLine("INPUT_PATH = " + MQ_tb_ResponseFolderPath.Text);
            ini_file.WriteLine("OUTPUT_PATH = " + MQ_tb_OutputFolderPath.Text);

            ini_file.Close();
        }

        private void MQ_bt_Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MQ_bt_OutputFolderSelect_Click(object sender, EventArgs e)
        {
            MQ_tb_OutputFolderPath.Text = cUtils.FolderDialog(MQ_tb_OutputFolderPath.Text);
        }

        private void MQ_bt_ResponseFolderSelect_Click(object sender, EventArgs e)
        {
            MQ_tb_ResponseFolderPath.Text = cUtils.FolderDialog(MQ_tb_ResponseFolderPath.Text);
        }

        private void MQ_cb_V1_export_CheckedChanged(object sender, EventArgs e)
        {
            SetInterface();
        }

        private void SetInterface()
        {
        }

    }
}
