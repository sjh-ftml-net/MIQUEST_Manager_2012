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

        private ArrayList ReferralData = new ArrayList();
        public int DataRows = 0;
        public ArrayList Columns = new ArrayList();
        public int ColumnCount = 0;

        public DateTime dtStart;
        public DateTime dtEnd;
        private Utils cUtils = new Utils();

        public bool Save(GlobalData cGlobals)
        {
            string sFileName = cGlobals.sOutPath + cGlobals.sPracticeCode + "_" + String.Format("{0:yyyyMMdd}", cGlobals.dtQueryDate) + "_REFERRAL.csv";
            return cUtils.SaveFileFromArrayList(sFileName, ReferralData, Columns, cGlobals.bPseudonymise, cGlobals.iPseudoDateShift);
        }

        public void Assess()
        {
            string[] sFields;
            string sDate;
            DateTime dDate;

            dtEnd = Convert.ToDateTime("1900-01-01");
            dtStart = DateTime.Today;

            foreach (string s in ReferralData)
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
            ReferralData.Add(sLine);
            DataRows++;
        }

        public void Clear()
        {
            ixSet = false;
            DataRows = 0;
            ReferralData.Clear();
        }
    }
}
