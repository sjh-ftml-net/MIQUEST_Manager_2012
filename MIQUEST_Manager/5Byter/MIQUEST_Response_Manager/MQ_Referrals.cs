using System;
using System.Collections;
using System.Linq;
using System.Text;
using UTILS;

namespace MIQUEST_Response_Manager
{
    public class MQ_Referrals
    {
        public bool ixSet = false;

        public int iNhsnumber = -1;
        public int iReference = -1;
        public int iDate = -1;
        public int iHcp = -1;
        public int iHcp_type = -1;
        public int iSpeciality = -1;
        public int iUnit = -1;
        public int iType = -1;

        // Optional
        public int iContractor = -1;
        public int iContract = -1;
        public int iActionDate = -1;
        public int iLinks = -1;

        public DateTime dtStart;
        public DateTime dtEnd;
        public int Length = 0;

        private ArrayList referrals = new ArrayList();

        private Utils cUtils = new Utils();

        public void Assess()
        {
            string[] sFields;
            string sDate;
            DateTime dDate;

            dtEnd = Convert.ToDateTime("1900-01-01");
            dtStart = DateTime.Today;

            foreach (string s in referrals)
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
                        case "SPECIALITY": iSpeciality = i; break;
                        case "UNIT": iUnit = i; break;
                        case "TYPE": iType = i; break;
                        case "CONTRACTOR": iContractor = i; break;
                        case "CONTRACT": iContract = i; break;
                        case "LINKS": iLinks = i; break;
                    }
                }
            }
        }

        public void Add(string sLine)
        {
            referrals.Add(sLine);
            Length++;
        }

        public void Clear()
        {
            ixSet = false;
            Length = 0;
            referrals.Clear();
        }
    }
}
