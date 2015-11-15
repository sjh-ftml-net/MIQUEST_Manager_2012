using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIQUEST_Response_Manager
{
    public class GlobalData
    {

        public string sInPath;
        public string sOutPath;

        public bool bV1Export;
        public bool bV1Bulk;
        public bool bV1Initial;

        public string sPracticeCode = "";
        public DateTime dtQueryDate;

        public string sExportType = "";
        public string sV1HeaderLine = "";

        public bool bPseudonymise;
        public bool bPseudoDateShift;
        public int iPseudoDateShift = -5;

        public void CreateV1Header()
        {

            if (bV1Initial)
                sExportType = "Initial";
            else
                sExportType = "Bulk";

            sV1HeaderLine = "\"" + sExportType.ToUpper() + "\"";
            sV1HeaderLine = sV1HeaderLine + ",\"" + String.Format("{0:yyyyMMdd}", dtQueryDate) + "\"";
            sV1HeaderLine = sV1HeaderLine + ",\"" + String.Format("{0:yyyyMMdd}", dtQueryDate.AddYears(-1)) + "\"";
            sV1HeaderLine = sV1HeaderLine + ",\"" + String.Format("{0:yyyyMMdd}", dtQueryDate) + "\"";
            sV1HeaderLine = sV1HeaderLine + ",\"" + sPracticeCode + "\"";

        }
    }
}
