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

        public MQ_Encounters cEncounters;
        public MQ_Journals cJournals;
        public MQ_Referrals cReferrals;
        public MQ_Patients cPatients;

        //public string sPracticeCode = "";

        public string FatalErrorMessage = "";
        public bool FatalError = false;
        public string WarningMessage = "";
        public StreamWriter fLogFile;

        // ------------------------------------------------------------------------------------------
        // Main Loop
        // ------------------------------------------------------------------------------------------
        private void MQ_bt_Run_Click(object sender, EventArgs e)
        {
            FatalError = false;
            FatalErrorMessage = "";
            
            MQ_ProgressBar.Value = 0;
            MQ_tb_status.Text = "";

            DateTime dtTimer = DateTime.Now;

            cParser = new Parser(this);
            cEncounters = new MQ_Encounters();
            cJournals = new MQ_Journals();
            cReferrals = new MQ_Referrals();
            cPatients = new MQ_Patients();

            cGlobals.sInPath = MQ_tb_ResponseFolderPath.Text;
            cGlobals.sOutPath = MQ_tb_OutputFolderPath.Text;
            cGlobals.bV1Export = MQ_cb_V1_export.Checked;
            cGlobals.bV1Initial = MQ_rb_V1_initial.Checked;
            cGlobals.bV1Bulk = MQ_rb_V1_bulk.Checked;
            cGlobals.bPseudonymise = MQ_cb_Pseudonymise.Checked;
            cGlobals.bPseudoDateShift = MQ_cb_PseudonymiseDateShift.Checked;
            if (cGlobals.bPseudoDateShift)
                cGlobals.iPseudoDateShift = cUtils.RandomNumber(0, 10);
            else
                cGlobals.iPseudoDateShift = 0;

            if (!cGlobals.sInPath.EndsWith("\\")) cGlobals.sInPath = cGlobals.sInPath + "\\";
            if (!cGlobals.sOutPath.EndsWith("\\")) cGlobals.sOutPath = cGlobals.sOutPath + "\\";

            if (MQ_cb_V1_export.Checked)
            {
                // Create the Sollis A2 folder structure (use todays date)
                string a2path = DateTime.Today.Year + "\\" + DateTime.Today.Month + "\\" + DateTime.Today.Day;
                cGlobals.sOutPath += a2path;
                if (!cGlobals.sOutPath.EndsWith("\\")) cGlobals.sOutPath = cGlobals.sOutPath + "\\";
            }

            if (!Directory.Exists(cGlobals.sInPath))
            {
                FatalErrorMessage = "Response folder not found";
                FatalError = true;
            }
            else if (!cUtils.CheckCreateFolder(cGlobals.sOutPath))
            {
                FatalErrorMessage = "Output folder not found";
                FatalError = true;
            }

            if (!FatalError)
            {
                fLogFile = new StreamWriter(cGlobals.sOutPath + String.Format("{0:yyyyMMdd}", DateTime.Today) + "_log.txt");
                cUtils.LogMessage(fLogFile, "Started");

                // Parse input files
                cUtils.LogMessage(fLogFile, "Parsing files");
                cUtils.LogMessage(fLogFile, "Input Folder: " + cGlobals.sInPath);
                cParser.ProcessResponseFiles(cGlobals.sInPath);

                // Assess Data
                cUtils.LogMessage(fLogFile, "Assessing data");
                cPatients.Assess();
                cJournals.Assess();
                cEncounters.Assess();
                cReferrals.Assess();

                if (cGlobals.bPseudonymise)
                {
                    cGlobals.sPracticeCode = cUtils.Pseudomymise(cGlobals.sPracticeCode, "PRACTICE", cGlobals.iPseudoDateShift);
                    cGlobals.dtQueryDate = Convert.ToDateTime(cUtils.Pseudomymise(cGlobals.dtQueryDate.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                    cEncounters.dtEnd = Convert.ToDateTime(cUtils.Pseudomymise(cEncounters.dtEnd.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                    cEncounters.dtStart = Convert.ToDateTime(cUtils.Pseudomymise(cEncounters.dtStart.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                    cJournals.dtEnd = Convert.ToDateTime(cUtils.Pseudomymise(cJournals.dtEnd.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                    cJournals.dtStart = Convert.ToDateTime(cUtils.Pseudomymise(cJournals.dtStart.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                    cReferrals.dtEnd = Convert.ToDateTime(cUtils.Pseudomymise(cReferrals.dtEnd.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                    cReferrals.dtStart = Convert.ToDateTime(cUtils.Pseudomymise(cReferrals.dtStart.ToShortDateString(), "DATE", cGlobals.iPseudoDateShift));
                }

                // Log some facts
                if (cGlobals.bPseudonymise)
                    cUtils.LogMessage(fLogFile, "Psuedonymising");
                cUtils.LogMessage(fLogFile, "Practice Code: " + cGlobals.sPracticeCode);
                cUtils.LogMessage(fLogFile, "Query Date: " + cGlobals.dtQueryDate.ToShortDateString());
                cUtils.LogMessage(fLogFile, "PATIENTS: " + cPatients.Describe());
                cUtils.LogMessage(fLogFile, "JOURNALS: " + cJournals.Describe());
                cUtils.LogMessage(fLogFile, "ENCOUNTERS: " + cEncounters.Describe());
                cUtils.LogMessage(fLogFile, "REFERRALS: " + cReferrals.Describe());

                // Version 2 Save
                if (MQ_cb_V2_export.Checked)
                {
                    cUtils.LogMessage(fLogFile, "Saving Version 2 Format Files");
                    cUtils.LogMessage(fLogFile, "Output Folder: " + cGlobals.sOutPath);
                    if (!cPatients.Save(cGlobals))
                    {
                        WarningMessage = "Data issues in Patient file";
                        cUtils.LogMessage(fLogFile, WarningMessage);
                    }
                    cUtils.LogMessage(fLogFile, "Written Patient file");

                    if (!cJournals.Save(cGlobals))
                    {
                        WarningMessage = "Data issues in Journal file";
                        cUtils.LogMessage(fLogFile, WarningMessage);
                    }
                    cUtils.LogMessage(fLogFile, "Written Journal file");

                    if (!cEncounters.Save(cGlobals))
                    {
                        WarningMessage = "Data issues in Encounter file";
                        cUtils.LogMessage(fLogFile, WarningMessage);
                    }
                    cUtils.LogMessage(fLogFile, "Written Encounter file");

                    if (!cReferrals.Save(cGlobals))
                    {
                        WarningMessage = "Data issues in Referral file";
                        cUtils.LogMessage(fLogFile, WarningMessage);
                    }
                    cUtils.LogMessage(fLogFile, "Written Referral file");
                }

                // Version 1 Save
                if (cGlobals.bV1Export)
                {
                    cGlobals.CreateV1Header();
                    cUtils.LogMessage(fLogFile, "Saving Version 1 Format Files");
                    cUtils.LogMessage(fLogFile, "Output Folder: " + cGlobals.sOutPath);

                    int iProgInc = (MQ_ProgressBar.Maximum - MQ_ProgressBar.Value) / 3;

                    cUtils.LogMessage(fLogFile, "Writing V1 Output Files");
                    cPatients.WriteV1File(cGlobals);
                    cUtils.LogMessage(fLogFile, "Written Patient file");
                    MQ_ProgressBar.Value = MQ_ProgressBar.Value + iProgInc;
                    cJournals.WriteV1File(cGlobals);
                    cUtils.LogMessage(fLogFile, "Written Journal file");
                    MQ_ProgressBar.Value = MQ_ProgressBar.Value + iProgInc;
                    cEncounters.WriteV1File(cGlobals);
                    cUtils.LogMessage(fLogFile, "Written Encounter file");
                    MQ_ProgressBar.Value = MQ_ProgressBar.Value + iProgInc;
                }

                MQ_ProgressBar.Value = MQ_ProgressBar.Maximum;
                cUtils.LogMessage(fLogFile,"Finished");
                cUtils.LogMessage(fLogFile,"Processed in " + DateTime.Now.Subtract(dtTimer).Seconds + " secs (" + DateTime.Now.Subtract(dtTimer).Milliseconds + " ms)");
                fLogFile.Close();
            }

            if (FatalError)
            {
                MessageBox.Show("ERROR: " + FatalErrorMessage);
                MQ_tb_status.Text = FatalErrorMessage;
            }
            else
            {
                if (WarningMessage != "")
                {
                    MessageBox.Show("WARNING: " + WarningMessage);
                    MQ_tb_status.Text = WarningMessage;
                }
                else
                     MQ_tb_status.Text = "Complete";
            }
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
            app_path = app_path + "MIQUESTResponseManager.ini";
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
                        else if (sFields[0].Trim() == "V1_EXPORT")
                            MQ_cb_V1_export.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "V1_BULK")
                            MQ_rb_V1_bulk.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "V1_INITIAL")
                            MQ_rb_V1_initial.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "V2_EXPORT")
                            MQ_cb_V2_export.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "PSEUDO")
                            MQ_cb_Pseudonymise.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "PSEUDO_DATE_SHIFT")
                            MQ_cb_PseudonymiseDateShift.Checked = Convert.ToBoolean(sFields[1].Trim());

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
            app_path = app_path + "MIQUESTResponseManager.ini";

            StreamWriter ini_file = new StreamWriter(app_path);

            ini_file.WriteLine("INPUT_PATH = " + MQ_tb_ResponseFolderPath.Text);
            ini_file.WriteLine("OUTPUT_PATH = " + MQ_tb_OutputFolderPath.Text);
            ini_file.WriteLine("V1_EXPORT = " + Convert.ToString(MQ_cb_V1_export.Checked));
            ini_file.WriteLine("V1_BULK = " + Convert.ToString(MQ_rb_V1_bulk.Checked));
            ini_file.WriteLine("V1_INITIAL = " + Convert.ToString(MQ_rb_V1_initial.Checked));
            ini_file.WriteLine("V2_EXPORT = " + Convert.ToString(MQ_cb_V2_export.Checked));
            ini_file.WriteLine("PSEUDO = " + Convert.ToString(MQ_cb_Pseudonymise.Checked));
            ini_file.WriteLine("PSEUDO_DATE_SHIFT = " + Convert.ToString(MQ_cb_PseudonymiseDateShift.Checked));

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
            if (MQ_cb_V1_export.Checked)
            {
                MQ_rb_V1_bulk.Enabled = true;
                MQ_rb_V1_initial.Enabled = true;
            }
            else
            {
                MQ_rb_V1_bulk.Enabled = false;
                MQ_rb_V1_initial.Enabled = false;
            }
            if (MQ_cb_Pseudonymise.Checked)
            {
                MQ_cb_PseudonymiseDateShift.Enabled = true;
            }
            else
            {
                MQ_cb_PseudonymiseDateShift.Enabled = false;
            }
        }

        private void MQ_cb_Pseudonymise_CheckedChanged(object sender, EventArgs e)
        {
            SetInterface();
        }

    }
}
