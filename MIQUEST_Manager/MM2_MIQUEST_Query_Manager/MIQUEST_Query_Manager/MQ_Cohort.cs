using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MIQUEST_Query_Manager
{
    public class MQ_Cohort_List
    {
        public bool Error = false;
        public string ErrorMessage = "";
        public string HQLHeaderText = "";
        private const string sPatientPrint = "PRINT NHS_NUMBER,REFERENCE,SEX,DATE_OF_BIRTH,POSTCODE,GP_USUAL,REGISTERED_DATE,REMOVED_DATE,DATE_OF_DEATH,ACTIVE";


        private Hashtable Index = new Hashtable();
        private ArrayList List = new ArrayList();

        public bool Add(MQ_Cohort Item)
        {
            bool bFatal = false;

            if (Index.ContainsKey(Item.Name))
            {
                Error = true;
                ErrorMessage = "Duplicate Cohort Name";
                bFatal = true;
            }
            else
            {
                Index.Add(Item.Name, null);
                List.Add(Item);
            }
            return bFatal;
        }

        public void Clear()
        {
            List.Clear();
            Index.Clear();
        }

        public int Save(string Path, int HQLOrder)
        {
            // Write the basic subset queries
            foreach (MQ_Cohort e in List)
            {
                string sFilename = Path + HQLOrder.ToString("D3") + "_" + e.Name + ".HQL";
                StreamWriter fOut = new StreamWriter(sFilename);

                // Prepare Header
                string HQLHead = HQLHeaderText.Replace("<TITLE>", e.Name);
                HQLHead = HQLHead.Replace("<DESC>", e.Description);
                HQLHead = HQLHead.Replace("<ORDER>", HQLOrder++.ToString("D3"));

                fOut.Write(HQLHead);
                fOut.WriteLine(e.HQL_Text);
                fOut.Close();
            }

            // Write the index exports for the mapped queries

            foreach (MQ_Cohort e in List)
            {
                if (e.Map != "")
                {
                    // Write the Subset G file
                    string sQname = e.Name + "XG";
                    string sFilename = Path + HQLOrder.ToString("D3") + "_" + sQname + ".HQL";
                    StreamWriter fOut = new StreamWriter(sFilename);
                    // Prepare Header
                    string HQLHead = HQLHeaderText.Replace("<TITLE>", sQname);
                    HQLHead = HQLHead.Replace("<DESC>", e.Description);
                    HQLHead = HQLHead.Replace("<ORDER>", HQLOrder++.ToString("D3"));
                    fOut.Write(HQLHead);

                    string sLocalSubset = e.Name + "G";
                    fOut.WriteLine("FOR " + e.Name);
                    fOut.WriteLine("SUBSET " + sLocalSubset + " LOCAL");
                    fOut.WriteLine("FROM PATIENTS");
                    fOut.WriteLine("WHERE ACTIVE IN (\"R\",\"T\",\"S\",\"D\",\"L\",\"P\")");
                    fOut.Close();

                    // Write the Report R File
                    sQname = e.Name + "XR";
                    sFilename = Path + HQLOrder.ToString("D3") + "_" + sQname + ".HQL";
                    fOut = new StreamWriter(sFilename);
                    // Prepare Header
                    HQLHead = HQLHeaderText.Replace("<TITLE>", sQname);
                    HQLHead = HQLHead.Replace("<DESC>", e.Description);
                    HQLHead = HQLHead.Replace("<ORDER>", HQLOrder++.ToString("D3"));
                    fOut.Write(HQLHead);

                    fOut.WriteLine("FOR " + sLocalSubset);
                    fOut.WriteLine("REPORT");
                    fOut.WriteLine(sPatientPrint);
                    fOut.WriteLine("FROM PATIENTS");
                    fOut.WriteLine("WHERE ACTIVE IN (\"R\",\"T\",\"S\",\"D\",\"L\",\"P\")");

                    fOut.Close();
                }
            }

            return HQLOrder;
       }

    }

    public class MQ_Cohort
    {
        public string Name = "";
        public string Description = "";
        public string HQL_Text = "";
        public string Map = "";

        public bool Error = false;
        public string ErrorMessage = "";

        public bool ProcessCohortBlock(string sBlockText, char cDelimiter)
        {
            bool bFatal = false;
            string sLine;
            string[] sFields;

            StringReader strReader = new StringReader(sBlockText);
            // Read Descriptor Line
            sLine = strReader.ReadLine();
            sFields = sLine.Split(cDelimiter);

            if (sFields.Length != 3)
            {
                Error = true;
                ErrorMessage = "Malformed Cohort Definition Line";
                bFatal = true;
            }
            else
            {
                Name = sFields[1].Trim();
                Description = sFields[2].Trim();
                sLine = strReader.ReadLine();
                while (sLine != null)
                {
                    if (!sLine.StartsWith("#") && sLine != "")
                    {
                        sLine = sLine.Trim();
                        sLine = sLine.Replace("<name>", Name);
                        if (sLine.StartsWith("$MAP"))
                        {
                            sFields = sLine.Split(cDelimiter);
                            if (sFields.Length != 2)
                            {
                                Error = true;
                                ErrorMessage = "Malformed Cohort Map Line";
                                bFatal = true;
                            }
                            else
                                Map = sFields[1].Trim();
                        }
                        else
                        {
                            if (HQL_Text == "")
                                HQL_Text = sLine;
                            else
                                HQL_Text = HQL_Text + "\r\n" + sLine;
                        }
                    }
                    sLine = strReader.ReadLine();
                }
            }

            return bFatal;
        }
    }
}
