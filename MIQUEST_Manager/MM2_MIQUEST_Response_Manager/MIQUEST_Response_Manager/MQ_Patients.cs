using System;
using System.Collections;
using System.Linq;
using System.Text;
using UTILS;
using System.IO;

namespace MIQUEST_Response_Manager
{
    public class MQ_Patients
    {

        public bool ixSet = false;

        public ArrayList PatientData = new ArrayList();
        public int DataRows = 0;
        public ArrayList Columns = new ArrayList();
        public int ColumnCount = 0;

        public int NullNHSNum = 0;
        private Utils cUtils = new Utils();

        public bool Save(GlobalData cGlobals)
        {
            string sFileName = cGlobals.sOutPath + cGlobals.sPracticeCode + "_" + String.Format("{0:yyyyMMdd}", cGlobals.dtQueryDate) + "_PATIENTS.csv";
            return cUtils.SaveFileFromArrayList(sFileName, PatientData, Columns, cGlobals.bPseudonymise, cGlobals.iPseudoDateShift);
        }

        public void WriteV1File(GlobalData cGlobals)
        {

            string sFileName = cGlobals.sPracticeCode + "_Tribal_-_" + cGlobals.sExportType + "_Patients_1.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);
            fOutFile.WriteLine(cGlobals.sV1HeaderLine);

            foreach (string sP in PatientData)
            {
                string[] sFields = sP.Split(',');

                string sOut;
                // Date of Birth
                sOut = cUtils.PrintFieldV1(Columns.IndexOf("DATE_OF_BIRTH"), sFields, "DATE");
                // Sex
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("SEX"), sFields, "STRING");
                // GP
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("GP"), sFields, "STRING");
                // Usual GP
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("GP_USUAL"), sFields, "STRING");
                // Registration Status
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("ACTIVE"), sFields, "STRING");
                // Registration Date
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("REGISTERED_DATE"), sFields, "DATE");
                // Removed Date
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("REMOVED_DATE"), sFields, "DATE");
                // Postcode
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("POSTCODE"), sFields, "STRING");
                // NHS Number
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
                // Patient ID -- USING NHS NUMBER AS PATIENT ID
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
                // Date of Death
                sOut += "," + cUtils.PrintFieldV1(Columns.IndexOf("DATE_OF_DEATH"), sFields, "DATE");

                fOutFile.WriteLine(sOut);
            }

            fOutFile.Close();
            
        }

        public void Assess()
        {
            string[] sFields;
            string sNHSnum;
            foreach (string s in PatientData)
            {
                sFields = s.Split(',');
                sNHSnum = cUtils.CleanString(sFields[Columns.IndexOf("NHS_NUMBER")]);
                if (sNHSnum == "")
                    NullNHSNum++;
            }
        }

        public string Describe()
        {
            string s;

            s = "Patients " + DataRows.ToString();
            s = s + " NULL NHSNo " + NullNHSNum.ToString();
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
            PatientData.Add(sLine);
            DataRows++;
        }

        public void Clear()
        {
            ixSet = false;
            DataRows = 0;
            NullNHSNum = 0;
            ColumnCount = 0;
            PatientData.Clear();
            Columns.Clear();
        }       
    }
  
}
