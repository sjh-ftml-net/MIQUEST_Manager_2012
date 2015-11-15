using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UTILS;

namespace MIQUEST_Response_Manager
{
    class Parser
    {
        HomeForm homeForm;
        Utils cUtils = new Utils();
        string FileCodeType;

        public Parser(HomeForm hf)
        {
            homeForm = hf;
        }

        // ----------------------------------------------------------------------------------------------------
        // Process the response files (step through each)
        // ----------------------------------------------------------------------------------------------------
        public bool ProcessResponseFiles(string sPath)
        {
            bool bFatal = false;

            DirectoryInfo di = new DirectoryInfo(sPath);
            FileInfo[] rgFiles = di.GetFiles("*.CSV", SearchOption.TopDirectoryOnly);
            homeForm.MQ_ProgressBar.Maximum = rgFiles.Length * 2;

            if (rgFiles.Length == 0)
            {
                homeForm.FatalError = true;
                homeForm.FatalErrorMessage = "No response files found";
            }
            else
            {
                foreach (FileInfo fi in rgFiles)
                {
                    ProcessResponseFile(fi.FullName);
                    homeForm.MQ_ProgressBar.Value = homeForm.MQ_ProgressBar.Value + 1;
                }
            }

            return bFatal;
        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response file
        // ----------------------------------------------------------------------------------------------------
        private void ProcessResponseFile(string sFilePathName)
        {
            bool bReportFile = true;
            int iRecords = 0;
            string sDataCat = "";

            StreamReader fResponseFile = new StreamReader(sFilePathName);
            string sLine = fResponseFile.ReadLine().Trim();
            while (sLine != null && !homeForm.FatalError)
            {

                if (sLine != "" && !sLine.StartsWith("#") && bReportFile)
                {

                    if (sLine.StartsWith("*"))
                        HeaderLine(sLine);
                    else if (sLine.StartsWith("FROM"))
                    {
                        sDataCat = ResponseDataCategory(sLine);
                    }
                    else if (sLine.StartsWith("&0"))
                        ResponseResultLine(sLine, out bReportFile);
                    else if (sLine.StartsWith("&"))
                        ResponseHeadLine(sLine, sDataCat);
                    else if (sLine.StartsWith("$"))
                    {
                        ResponseDataSet(sLine, sDataCat, FileCodeType);
                        iRecords++;
                    }
                }
                sLine = fResponseFile.ReadLine();
            }

            if (!bReportFile)
                cUtils.LogMessage(homeForm.fLogFile, Path.GetFileName(sFilePathName) + ": Subset or aggregate");
            else
                cUtils.LogMessage(homeForm.fLogFile, Path.GetFileName(sFilePathName) + ": " + sDataCat + " " + iRecords);

            if (homeForm.FatalError)
            {
                homeForm.FatalErrorMessage = homeForm.FatalErrorMessage + Path.GetFileName(sFilePathName);
                cUtils.LogMessage(homeForm.fLogFile, Path.GetFileName(sFilePathName) + ": " + homeForm.FatalErrorMessage);
            }

        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response header line (starts &n)
        // ----------------------------------------------------------------------------------------------------
        private void HeaderLine(string sLine)
        {

            string[] sFields = sLine.Split(',');
            string sKey = sFields[0].Trim();

            if (sFields.Length < 2)
            {
                homeForm.FatalErrorMessage = "Malformed header line " + sLine + " in ";
                homeForm.FatalError = true;
            }
            else if (sKey == "*RSP_IDENT")
            {
                if (homeForm.cGlobals.sPracticeCode == "")
                {
                    homeForm.cGlobals.sPracticeCode = cUtils.CleanString(sFields[1]);
                }
                else if (sFields[1].Trim() != homeForm.cGlobals.sPracticeCode)
                {
                    homeForm.FatalErrorMessage = "Inconsistent practice code in ";
                    homeForm.FatalError = true;
                }
            }
            else if (sKey == "*RSP_RDATE")
            {
                DateTime.TryParse(cUtils.CleanDate(sFields[1]), out homeForm.cGlobals.dtQueryDate);
            }
            else if (sKey == "*QRY_TITLE")
            {
                if (sFields.Length == 3)
                {
                    if (sFields[2].IndexOf("DRUGS") > -1)
                        FileCodeType = "DRUGS";
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response result line (starts &0)
        // ----------------------------------------------------------------------------------------------------
        private void ResponseResultLine(string sLine, out bool bReportFile)
        {
            bReportFile = true;

            string[] sFields = sLine.Split(',');

            string s = cUtils.CleanString(sFields[1]);

            if (s == "ERROR" || s == "REJECTED")
            {
                homeForm.FatalErrorMessage = "Query error or rejected in ";
                homeForm.FatalError = true;
            }
            else if (s != "REPORT")
            {
                bReportFile = false;
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response result line (starts &1 &2 etc)
        // ----------------------------------------------------------------------------------------------------
        private void ResponseHeadLine(string sLine, string sDataCategory)
        {
            if (sDataCategory == "ENCOUNTERS")
                homeForm.cEncounters.SetIXFromHeadLine(sLine);
            else if (sDataCategory == "REFERRALS")
                homeForm.cReferrals.SetIXFromHeadLine(sLine);
            else if (sDataCategory == "PATIENTS")
                homeForm.cPatients.SetIXFromHeadLine(sLine);
            else if (sDataCategory == "JOURNALS")
                homeForm.cJournals.SetIXFromHeadLine(sLine);
        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response data set line (starts $)
        // ----------------------------------------------------------------------------------------------------
        private string ResponseDataCategory(string sLine)
        {
            string[] sFields = sLine.Split();

            string s = cUtils.CleanString(sFields[1]);

            if (s != "PATIENTS" && s != "ENCOUNTERS" && s != "REFERRALS" && s != "JOURNALS")
            {
                homeForm.FatalError = true;
                homeForm.FatalErrorMessage = "Invalid Data Category Encountered";
            }

            return s;
        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response data set line (starts $)
        // ----------------------------------------------------------------------------------------------------
        private void ResponseDataSet(string sLine, string sDataCategory, string sCodeType)
        {
            if (sDataCategory == "ENCOUNTERS")
                homeForm.cEncounters.Add(sLine);
            else if (sDataCategory == "REFERRALS")
                homeForm.cReferrals.Add(sLine);
            else if (sDataCategory == "PATIENTS")
                homeForm.cPatients.Add(sLine);
            else if (sDataCategory == "JOURNALS")
                homeForm.cJournals.Add(sLine, sCodeType);
        }
    }
}
