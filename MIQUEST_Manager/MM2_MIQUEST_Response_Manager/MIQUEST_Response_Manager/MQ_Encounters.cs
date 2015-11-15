using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using UTILS;

namespace MIQUEST_Response_Manager
{
    public class MQ_Encounters
    {

        public bool ixSet = false;

        private ArrayList EncounterData = new ArrayList();
        public int DataRows = 0;
        public ArrayList Columns = new ArrayList();
        public int ColumnCount = 0;

        public DateTime dtStart;
        public DateTime dtEnd;
        private Utils cUtils = new Utils();

        public void WriteV1File(GlobalData cGlobals)
        {

            string sFileName = cGlobals.sPracticeCode + "_Tribal_-_" + cGlobals.sExportType + "_Encounters_1.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);
            fOutFile.WriteLine(cGlobals.sV1HeaderLine);

            foreach (string sP in EncounterData)
            {
                string[] sFields = sP.Split(',');

                string sOut;

                // Patient ID (use NHS Number)
                sOut = cUtils.PrintFieldV1(Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
                // Encounter Date
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("DATE"), sFields, "DATE");
                // HCP
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("HCP"), sFields, "STRING");
                // HCP Type
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("HCP_TYPE"), sFields, "HCPTYPE");
                // Session
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("SESSION"), sFields, "STRING");
                // Location
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("LOCATION"), sFields, "STRING");

                fOutFile.WriteLine(sOut);
            }

            fOutFile.Close();

        }

        public bool Save(GlobalData cGlobals)
        {
            string sFileName = cGlobals.sOutPath + cGlobals.sPracticeCode + "_" + String.Format("{0:yyyyMMdd}", cGlobals.dtQueryDate) + "_ENCOUNTERS.csv";
            return cUtils.SaveFileFromArrayList(sFileName, EncounterData, Columns, cGlobals.bPseudonymise, cGlobals.iPseudoDateShift);
        }

        public void Assess()
        {
            string[] sFields;
            string sDate;
            DateTime dDate;

            dtEnd = Convert.ToDateTime("1900-01-01");
            dtStart = DateTime.Today;

            foreach (string s in EncounterData)
            {
                sFields = s.Split(',');
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

        public void Add(string sLine)
        {
            EncounterData.Add(sLine);
            DataRows++;
        }

        public void Clear()
        {
            ixSet = false;
            DataRows = 0;
            EncounterData.Clear();
        }
    }
}
