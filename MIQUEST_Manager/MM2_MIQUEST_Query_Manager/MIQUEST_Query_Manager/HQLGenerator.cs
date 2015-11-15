using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MIQUEST_Query_Manager
{
    class HQLGenerator
    {
        private   HomeForm homeForm;

        public bool FatalError = false;
        public string ErrorMessage = "";

        public    HQLGenerator(HomeForm homeForm)
        {
            this.homeForm = homeForm;
        }

        public void Generate()
        {

            HQLHeader cHQLHeader = new HQLHeader();

            // Pass some configuration items to the Header
            cHQLHeader.SetID = homeForm.sSetID;
            cHQLHeader.SetDescription = homeForm.sSetDescription;

            cHQLHeader.ScheduledQuery = homeForm.ifCbScheduled.Checked;
            cHQLHeader.ScheduledStart = homeForm.ifDtScheduledStart.Value;
            cHQLHeader.ScheduledEnd = homeForm.ifDtScheduledEnd.Value;
            cHQLHeader.ScheduledPeriod = Convert.ToInt16(homeForm.ifTbScheduledPeriod.Text);
            cHQLHeader.ScheduledWeekMonth = homeForm.ifCbScheduledWeekMonth.Text;

            cHQLHeader.RemoteItems = homeForm.ifCbRemote.Checked;
            cHQLHeader.PracticeCode = homeForm.ifTbPracticeCode.Text;
            cHQLHeader.Media = homeForm.ifCbMedia.Text;

            cHQLHeader.SetCodeSet(homeForm.ifCbSystemType.Text);

            homeForm.fLogFile.WriteLine("Creating HQL Files");

            GenerateCohort(ref cHQLHeader);

            GenerateJournal(ref cHQLHeader, homeForm.cCohorts.GlobalCohort);

            if (homeForm.ifCbEncounterGen.Checked)
            {
                GenerateEncounters(ref cHQLHeader, homeForm.cCohorts.GlobalCohort);
            }

            if (homeForm.ifCbReferralGen.Checked)
            {
                GenerateReferrals(ref cHQLHeader, homeForm.cCohorts.GlobalCohort);
            }
            //homeForm.fLogFile.WriteLine("Querynames: " + cHQLHeader.ListQueries());
            cHQLHeader.Clear();
        }

        public void GenerateCohort(ref HQLHeader cHQLHeader)
        {
            string sFileName;

            homeForm.fLogFile.WriteLine("Creating COHORT SUBSET Files");

            foreach (HQLCohort.HQLCohortItem c in homeForm.cCohorts)
            {

                homeForm.fLogFile.WriteLine("  " + cHQLHeader.Order.ToString("D3") + "_" + c.Name + ".HQL");

                string sFilename = homeForm.sOutPath + cHQLHeader.Order.ToString("D3") + "_" + c.Name + ".HQL";
                StreamWriter fOut = new StreamWriter(sFilename);
                // Prepare Header
                string sHeader = cHQLHeader.Generate(c.Name, c.Description, "Cohort Subset");
                if (cHQLHeader.FatalError)
                {
                    FatalError = true;
                    ErrorMessage = cHQLHeader.ErrorMessage;
                    homeForm.fLogFile.WriteLine("ERROR: " + ErrorMessage);
                }
                else
                {
                    fOut.WriteLine(sHeader);
                    fOut.WriteLine(c.HQLText);
                }
                fOut.Close();
            }

            // --- Create the report queries for mapped cohorts
            homeForm.fLogFile.WriteLine("Creating COHORT REPORT Files");
            foreach (HQLCohort.HQLCohortItem c in homeForm.cCohorts)
            {
                if (c.Map != "")
                {

                    // Generate Subset File

                    sFileName = cHQLHeader.Order.ToString("D3") + "_" + c.Name + GlobalDefs.cSubsetExt + ".HQL";
                    homeForm.fLogFile.WriteLine("  " + sFileName);

                    StreamWriter fOut = new StreamWriter(homeForm.sOutPath + sFileName);
                    // Prepare Header
                    string sHeader = cHQLHeader.Generate(c.Name + GlobalDefs.cSubsetExt, c.Description, "Cohort Report Subset");
                    if (cHQLHeader.FatalError)
                    {
                        FatalError = true;
                        ErrorMessage = cHQLHeader.ErrorMessage;
                    }
                    else
                    {
                        fOut.WriteLine(sHeader);
                        fOut.WriteLine(c.CreateReportSubset());
                    }
                    fOut.Close();

                    // Generate Report File
                    sFileName = cHQLHeader.Order.ToString("D3") + "_" + c.Name + GlobalDefs.cReportExt + ".HQL";

                    homeForm.fLogFile.WriteLine("  " + sFileName);

                    fOut = new StreamWriter(homeForm.sOutPath + sFileName);
                    // Prepare Header
                    sHeader = cHQLHeader.Generate(c.Name + GlobalDefs.cReportExt, c.Description, "Cohort Report Print");
                    if (cHQLHeader.FatalError)
                    {
                        FatalError = true;
                        ErrorMessage = cHQLHeader.ErrorMessage;
                    }
                    else
                    {
                        fOut.WriteLine(sHeader);
                        fOut.WriteLine(c.CreateReportPrint());
                    }
                    fOut.Close();
                }
            }
        }

        public DateTime SetNewEnd(string sPeriod, DateTime Start, int Period)
        {
            DateTime dtRes;
            if (sPeriod == "LTC")
            {
                if (homeForm.ifCbFileSplitWeeksMonthsLTC.Text == "Week")
                    dtRes = Start.AddDays(Period);
                else
                    dtRes = Start.AddMonths(Period);
            }
            else
            {
                if (homeForm.ifCbFileSplitWeeksMonths.Text == "Week")
                    dtRes = Start.AddDays(Period);
                else
                    dtRes = Start.AddMonths(Period);
            }
            return dtRes;
        }

        public void SetDates(string sPeriod, out int iPeriod, out DateTime dtRStart, out DateTime dtREnd, out DateTime dtQueryEnd)
        {

            if (homeForm.ifCbFileSplit.Checked)
            {
                if (sPeriod == "LTC")
                {
                    iPeriod = Convert.ToInt16(homeForm.ifTbExportSplitLTC.Text);
                    dtRStart = homeForm.dtQueryLTCStartDate;
                    dtQueryEnd = homeForm.dtQueryLTCEndDate;
                    if (homeForm.ifCbFileSplitWeeksMonthsLTC.Text == "Week")
                        dtREnd = dtRStart.AddDays(iPeriod);
                    else
                        dtREnd = dtRStart.AddMonths(iPeriod);
                }
                else
                {
                    iPeriod = Convert.ToInt16(homeForm.ifTbExportSplit.Text);
                    dtRStart = homeForm.dtQueryStartDate;
                    dtQueryEnd = homeForm.dtQueryEndDate;
                    if (homeForm.ifCbFileSplitWeeksMonths.Text == "Week")
                        dtREnd = dtRStart.AddDays(iPeriod);
                    else
                        dtREnd = dtRStart.AddMonths(iPeriod);
                }
            }
            else
            {
                if (sPeriod == "LTC")
                {
                    iPeriod = Convert.ToInt16(homeForm.ifTbExportSplitLTC.Text);
                    dtRStart = homeForm.dtQueryLTCStartDate;
                    dtREnd = homeForm.dtQueryLTCEndDate;
                    dtQueryEnd = homeForm.dtQueryLTCEndDate;
                }
                else
                {
                    iPeriod = Convert.ToInt16(homeForm.ifTbExportSplit.Text);
                    dtRStart = homeForm.dtQueryStartDate;
                    dtREnd = homeForm.dtQueryEndDate;
                    dtQueryEnd = homeForm.dtQueryEndDate;
                }
            }
        }

        public void GenerateJournal(ref HQLHeader cHQLHeader, string Subset)
        {
            string sFileName;
            string sLocalSubset;

            homeForm.fLogFile.WriteLine("Creating JOURNAL Files");
            foreach (HQLJournal.HQLJournalSetItem JournalSet in homeForm.cJournal)
            {

                // ---- Create the SUBSET file ----
                sLocalSubset = JournalSet.Name + "00" + GlobalDefs.cSubsetExt;
                sFileName = cHQLHeader.Order.ToString("D3") + "_" + sLocalSubset + ".HQL";
                homeForm.fLogFile.WriteLine("* Journal Set: " + JournalSet.Period + ", " + sLocalSubset + ", File: " + sFileName);

                StreamWriter fOut = new StreamWriter(homeForm.sOutPath + sFileName);
                // Create local subset ID

                // Prepare Header
                string sComment = JournalSet.CodeType + "-" + JournalSet.Period + "-" + JournalSet.SetType;
                string sHeader = cHQLHeader.Generate(sLocalSubset, sComment, "Journal Subset Query " + sComment);
                if (cHQLHeader.FatalError)
                {
                    FatalError = true;
                    ErrorMessage = cHQLHeader.ErrorMessage;
                }
                else
                {
                    fOut.WriteLine(sHeader);
                    fOut.WriteLine(JournalSet.CreateReportSubset(Subset, JournalSet.Period));
                }
                fOut.Close();

                // ---- Create the REPORT file(s) ----

                DateTime dtRStart;
                DateTime dtREnd;
                DateTime dtQueryEnd;
                int iPeriod;
                int iCount = 1;

                // Calculate the initial Dates
                SetDates(JournalSet.Period, out iPeriod, out dtRStart, out dtREnd, out dtQueryEnd);

                while (dtRStart < dtQueryEnd)
                {
                    if (iCount > 1)
                        dtRStart = dtRStart.AddDays(1);
                    if (dtREnd > dtQueryEnd)
                        dtREnd = dtQueryEnd;

                    string sQueryName = JournalSet.Name + iCount.ToString("D2") + GlobalDefs.cReportExt;
                    sFileName = cHQLHeader.Order.ToString("D3") + "_" + sQueryName + ".HQL";
                    homeForm.fLogFile.WriteLine("  " + sFileName + ": " + dtRStart.ToShortDateString() + " to " + dtREnd.ToShortDateString());

                    // Write the file
                    fOut = new StreamWriter(homeForm.sOutPath + sFileName);
                    // Prepare Header
                    sHeader = cHQLHeader.Generate(sQueryName, sComment, "Journal Report Query " + sComment);
                    if (cHQLHeader.FatalError)
                    {
                        FatalError = true;
                        ErrorMessage = cHQLHeader.ErrorMessage;
                    }
                    else
                    {
                        string sMsg;
                        fOut.WriteLine(sHeader);
                        fOut.WriteLine(JournalSet.CreateReportPrint(dtRStart, dtREnd, out sMsg));
                        if (iCount == 1)
                            homeForm.fLogFile.WriteLine("    " + sMsg);
                    }
                    fOut.Close();

                    dtRStart = dtREnd;
                    dtREnd = SetNewEnd(JournalSet.Period, dtRStart, iPeriod);
                    iCount++;
                }
            }
        }

        public void GenerateEncounters(ref HQLHeader cHQLHeader, string Subset)
        {
            string sFileName;
            string sLocalSubset;
            HQLEncounters cEncounters = new HQLEncounters();

            string sQname = HQLEncounters.Name;

            homeForm.fLogFile.WriteLine("Creating ENCOUNTER Files");

            // ---- Create the SUBSET file ----
            sLocalSubset = sQname + "00" + GlobalDefs.cSubsetExt;
            sFileName = cHQLHeader.Order.ToString("D3") + "_" + sLocalSubset + ".HQL";
            homeForm.fLogFile.WriteLine("* Encounter Set: " + sLocalSubset + ", File: " + sFileName);

            StreamWriter fOut = new StreamWriter(homeForm.sOutPath + sFileName);
            // Create local subset ID

            // Prepare Header
            string sHeader = cHQLHeader.Generate(sLocalSubset, "Encounters", "Encounter Subset Query");
            if (cHQLHeader.FatalError)
            {
                FatalError = true;
                ErrorMessage = cHQLHeader.ErrorMessage;
            }
            else
            {
                fOut.WriteLine(sHeader);
                fOut.WriteLine(cEncounters.CreateReportSubset(Subset));
            }
            fOut.Close();

            // ---- Create the REPORT file(s) ----

            DateTime dtRStart;
            DateTime dtREnd;
            DateTime dtQueryEnd;
            int iPeriod;
            int iCount = 1;

            // Calculate the initial Dates
            SetDates("CURRENT", out iPeriod, out dtRStart, out dtREnd, out dtQueryEnd);

            while (dtRStart < dtQueryEnd)
            {
                if (iCount > 1)
                    dtRStart = dtRStart.AddDays(1);
                if (dtREnd > dtQueryEnd)
                    dtREnd = dtQueryEnd;

                string sQueryName = sQname + iCount.ToString("D2") + GlobalDefs.cReportExt;
                sFileName = cHQLHeader.Order.ToString("D3") + "_" + sQueryName + ".HQL";
                homeForm.fLogFile.WriteLine("  " + sFileName + ": " + dtRStart.ToShortDateString() + " to " + dtREnd.ToShortDateString());

                // Write the file
                fOut = new StreamWriter(homeForm.sOutPath + sFileName);
                // Prepare Header
                sHeader = cHQLHeader.Generate(sQueryName, "Encounters", "Encounter Report Query ");
                if (cHQLHeader.FatalError)
                {
                    FatalError = true;
                    ErrorMessage = cHQLHeader.ErrorMessage;
                }
                else
                {
                    fOut.WriteLine(sHeader);
                    fOut.WriteLine(cEncounters.CreateReportPrint(dtRStart, dtREnd));
                }
                fOut.Close();

                dtRStart = dtREnd;
                dtREnd = SetNewEnd("CURRENT", dtRStart, iPeriod);
                iCount++;
            }
        }

        public void GenerateReferrals(ref HQLHeader cHQLHeader, string Subset)
        {
            string sFileName;
            string sLocalSubset;
            HQLReferrals cReferrals = new HQLReferrals();

            string sQname = HQLReferrals.Name;

            homeForm.fLogFile.WriteLine("Creating REFERRAL Files");

            // ---- Create the SUBSET file ----
            sLocalSubset = sQname + "00" + GlobalDefs.cSubsetExt;
            sFileName = cHQLHeader.Order.ToString("D3") + "_" + sLocalSubset + ".HQL";
            homeForm.fLogFile.WriteLine("* Referral Set: " + sLocalSubset + ", File: " + sFileName);

            StreamWriter fOut = new StreamWriter(homeForm.sOutPath + sFileName);
            // Create local subset ID

            // Prepare Header
            string sHeader = cHQLHeader.Generate(sLocalSubset, "Referrals", "Referral Subset Query");
            if (cHQLHeader.FatalError)
            {
                FatalError = true;
                ErrorMessage = cHQLHeader.ErrorMessage;
            }
            else
            {
                fOut.WriteLine(sHeader);
                fOut.WriteLine(cReferrals.CreateReportSubset(Subset));
            }
            fOut.Close();

            // ---- Create the REPORT file(s) ----

            DateTime dtRStart;
            DateTime dtREnd;
            DateTime dtQueryEnd;
            int iPeriod;
            int iCount = 1;

            // Calculate the initial Dates
            SetDates("CURRENT", out iPeriod, out dtRStart, out dtREnd, out dtQueryEnd);

            while (dtRStart < dtQueryEnd)
            {
                if (iCount > 1)
                    dtRStart = dtRStart.AddDays(1);
                if (dtREnd > dtQueryEnd)
                    dtREnd = dtQueryEnd;

                string sQueryName = sQname + iCount.ToString("D2") + GlobalDefs.cReportExt;
                sFileName = cHQLHeader.Order.ToString("D3") + "_" + sQueryName + ".HQL";
                homeForm.fLogFile.WriteLine("  " + sFileName + ": " + dtRStart.ToShortDateString() + " to " + dtREnd.ToShortDateString());

                // Write the file
                fOut = new StreamWriter(homeForm.sOutPath + sFileName);
                // Prepare Header
                sHeader = cHQLHeader.Generate(sQueryName, "Referrals", "Referral Report Query ");
                if (cHQLHeader.FatalError)
                {
                    FatalError = true;
                    ErrorMessage = cHQLHeader.ErrorMessage;
                }
                else
                {
                    fOut.WriteLine(sHeader);
                    fOut.WriteLine(cReferrals.CreateReportPrint(dtRStart, dtREnd));
                }
                fOut.Close();

                dtRStart = dtREnd;
                dtREnd = SetNewEnd("CURRENT", dtRStart, iPeriod);
                iCount++;
            }
        }

    }
}
