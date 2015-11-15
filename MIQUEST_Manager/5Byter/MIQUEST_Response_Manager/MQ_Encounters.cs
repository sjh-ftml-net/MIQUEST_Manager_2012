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

        public int iNhsnumber = -1;
        public int iReference = -1;
        public int iDate = -1;
        public int iHcp = -1;
        public int iHcp_type = -1;
        public int iSession = -1;
        public int iLocation = -1;

        // Optional
        public int iTime = -1;
        public int iDuration = -1;
        public int iTravel = -1;
        public int iLinks = -1;

        public DateTime dtStart;
        public DateTime dtEnd;
        public int Length = 0;

        private ArrayList encounters = new ArrayList();

        private Utils cUtils = new Utils();

        public void WriteV1File(GlobalData cGlobals)
        {

            string sFileName = cGlobals.sPracticeCode + "_Tribal_-_" + cGlobals.sExportType + "_Encounters_1.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);
            fOutFile.WriteLine(cGlobals.sV1HeaderLine);

            foreach (string sP in encounters)
            {
                string[] sFields = sP.Split(',');

                string sOut;
                // Patient ID (use NHS Number)
                sOut = cUtils.PrintFieldV1(iNhsnumber, sFields, "STRING");
                // Encounter Date
                sOut += "," + cUtils.PrintFieldV1(iDate, sFields, "DATE");
                // HCP
                sOut += "," + cUtils.PrintFieldV1(iHcp, sFields, "STRING");
                // HCP Type
                sOut += "," + cUtils.PrintFieldV1(iHcp_type, sFields, "HCPTYPE");
                // Session
                sOut += "," + cUtils.PrintFieldV1(iSession, sFields, "STRING");
                // Location
                sOut += "," + cUtils.PrintFieldV1(iLocation, sFields, "STRING");

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

            foreach (string s in encounters)
            {
                sFields = s.Split(',');
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
                        case "HCP_TYPE": iHcp_type = i; break;
                        case "SESSION": iHcp_type = i; break;
                        case "LOCATION": iLocation = i; break;
                        case "TIME": iTime = i; break;
                        case "DURATION": iDuration = i; break;
                        case "TRAVEL": iTravel = i; break;
                        case "LINKS": iLinks = i; break;
                    }
                }
            }
        }

        public void Add(string sLine)
        {
            encounters.Add(sLine);
            Length++;
        }

        public void Clear()
        {
            ixSet = false;
            Length = 0;
            encounters.Clear();
        }
    }
}
