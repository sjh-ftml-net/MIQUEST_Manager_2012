using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MIQUEST_Query_Manager
{
    public class HQLCohort : IEnumerable
    {
        public const int Max = 10;
        private HQLCohortItem[] CohortList = new HQLCohortItem[Max];
        private int iIndex = 0;
        public string ErrorMessage = "";
        public bool FatalError = false;
        public int Length;
        public string GlobalCohort;

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)new HQLCohortEnumerator(this);
        }

        private class HQLCohortEnumerator : IEnumerator
        {
            private HQLCohort hQLCohort;
            private int iEnIndex;

            public HQLCohortEnumerator(HQLCohort hQLCohort)
            {
                this.hQLCohort = hQLCohort;
                iEnIndex = -1;
            }
            public bool MoveNext()
            {
                iEnIndex++;
                if (iEnIndex >= hQLCohort.iIndex)
                    return false;
                else
                    return true;
            }
            public void Reset()
            {
                iEnIndex = -1;
            }
            public object Current
            {
                get
                {
                    return (hQLCohort.CohortList[iEnIndex]);
                }
            }
        }

        #region HQLCohort Methods

        public bool Add(string DefinitionString)
        {
            bool bResult = true;
            string sCohort;

            if (iIndex >= Max)
            {
                bResult = false;
                FatalError = true;
                ErrorMessage = "Maximum number of cohorts exceeded";
            }
            else
            {
                HQLCohortItem cCohort = new HQLCohortItem();
                if (cCohort.ProcessCohortDefinition(DefinitionString, out sCohort))
                {
                    CohortList[iIndex++] = cCohort;
                    if (sCohort != "")
                        GlobalCohort = sCohort;
                }
                else
                {
                    FatalError = true;
                    ErrorMessage = cCohort.ErrorMessage;
                    bResult = false;
                }

                Length = iIndex;
            }

            return bResult;
        }

        public bool Add(string Name, string Description)
        {
            bool bResult = true;

            if (iIndex >= Max)
            {
                bResult = false;
                FatalError = true;
                ErrorMessage = "Maximum number of cohorts exceeded";
            }
            else
            {
                HQLCohortItem cCohort = new HQLCohortItem();
                cCohort.Name = Name;
                cCohort.Description = Description;

                CohortList[iIndex++] = cCohort;

                Length = iIndex;
            }

            return bResult;
        }

        public HQLCohortItem Top()
        {
            if (iIndex > -1)
                return CohortList[iIndex - 1];
            else
                return null;
        }

        public void Clear()
        {
            iIndex = 0;
            Length = 0;
        }

        #endregion

        public class HQLCohortItem
        {
            public string Name = "";
            public string Description = "";
            public string HQLText = "";
            public string Map = "";

            public bool FatalError = false;
            public string ErrorMessage = "";

            public bool ProcessCohortDefinition(string sBlockText, out string GlobalCohort)
            {
                bool bResult = true;
                string sLine;
                string[] sFields;

                GlobalCohort = "";

                StringReader strReader = new StringReader(sBlockText);
                // Read Descriptor Line
                sLine = strReader.ReadLine();
                sFields = sLine.Split(GlobalDefs.cDelimiter);

                if (sFields.Length != 3)
                {
                    bResult = false;
                    FatalError = true;
                    ErrorMessage = "Malformed Cohort Definition Line";
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
                                sFields = sLine.Split(GlobalDefs.cDelimiter);
                                if (sFields.Length != 2)
                                {
                                    FatalError = true;
                                    ErrorMessage = "Malformed Cohort Map Line";
                                    bResult = false;
                                }
                                else
                                {
                                    Map = sFields[1].Trim();
                                    GlobalCohort = Name;
                                }
                            }
                            else
                            {
                                if (HQLText == "")
                                    HQLText = sLine;
                                else
                                    HQLText = HQLText + "\r\n" + sLine;
                            }
                        }
                        sLine = strReader.ReadLine();
                    }
                }

                return bResult;
            }

            public string CreateReportSubset()
            {
                string sBuffer = "";

                sBuffer = "FOR " + Name + "\r\n";
                sBuffer = sBuffer + "SUBSET " + Name + GlobalDefs.cSubsetExt + " TEMP" + "\r\n";
                sBuffer = sBuffer + "FROM PATIENTS" + "\r\n";
                sBuffer = sBuffer + "WHERE ACTIVE IN (\"R\",\"T\",\"S\",\"D\",\"L\",\"P\")" + "\r\n";

                return sBuffer;
            }

            public string CreateReportPrint()
            {
                string sBuffer = "";
                sBuffer = "FOR " + Name + GlobalDefs.cSubsetExt + "\r\n";
                sBuffer = sBuffer + "REPORT" + "\r\n";
                sBuffer = sBuffer + "PRINT " + GlobalDefs.PatientFields + "\r\n";
                sBuffer = sBuffer + "FROM PATIENTS" + "\r\n";
                sBuffer = sBuffer + "WHERE ACTIVE IN (\"R\",\"T\",\"S\",\"D\",\"L\",\"P\")" + "\r\n";

                return sBuffer;
           }
        }
    }
}
