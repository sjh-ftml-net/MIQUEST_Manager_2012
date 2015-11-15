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

        private ArrayList JournalData = new ArrayList();
        public int DataRows = 0;
        public ArrayList Columns = new ArrayList();
        public int ColumnCount = 0;

        struct JournalEntry
        {
            public string Type;
            public string Content;
        }

        public DateTime dtStart;
        public DateTime dtEnd;
        private Utils cUtils = new Utils();

        public bool Save(GlobalData cGlobals)
        {
            // Can't use the Save in utils because of the daft Apollo V1 structure we need

            bool bRet = true;

            string sFileName = cGlobals.sPracticeCode + "_" + String.Format("{0:yyyyMMdd}", cGlobals.dtQueryDate) + "_JOURNAL.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);

            fOutFile.WriteLine(cUtils.PrintStringArray(Columns));

            foreach (JournalEntry je in JournalData)
            {
                string[] sFields = je.Content.Split(',');

                if (sFields.Length != Columns.Count)
                {
                    bRet = false;
                }

                string sRow = "";
                foreach (string sAttributeName in Columns)
                {
                    int pos = Columns.IndexOf(sAttributeName);
                    if (pos != -1)
                    {
                        string sData;
                        if (sAttributeName.IndexOf("DATE") != -1)
                            sData = cUtils.CleanField(pos, sFields, "DATE");
                        else
                            sData = cUtils.CleanField(pos, sFields, "STRING");

                        if (cGlobals.bPseudonymise)
                        {
                            sData = cUtils.Pseudomymise(sData, sAttributeName, cGlobals.iPseudoDateShift);
                        }

                        sData = cUtils.Quote(sData);

                        if (sRow == "")
                            sRow = sData;
                        else
                            sRow += "," + sData;
                    }
                }
                fOutFile.WriteLine(sRow);
            }

            fOutFile.Close();

            return bRet;
        }

        public void WriteV1File(GlobalData cGlobals)
        {

            string sFileName = cGlobals.sPracticeCode + "_Tribal_-_" + cGlobals.sExportType + "_Events_1.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);
            fOutFile.WriteLine(cGlobals.sV1HeaderLine);

            foreach (JournalEntry jEntry in JournalData)
            {
                string[] sFields = jEntry.Content.Split(',');

                string sOut;
                // Patient ID -- USING NHS NUMBER AS PATIENT ID
                sOut = cUtils.PrintFieldV1(Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
                // Event Start Date
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("DATE"), sFields, "DATE");
                // HCP
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("HCP"), sFields, "STRING");
                // HCP Type
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("HCP_TYPE"), sFields, "STRING");
                // Recorded Date (use event date)
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("DATE"), sFields, "DATE");
                // Read Code
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("CODE"), sFields, "STRING");
                // Value 1
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("VALUE1"), sFields, "STRING");
                // Value 2
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("VALUE2"), sFields, "STRING");
                // End Date (use event date)
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("DATE"), sFields, "DATE");
                // Prescription Type - Empty
                sOut += ",\"\"" ;
                // Event Types
                if (jEntry.Type == "DRUGS")
                    sOut += ",\"P\"";
                else
                {
                    string sCode = cUtils.CleanString(sFields[Columns.IndexOf("CODE")]);
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

            foreach (JournalEntry jEntry in JournalData)
            {
                sFields = jEntry.Content.Split(',');
                sDate = sFields[Columns.IndexOf("DATE")];
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

            s = "Records " + DataRows.ToString();
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

        public void SetIXFromHeadLine(string sLine)
        {
            if (!ixSet)
            {
                ixSet = true;
                ColumnCount = 0;
                string[] sFields = sLine.Split(',');

                for (int i = 0; i < sFields.Length; i++)
                {
                    string col = cUtils.CleanString(sFields[i]);
                    Columns.Add(col);
                }
            }
        }

        public void Add(string sLine, string CodeType)
        {
            JournalEntry je = new JournalEntry();
            je.Type = CodeType;
            je.Content = sLine;
            JournalData.Add(je);
            DataRows++;
        }

        public void Clear()
        {
            ixSet = false;
            DataRows = 0;
            JournalData.Clear();    
        }
    }
}
