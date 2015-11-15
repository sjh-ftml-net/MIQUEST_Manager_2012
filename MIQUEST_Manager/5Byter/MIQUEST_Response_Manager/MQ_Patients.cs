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

        #region VARS
        public bool ixSet = false;

        public ArrayList PatientsRaw = new ArrayList();
        public ArrayList Patients = new ArrayList();
        public int Length = 0;
        public ArrayList Columns = new ArrayList();
        public int ColumnCount = 0;

        public int NullNHSNum = 0;

        private Utils cUtils = new Utils();

        #endregion

        public void WriteV1File(GlobalData cGlobals)
        {

            string sFileName = cGlobals.sPracticeCode + "_Tribal_-_" + cGlobals.sExportType + "_Patients_1.csv";

            StreamWriter fOutFile = new StreamWriter(cGlobals.sOutPath + sFileName);
            fOutFile.WriteLine(cGlobals.sV1HeaderLine);

            foreach (string sP in PatientsRaw)
            {
                string[] sFields = sP.Split(',');

                string sOut;
                // Date of Birth
                sOut = cUtils.PrintFieldV1(Columns.IndexOf("DATE_OF_BIRTH"), sFields, "DATE");
                // Sex
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("SEX"), sFields, "STRING");
                // GP
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("GP"), sFields, "STRING");
                // Usual GP
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("GP_USUAL"), sFields, "STRING");
                // Registration Status
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("ACTIVE"), sFields, "STRING");
                // Registration Date
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("REGISTERED_DATE"), sFields, "DATE");
                // Removed Date
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("REMOVED_DATE"), sFields, "DATE");
                // Postcode
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("POSTCODE"), sFields, "STRING");
                // NHS Number
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
                // Patient ID -- USING NHS NUMBER AS PATIENT ID
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
                // Date of Death
                sOut = sOut + "," + cUtils.PrintFieldV1(Columns.IndexOf("DATE_OF_DEATH"), sFields, "DATE");

                fOutFile.WriteLine(sOut);
            }

            fOutFile.Close();
            
        }

        public void CreatePatientDataBase()
        {

            foreach (string sP in PatientsRaw)
            {
                MQ_Patient p = new MQ_Patient(this);
                p.LoadFromString(sP);
                Patients.Add(p);
            }
        }

        public void Assess()
        {
            string[] sFields;
            string sNHSnum;
            foreach (string s in PatientsRaw)
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

            s = "Patients " + Length.ToString();
            s = s + " NULL NHSNo " + NullNHSNum.ToString();
            return s;
        }

        public void SetIX(string sLine)
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
            PatientsRaw.Add(sLine);
            Length++;
        }

        public void Clear()
        {
            ixSet = false;
            Length = 0;
            NullNHSNum = 0;
            ColumnCount = 0;
            PatientsRaw.Clear();
            Patients.Clear();
            Columns.Clear();
        }       
    }
    
    public class MQ_Patient
    {

        // Patient MIQUEST attributes
        // Mandatory (All)
        public string sREFERENCE;
        public string sDATE_OF_BIRTH;
        public DateTime dDATE_OF_BIRTH;
        public string sSEX;
        public string sPOSTCODE;
        public string sMARITAL_STATUS;
        public string sGP;
        public string sGP_USUAL;
        public string sACTIVE;
        public string sREGISTERED_DATE;
        public DateTime dREGISTERED_DATE;
        public string sREMOVED_DATE;
        public DateTime dREMOVED_DATE;
        public string sHA;
        public string sPCG;
        public string sSURGERY;
        public string sMILEAGE;
        public string sDISPENSING;
        // Optional (All)
        public string sETHNIC;
        public string sDATE_OF_DEATH;
        public DateTime dDATE_OF_DEATH;
        public string sPRACTICE;
        // Mandatory (Local)
        public string sSURNAME;
        public string sFORENAME;
        public string sTITLE;
        public string sADDRESS;
        public string sNHS_NUMBER;
        // Optional (Local)
        public string sADDRESS_1;
        public string sADDRESS_2;
        public string sADDRESS_3;
        public string sADDRESS_4;
        public string sADDRESS_5;
        public string sPRACT_NUMBER;

        private MQ_Patients mQ_Patients;

        private Utils cUtils = new Utils();

        public MQ_Patient(MQ_Patients mQ_Patients)
        {
            // TODO: Complete member initialization
            this.mQ_Patients = mQ_Patients;
        }

        public void LoadFromString(string sLine)
        {

            string[] sFields = sLine.Split(',');

            sREFERENCE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("REFERENCE"), sFields, "STRING");
            sDATE_OF_BIRTH = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("DATE_OF_BIRTH"), sFields, "DATE");
            dDATE_OF_BIRTH = Convert.ToDateTime(sDATE_OF_BIRTH);
            sSEX = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("SEX"), sFields, "STRING");
            sPOSTCODE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("POSTCODE"), sFields, "STRING");
            sMARITAL_STATUS = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("MARITAL_STATUS"), sFields, "STRING");
            sGP = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("GP"), sFields, "STRING");
            sGP_USUAL = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("GP_USUAL"), sFields, "STRING");
            sACTIVE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ACTIVE"), sFields, "STRING");
            sREGISTERED_DATE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("REGISTERED_DATE"), sFields, "DATE");
            dREGISTERED_DATE = Convert.ToDateTime(sREGISTERED_DATE);
            sREMOVED_DATE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("REMOVED_DATE"), sFields, "DATE");
            dREMOVED_DATE = Convert.ToDateTime(sREMOVED_DATE);
            sHA = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("HA"), sFields, "STRING");
            sPCG = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("PCG"), sFields, "STRING");
            sSURGERY = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("SURGERY"), sFields, "STRING");
            sMILEAGE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("MILEAGE"), sFields, "STRING");
            sDISPENSING = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("DISPENSING"), sFields, "STRING");
            // Optional (All)
            sETHNIC = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ETHNIC"), sFields, "STRING");
            sDATE_OF_DEATH = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("DATE_OF_DEATH"), sFields, "STRING");
            //public DateTime dDATE_OF_DEATH;
            sPRACTICE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("PRACTICE"), sFields, "STRING");
            // Mandatory (Local)
            sSURNAME = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("SURNAME"), sFields, "STRING");
            sFORENAME = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("FORENAME"), sFields, "STRING");
            sTITLE = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("TITLE"), sFields, "STRING");
            sADDRESS = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ADDRESS"), sFields, "STRING");
            sNHS_NUMBER = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("NHS_NUMBER"), sFields, "STRING");
            // Optional (Local)
            sADDRESS_1 = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ADDRESS_1"), sFields, "STRING");
            sADDRESS_2 = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ADDRESS_2"), sFields, "STRING");
            sADDRESS_3 = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ADDRESS_3"), sFields, "STRING");
            sADDRESS_4 = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ADDRESS_4"), sFields, "STRING");
            sADDRESS_5 = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("ADDRESS_5"), sFields, "STRING");
            sPRACT_NUMBER = cUtils.PrintFieldV1(mQ_Patients.Columns.IndexOf("PRACT_NUMBER"), sFields, "STRING");
        }
    }
}
