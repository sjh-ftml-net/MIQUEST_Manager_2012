using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using UTILS;

namespace MIQUEST_Response_Manager
{
    public class MQ_Journals
    {
        public bool ixSet = false;

        public int iNhsnumber = -1;
        public int iReference = -1;
        public int iDate = -1;
        public int iHcp = -1;
        public int iHcpType = -1;
        public int iRecordDate = -1;
        public int iCode = -1;
        public int iValue1 = -1;
        public int iValue2 = -1;
        public int iGMS = -1;
        public int iText = -1;

        // Optional
        public int iEndDate = -1;
        public int iTime = -1;
        public int iRubric = -1;
        public int iContext = -1;
        public int iCertainty = -1;
        public int iSeverity = -1;
        public int iLinks = -1;
        public int iEpisode = -1;

        public DateTime dtStart;
        public DateTime dtEnd;
        public int Length = 0;

        private ArrayList journals = new ArrayList();
        struct JournalEntry
        {
            public string Type;
            public string Content;
        }

        private Utils cUtils = new Utils();

        public void WriteV1File(GlobalData cGlobals)
        {

            string sFileName = cGlobals.sPracticeCode + "_Tribal_-_" + cGlobals.sExportType + "_Events_1.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);
            fOutFile.WriteLine(cGlobals.sV1HeaderLine);

            foreach (JournalEntry jEntry in journals)
            {
                string[] sFields = jEntry.Content.Split(',');

                string sOut;
                // Patient ID -- USING NHS NUMBER AS PATIENT ID
                sOut = cUtils.PrintFieldV1(iNhsnumber, sFields, "STRING");
                // Event Start Date
                sOut += "," + cUtils.PrintFieldV1(iDate, sFields, "DATE");
                // HCP
                sOut = sOut + "," + cUtils.PrintFieldV1(iHcp, sFields, "STRING");
                // HCP Type
                sOut += "," + cUtils.PrintFieldV1(iHcpType, sFields, "STRING");
                // Recorded Date (use event date)
                sOut += "," + cUtils.PrintFieldV1(iDate, sFields, "DATE");
                // Read Code
                sOut = sOut + "," + cUtils.PrintFieldV1(iCode, sFields, "STRING");
                // Value 1
                sOut += "," + cUtils.PrintFieldV1(iValue1, sFields, "STRING");
                // Value 2
                sOut += "," + cUtils.PrintFieldV1(iValue2, sFields, "STRING");
                // End Date (use event date)
                sOut += "," + cUtils.PrintFieldV1(iDate, sFields, "DATE");
                // Prescription Type - Empty
                sOut += ",\"\"" ;
                // Event Types
                if (jEntry.Type == "DRUGS")
                    sOut += ",\"P\"";
                else
                {
                    string sCode = cUtils.CleanString(sFields[iCode]);
                    if (sCode == "Ub0oo")
                        sOut += ",\"S\"";
                    else if (sCode == "22K..")
                        sOut += ",\"M\"";
                    else if (sCode == "X773t")
                        sOut += ",\"B\"";
                    else
                        sOut += ",\"C\"";
                }


                fOutFile.WriteLine(sOut);
            }

            fOutFile.Close();

        }

        public void Assess()
        {
            string[] sFields;
            string sDate;
            DateTime dDate;

            dtEnd = Convert.ToDateTime("1900-01-01");
            dtStart = DateTime.Today;

            foreach (JournalEntry jEntry in journals)
            {
                sFields = jEntry.Content.Split(',');
                sDate = sFields[iDate];
                sDate = cUtils.CleanDate(sDate);
                if (DateTime.TryParse(sDate, out dDate))
                {
                    if (dDate < dtStart)
                        dtStart = dDate;
                    if (dDate > dtEnd)
                        dtEnd = dDate;
                }
            }
        }

        public string Describe()
        {
            string s;

            s = "Records " + Length.ToString();
            s = s + " Start " + dtStart.ToShortDateString();
            s = s + " End " + dtEnd.ToShortDateString();

            string sUnits = "Years";
            int iDiff = dtEnd.Year - dtStart.Year;
            if (iDiff < 1)
            {
                iDiff = (dtEnd - dtStart).Days;
                sUnits = "Days";
            }
            else if (iDiff == 1)
                sUnits = "Year";

            s = s + " Period " + iDiff.ToString() + " " + sUnits;
            return s;
        }

        public void SetIX(string sLine)
        {
            if (!ixSet)
            {
                ixSet = true;
                string[] sFields = sLine.Split(',');

                for (int i = 0; i < sFields.Length; i++)
                {
                    string col = cUtils.CleanString(sFields[i]);
                    switch (col)
                    {
                        case "NHS_NUMBER": iNhsnumber = i; break;
                        case "REFERENCE": iReference = i; break;
                        case "DATE": iDate = i; break;
                        case "HCP": iHcp = i; break;
                        case "HCP_TYPE": iHcpType = i; break;
                        case "RECORD_DATE": iRecordDate = i; break;
                        case "CODE": iCode = i; break;
                        case "VALUE1": iValue1 = i; break;
                        case "VALUE2": iValue2 = i; break;
                        case "GMS": iGMS = i; break;
                        case "TEXT": iText = i; break;
                        case "END_DATE": iEndDate = i; break;
                        case "TIME": iTime = i; break;
                        case "RUBRIC": iRubric = i; break;
                        case "CONTEXT": iContext = i; break;
                        case "CERTAINTY": iCertainty = i; break;
                        case "SEVERITY": iSeverity = i; break;
                        case "LINKS": iLinks = i; break;
                        case "EPISODE": iEpisode = i; break;
                    }
                }
            }
        }

        public void Add(string sLine, string CodeType)
        {
            JournalEntry je = new JournalEntry();
            je.Type = CodeType;
            je.Content = sLine;
            journals.Add(je);
            Length++;
        }

        public void Clear()
        {
            ixSet = false;
            Length = 0;
            journals.Clear();
        }
    }
}
