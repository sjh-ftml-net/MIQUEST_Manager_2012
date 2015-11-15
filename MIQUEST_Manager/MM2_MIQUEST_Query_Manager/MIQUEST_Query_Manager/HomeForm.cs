/*
 * MIQUEST Query Manager
 * 
 * History
 * -------
 * Version  1.00    17/10/2011  Simon Heathfield    First Issue
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace MIQUEST_Query_Manager
{


    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();

            ifDtQueryEndDate.Value = DateTime.Today;
            SetInterface();
        }

        #region VARS

        // Error Processing
        public string sErrorMessage = "";
        public bool bFatal = false;

        // HQL Elements
        public string sHQLHeaderText;

        public HQLCohort cCohorts = new HQLCohort();
        public HQLJournal cJournal = new HQLJournal();

        public StreamWriter fLogFile;

        public DateTime dtQueryEndDate;
        public DateTime dtQueryLTCEndDate;
        public DateTime dtQueryStartDate;
        public DateTime dtQueryLTCStartDate;
        // End of HQL Elements

        private Utils cUtils = new Utils();

        public string sInPath;
        public string sOutPath;

        public string sSetID = "MQM";
        public string sSetDescription = "MQM Export";
        public bool bRemote = false;
        public string sPcode = "PCODE";

        #endregion

        private void SetDates()
        {
            dtQueryEndDate = ifDtQueryEndDate.Value;
            dtQueryStartDate = dtQueryEndDate.AddMonths(0 - Convert.ToInt16(ifTbExportPeriod.Text));
            dtQueryLTCStartDate = dtQueryEndDate.AddMonths(0 - Convert.ToInt16(ifTbLTCExportPeriod.Text));
            dtQueryLTCEndDate = dtQueryStartDate;
        }

        //----------------------------------------------------------------------------------------------------
        // Main Loop
        //----------------------------------------------------------------------------------------------------
        private void ifBtRun_Click(object sender, EventArgs e)
        {
            bool bFatal = false;
            DateTime dtTimer = DateTime.Now;

            ifProgressBar.Value = 0;

            ifTbStatus.Text = "Running";
            sInPath = ifTbInputPath.Text;
            sOutPath = ifTbOutputPath.Text;

            if (!File.Exists(sInPath))
            {
               ifTbStatus.Text = "Template file not found";
                bFatal = true;
            }
            else
            {
                if (!cUtils.CheckCreateFolder(sOutPath))
                {
                    ifTbStatus.Text = "Output folder not found";
                    bFatal = true;
                }
                if (!sOutPath.EndsWith("\\")) sOutPath = sOutPath + "\\";

                if (ifCbClearFolder.Checked)
                {
                    cUtils.ClearFolder(sOutPath);
                }
            }

            if (!bFatal)
            {
                // Start the real processing
                string sLogfilePath = sOutPath + String.Format("{0:yyyyMMdd}", DateTime.Today) + "_log.txt";
                fLogFile = new StreamWriter(sLogfilePath);
                fLogFile.WriteLine("MIQUEST Manager Started at " + DateTime.Now);
                fLogFile.WriteLine("");
                fLogFile.WriteLine("Global Query Settings...");

                SetDates();

                fLogFile.WriteLine("End Date:       " + dtQueryEndDate.ToShortDateString());
                fLogFile.WriteLine("Start Date:     " + dtQueryStartDate.ToShortDateString());
                fLogFile.WriteLine("LTC End Date:   " + dtQueryLTCEndDate.ToShortDateString());
                fLogFile.WriteLine("LTC Start Date: " + dtQueryLTCStartDate.ToShortDateString());

                if (ifCbFileSplit.Checked)
                {
                    fLogFile.WriteLine("Splitting files into " + ifTbExportPeriod.Text + " " + ifCbFileSplitWeeksMonths.Text + " periods");
                    fLogFile.WriteLine("Splitting LTC files into " + ifTbExportSplitLTC.Text + " " + ifCbFileSplitWeeksMonthsLTC.Text + " periods");
                }
                else
                {
                    fLogFile.WriteLine("No file split selected");
                }

                if (ifCbScheduled.Checked)
                    fLogFile.WriteLine("Scheduled Query: From " + ifDtScheduledStart.Value.ToShortDateString() + " to " + ifDtScheduledEnd.Value.ToShortDateString() + " each " + ifTbScheduledPeriod.Text + " " + ifCbScheduledWeekMonth.Text + "s");
                else
                   fLogFile.WriteLine("No query schedule");

                if (ifCbRemote.Checked)
                    fLogFile.WriteLine("Remote Query: Respondent = " + ifTbPracticeCode.Text + ", Media = " + ifCbMedia.Text);
                else
                    fLogFile.WriteLine("Local query");

                // Parse the template
                fLogFile.WriteLine("");
                fLogFile.WriteLine("Begun parsing template...");
                Parser cParser = new Parser(this);
                cParser.Parse(sInPath, fLogFile);
                if (cParser.FatalError)
                {
                    bFatal = true;
                    ifTbStatus.Text = cParser.ErrorMessage;
                }

                // If all OK Generate the ouptput
                if (!bFatal)
                {

                    if (ifTbQuerySetName.Text != "")
                        sSetID = ifTbQuerySetName.Text;
                    if (ifTbQuerySetDescription.Text != "")
                        sSetDescription = ifTbQuerySetDescription.Text;

                    fLogFile.WriteLine("");
                    fLogFile.WriteLine("Begun generating HQL files...");
                    HQLGenerator cHQLGen = new HQLGenerator(this);
                    cHQLGen.Generate();

                        // Finished Processing
                    if (cHQLGen.FatalError)
                    {
                        bFatal = true;
                        ifTbStatus.Text = cHQLGen.ErrorMessage;
                    }
                    else
                    {
                        ifTbStatus.Text = "Complete";
                    }
                }

                cCohorts.Clear();
                cJournal.Clear();

                if (bFatal)
                {
                    fLogFile.WriteLine("Fatal Error: " + ifTbStatus.Text);
                }

                fLogFile.WriteLine("");
                fLogFile.WriteLine("MIQUEST Manager Completed at " + DateTime.Now);

                fLogFile.WriteLine("Processed in " + DateTime.Now.Subtract(dtTimer).Seconds + " secs (" + DateTime.Now.Subtract(dtTimer).Milliseconds + " ms)");

                fLogFile.Close();

                if (bFatal)
                {
                    MessageBox.Show(ifTbStatus.Text);
                }
            }
        }

        #region HEADER

        //private string xxPrepareHQLHeader(string sSetID, string sSetDescription, string sSystemType, bool bRemote, string sPcode)
        //{
        //    string sBuffer = "";

        //    // The date the query was written (e.g. *QRY_WDATE,20110523,23/05/2011)
        //    sBuffer  = "*QRY_WDATE," + String.Format("{0:yyyyMMdd}", DateTime.Today) + "," + String.Format("{0:dd/MM/yyyy}", DateTime.Today) + "\r\n";
        //    // The query title (e.g. *QRY_TITLE,DIA001G,Subset)
        //    sBuffer = sBuffer + "*QRY_TITLE," + "<TITLE>" + "," + "<DESC>" + "\r\n";
        //    // The order in which to run the query (e.g. *QRY_ORDER,005,)
        //    sBuffer = sBuffer + "*QRY_ORDER," + "<ORDER>" + ",\r\n";
        //    // The Data Collection Agreement (e.g. *QRY_AGREE,LOCAL,)
        //    sBuffer = sBuffer + "*QRY_AGREE,LOCAL," + ",\r\n";
        //    // The identity of the enquirer (e.g. *ENQ_IDENT,LOCAL,)
        //    sBuffer = sBuffer + "*ENQ_IDENT,LOCAL," + ",\r\n";
        //    // The set of queries to which this belongs (e.g. *QRY_SETID,CTV3, CTV3 set)
        //    sBuffer = sBuffer + "*QRY_SETID," + sSetID + "," + sSetDescription + ",\r\n";

        //    // This will need to respect the EMIS mixture....

        //    // The coding scheme used (e.g. *QRY_CODES,0,9999R3,Read Version 3)
        //    if (sSystemType == "CTV3 Systems" || sSystemType == "TPP SystmOne")
        //        sBuffer = sBuffer + "*QRY_CODES,0,9999R3,Read Version 3" + ",\r\n";
        //    else if (sSystemType == "READ2 Systems")
        //        sBuffer = sBuffer + "*QRY_CODES,0,9999R2,Read Version 2" + ",\r\n";
        //    else if (sSystemType == "EMIS Sytems")
        //        sBuffer = sBuffer + "*QRY_CODES,0,9999BN,BNF Drug Codes" + ",\r\n";
        //    else
        //        MessageBox.Show("Unknown System Type - call the dozy programmer");

        //    // REMOTE ENQUIRY

        //    if (bRemote)
        //    {
        //        // The identity of the target (e.g. *ENQ_RSPID,B12345,Longtown Surgery
        //        sBuffer = sBuffer + "*ENQ_RSPID," + sPcode + "," + ",\r\n";
        //        // Media for responses, D or N (e.g. *QRY_MEDIA,D,DISK) - Only mandated for REMOTE
        //        sBuffer = sBuffer + "*QRY_MEDIA,D,DISK" + ",\r\n";
        //        // The date the query was submitted (e.g. *QRY_SDATE,20110201,01/02/2011) - Only mandated for REMOTE
        //        sBuffer = sBuffer + "*QRY_SDATE," + String.Format("{0:yyyyMMdd}", DateTime.Today) + "," + String.Format("{0:dd/MM/yyyy}", DateTime.Today) + ",\r\n";
        //    }

        //    return sBuffer;

        //}

        #endregion

        #region CONTROLS

        private void ifCbFileSplit_CheckedChanged(object sender, EventArgs e)
        {
            SetInterface();
        }

        private void ifCbScheduled_CheckedChanged(object sender, EventArgs e)
        {
            SetInterface();
            ifDtScheduledStart.Value = DateTime.Today;
            ifDtScheduledEnd.Value = DateTime.Today.AddYears(1);
        }

        private void ifTbExportPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //accept only numbers and back space.
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ifTbLTCExportPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            //accept only numbers and back space.
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ifTbExportSplit_KeyPress(object sender, KeyPressEventArgs e)
        {
            //accept only numbers and back space.
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ifTbExportSplitLTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            //accept only numbers and back space.
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void ifBtSelectInputPath_Click(object sender, EventArgs e)
        {
            ifTbInputPath.Text = cUtils.FileDialog(ifTbInputPath.Text);
        }

        private void ifBtSelectOutputPath_Click(object sender, EventArgs e)
        {
            ifTbOutputPath.Text = cUtils.FolderDialog(ifTbOutputPath.Text);
        }

        private void ifBtQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string sAppPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (!Directory.Exists(sAppPath))
            {
                Directory.CreateDirectory(sAppPath);
            }

            if (!sAppPath.EndsWith("\\")) sAppPath = sAppPath + "\\";
            sAppPath = sAppPath + "MIQUESTQueryManager.ini";

            StreamWriter fIniFile = new StreamWriter(sAppPath);

            fIniFile.WriteLine("INPATH = " + ifTbInputPath.Text);
            fIniFile.WriteLine("OUTPATH = " + ifTbOutputPath.Text);
            fIniFile.WriteLine("DELETE_FILES = " + Convert.ToString(ifCbClearFolder.Checked));
            fIniFile.WriteLine("SET_NAME = " + ifTbQuerySetName.Text);
            fIniFile.WriteLine("SET_DESCRIPTION = " + ifTbQuerySetDescription.Text);
            fIniFile.WriteLine("SYSTEM_TYPE = " + ifCbSystemType.Text);
            fIniFile.WriteLine("EXPORT_PERIOD = " + ifTbExportPeriod.Text);
            fIniFile.WriteLine("LTC_EXPORT_PERIOD = " + ifTbLTCExportPeriod.Text);
            fIniFile.WriteLine("FILE_SPLIT = " + Convert.ToString(ifCbFileSplit.Checked));
            fIniFile.WriteLine("POPULATION = " + ifTbPracticePopulation.Text);
            fIniFile.WriteLine("EXPORT_SPLIT = " + ifTbExportSplit.Text);
            fIniFile.WriteLine("LTC_EXPORT_SPLIT = " + ifTbExportSplitLTC.Text);
            fIniFile.WriteLine("EXPORT_SPLIT_WM = " + ifCbFileSplitWeeksMonths.Text);
            fIniFile.WriteLine("LTC_EXPORT_SPLIT_WM = " + ifCbFileSplitWeeksMonthsLTC.Text);

            fIniFile.WriteLine("ENCOUNTER_GEN = " + Convert.ToString(ifCbEncounterGen.Checked));
            fIniFile.WriteLine("REFERRAL_GEN = " + Convert.ToString(ifCbReferralGen.Checked));

            fIniFile.WriteLine("SCHEDULED = " + Convert.ToString(ifCbScheduled.Checked));
            fIniFile.WriteLine("SCHEDULED_START = " + ifDtScheduledStart.Text);
            fIniFile.WriteLine("SCHEDULED_END = " + ifDtScheduledEnd.Text);
            fIniFile.WriteLine("SCHEDULED_WM = " + ifCbScheduledWeekMonth.Text);
            fIniFile.WriteLine("SCHEDULED_PERIOD = " + ifTbScheduledPeriod.Text);

            fIniFile.WriteLine("REMOTE = " + Convert.ToString(ifCbRemote.Checked));
            fIniFile.WriteLine("PCODE = " + ifTbPracticeCode.Text);
            fIniFile.WriteLine("MEDIA = " + ifCbMedia.Text);

            fIniFile.Close();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            string sAppPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!sAppPath.EndsWith("\\")) sAppPath = sAppPath + "\\";
            sAppPath = sAppPath + "MIQUESTQueryManager.ini";
            string sLine;
            string[] sFields;

            if (File.Exists(sAppPath))
            {
                StreamReader fIniFile = new StreamReader(sAppPath);

                sLine = fIniFile.ReadLine();
                while (sLine != null)
                {
                    sLine = sLine.Trim();
                    if (!sLine.StartsWith("#"))
                    {
                        sFields = sLine.Split('=');
                        if (sFields[0].Trim() == "INPATH")
                            ifTbInputPath.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "OUTPATH")
                            ifTbOutputPath.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "DELETE_FILES")
                            ifCbClearFolder.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "SET_NAME")
                            ifTbQuerySetName.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "SET_DESCRIPTION")
                            ifTbQuerySetDescription.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "SYSTEM_TYPE")
                            ifCbSystemType.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "EXPORT_PERIOD")
                            ifTbExportPeriod.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "LTC_EXPORT_PERIOD")
                            ifTbLTCExportPeriod.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "FILE_SPLIT")
                            ifCbFileSplit.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "POPULATION")
                            ifTbPracticePopulation.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "EXPORT_SPLIT")
                            ifTbExportSplit.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "LTC_EXPORT_SPLIT")
                            ifTbExportSplitLTC.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "EXPORT_SPLIT_WM")
                            ifCbFileSplitWeeksMonths.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "LTC_EXPORT_SPLIT_WM")
                            ifCbFileSplitWeeksMonthsLTC.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "SCHEDULED")
                            ifCbScheduled.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "SCHEDULED_START")
                            ifDtScheduledStart.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "SCHEDULED_END")
                            ifDtScheduledEnd.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "SCHEDULED_WM")
                            ifCbScheduledWeekMonth.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "SCHEDULED_PERIOD")
                            ifTbScheduledPeriod.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "REMOTE")
                            ifCbRemote.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "PCODE")
                            ifTbPracticeCode.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "MEDIA")
                            ifCbMedia.Text = sFields[1].Trim();
                        else if (sFields[0].Trim() == "ENCOUNTER_GEN")
                            ifCbEncounterGen.Checked = Convert.ToBoolean(sFields[1].Trim());
                        else if (sFields[0].Trim() == "REFERRAL_GEN")
                            ifCbReferralGen.Checked = Convert.ToBoolean(sFields[1].Trim());

                        sLine = fIniFile.ReadLine();
                    }
                }
                fIniFile.Close();
            }
        }

        private void ifCbRemote_CheckedChanged(object sender, EventArgs e)
        {
            SetInterface();
        }

        #endregion

        private void SetInterface()
        {
            if (ifCbRemote.Checked)
            {
                ifTbPracticeCode.Enabled = true;
                ifCbMedia.Enabled = true;
            }
            else
            {
                ifTbPracticeCode.Enabled = false;
                ifCbMedia.Enabled = false;
            }
            if (ifCbScheduled.Checked)
            {
                ifDtScheduledStart.Enabled = true;
                ifDtScheduledEnd.Enabled = true;
                ifTbScheduledPeriod.Enabled = true;
                ifCbScheduledWeekMonth.Enabled = true;
            }
            else
            {
                ifDtScheduledStart.Enabled = false;
                ifDtScheduledEnd.Enabled = false;
                ifTbScheduledPeriod.Enabled = false;
                ifCbScheduledWeekMonth.Enabled = false;
            }
            if (ifCbFileSplit.Checked)
            {
                ifTbPracticePopulation.Enabled = true;
                ifTbExportSplit.Enabled = true;
                ifTbExportSplitLTC.Enabled = true;
                ifCbFileSplitWeeksMonths.Enabled = true;
                ifCbFileSplitWeeksMonthsLTC.Enabled = true;
                ifCbSystemType.Enabled = true;
            }
            else
            {
                ifTbPracticePopulation.Enabled = false;
                ifTbExportSplit.Enabled = false;
                ifTbExportSplitLTC.Enabled = false;
                ifCbFileSplitWeeksMonths.Enabled = false;
                ifCbFileSplitWeeksMonthsLTC.Enabled = false;
                ifCbSystemType.Enabled = false;
            }
        }
        
        private void ifBtSuggestSplits_Click(object sender, EventArgs e)
        {
            if ( ifTbPracticePopulation.Text.Trim() != "")
            {
                long pop = Convert.ToInt32(ifTbPracticePopulation.Text);

                if (ifCbSystemType.Text != "TPP SystmOne")
                {
                    MessageBox.Show("No file split necessary for " + ifCbSystemType.Text + " switching it off");
                    ifCbFileSplit.Checked = false;
                }
                else
                {
                    ifCbFileSplit.Checked = true;
                    if (pop > 15000)
                    {
                        ifCbFileSplitWeeksMonths.Text = "Month";
                        ifTbExportSplit.Text = "1";
                        ifCbFileSplitWeeksMonthsLTC.Text = "Month";
                        ifTbExportSplitLTC.Text = "1";
                    }
                    else if (pop > 10000)
                    {
                        ifCbFileSplitWeeksMonths.Text = "Month";
                        ifTbExportSplit.Text = "1";
                        ifCbFileSplitWeeksMonthsLTC.Text = "Month";
                        ifTbExportSplitLTC.Text = "2";
                    }
                    else if (pop > 8000)
                    {
                        ifCbFileSplitWeeksMonths.Text = "Month";
                        ifTbExportSplit.Text = "1";
                        ifCbFileSplitWeeksMonthsLTC.Text = "Month";
                        ifTbExportSplitLTC.Text = "6";
                    }
                    else
                    {
                        ifCbFileSplitWeeksMonths.Text = "Month";
                        ifTbExportSplit.Text = "3";
                        ifCbFileSplitWeeksMonthsLTC.Text = "Month";
                        ifTbExportSplitLTC.Text = "12";
                    }
                }
            }
        }
    }
}
