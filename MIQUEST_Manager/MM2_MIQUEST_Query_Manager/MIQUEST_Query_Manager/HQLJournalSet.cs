using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace MIQUEST_Query_Manager
{
    public class HQLJournal : IEnumerable
    {
        public const int Max = 100;
        public HQLJournalSetItem[] JournalSetList = new HQLJournalSetItem[Max];
        private int iIndex = 0;
        public string ErrorMessage = "";
        public bool FatalError = false;
        public int Length;

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)new HQLJournalSetEnumerator(this);
        }

        private class HQLJournalSetEnumerator : IEnumerator
        {
            private HQLJournal hQLJournalSet;
            private int iEnIndex;

            public HQLJournalSetEnumerator(HQLJournal hQLJournalSet)
            {
                this.hQLJournalSet = hQLJournalSet;
                iEnIndex = -1;
            }
            public bool MoveNext()
            {
                iEnIndex++;
                if (iEnIndex >= hQLJournalSet.iIndex)
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
                    return (hQLJournalSet.JournalSetList[iEnIndex]);
                }
            }
        }

        public bool Add(string DefinitionString)
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
                HQLJournalSetItem cJournalSetItem = new HQLJournalSetItem();
                if (cJournalSetItem.ProcessJournalSetDefinition(DefinitionString))
                {
                    JournalSetList[iIndex++] = cJournalSetItem;
                }
                else
                {
                    FatalError = true;
                    ErrorMessage = cJournalSetItem.ErrorMessage;
                    bResult = false;
                }

                Length = iIndex;
            }
            return bResult;
        }

        public void Clear()
        {
            for (int i = 0; i < Length; i++)
            {
                JournalSetList[i].Clear();
            }

            iIndex = 0;
            Length = 0;
        }

        public HQLJournalSetItem Top()
        {
            if (iIndex > -1)
                return JournalSetList[iIndex - 1];
            else
                return null;
        }

        public class HQLJournalSetItem : IEnumerable
        {
            // Only allowed 20 print clauses in a file
            public const int Max = 20;
            public CodeSet[] CodeSetList = new CodeSet[Max];
            private int iIndex = 0;
            public int CodeSetCount = 0;

            public string Name = "";
            public string CodeType = "";
            public string Period = "";
            public string SetType = "";

            public bool FatalError = false;
            public string ErrorMessage = "";

            public IEnumerator GetEnumerator()
            {
                return (IEnumerator)new HQLJournalSetItemEnumerator(this);
            }

            private class HQLJournalSetItemEnumerator : IEnumerator
            {
                private HQLJournalSetItem hQLJournalSetItem;
                private int iEnIndex;

                public HQLJournalSetItemEnumerator(HQLJournalSetItem hQLJournalSetItem)
                {
                    this.hQLJournalSetItem = hQLJournalSetItem;
                    iEnIndex = -1;
                }
                public bool MoveNext()
                {
                    iEnIndex++;
                    if (iEnIndex >= hQLJournalSetItem.iIndex)
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
                        return (hQLJournalSetItem.CodeSetList[iEnIndex]);
                    }
                }
            }

            // $START_JOURNAL_SET; Set1; CTV3; CURRENT; DIAGNOSES
            public bool ProcessJournalSetDefinition(string sBlockText)
            {
                bool bResult = true;
                string sLine;
                string[] sFields;

                StringReader strReader = new StringReader(sBlockText);
                // Read Descriptor Line
                sLine = strReader.ReadLine();
                sFields = sLine.Split(GlobalDefs.cDelimiter);

                if (sFields.Length != 5)
                {
                    FatalError = true;
                    ErrorMessage = "Malformed Journal Set Instruction " + sLine;
                    bResult = false;
                }
                else
                {
                    Name = sFields[1].Trim();
                    CodeType = sFields[2].Trim();
                    Period = sFields[3].Trim();
                    SetType = sFields[4].Trim();

                    sLine = strReader.ReadLine();
                    while (sLine != null && !FatalError)
                    {
                        if (sLine.StartsWith("$CODES"))
                        {
                            CodeSet cCodeSet = new CodeSet();

                            if (cCodeSet.Create(sLine))
                            {
                                if (iIndex < Max)
                                {
                                    CodeSetList[iIndex++] = cCodeSet;
                                    CodeSetCount = iIndex;
                                }
                                else
                                {
                                    FatalError = true;
                                    ErrorMessage = "Maximum codes in a code set reached " + Name;
                                    bResult = false;
                                }
                            }
                            else
                            {
                                FatalError = true;
                                ErrorMessage = "Malformed CODES Instruction " + sLine;
                                bResult = false;
                            }

                        }
                        sLine = strReader.ReadLine();
                    }

                }

                return bResult;
            }

            public void Clear()
            {
                iIndex = 0;
                CodeSetCount = 0;
            }

            public class CodeSet
            {
                //$CODES; CFN; 1; "XaBVJ%"; Clinical Findings

                public string Name = "";
                public int Order = 0; // 1 = IN, 2 = IN & NOT_IN
                public string HQLTextIn = "";
                public string HQLTextNotIn = "";
                public string Description = "";

                public bool Create(string DefinitionLine)
                {
                    bool bResult = true;
                    string[] sFields = DefinitionLine.Split(GlobalDefs.cDelimiter);

                        if (sFields.Length == 5)
                        {
                            if (sFields[2].Trim() != "1")
                                bResult = false;
                            else
                            {
                                Name = sFields[1].Trim();
                                Order = 1;
                                HQLTextIn = sFields[3].Trim();
                                Description = sFields[4].Trim();
                            }
                        }
                        else if (sFields.Length == 6)
                        {
                            if (sFields[2].Trim() != "2")
                                bResult = false;
                            else
                            {
                                Name = sFields[1].Trim();
                                Order = 1;
                                HQLTextIn = sFields[3].Trim();
                                HQLTextNotIn = sFields[4].Trim();
                                Description = sFields[5].Trim();
                            }
                        }
                        else
                            bResult = false;

                    return bResult;
                }
            }

            public string CreateReportSubset(string Subset, string period)
            {
                string sBuffer = "";

                sBuffer = "FOR " + Subset + "\r\n";
                sBuffer = sBuffer + "SUBSET " + Name + GlobalDefs.cSubsetExt + " TEMP" + "\r\n";
                sBuffer = sBuffer + "FROM JOURNALS (ONE FOR PATIENT)" + "\r\n";
                if (period == "LTC")
                    sBuffer = sBuffer + "WHERE ACTIVE IN (\"R\")" + "\r\n";
                else
                    sBuffer = sBuffer + "WHERE ACTIVE IN (\"R\",\"D\",\"L\")" + "\r\n";

                return sBuffer;
            }

            public string CreateReportPrint(DateTime from, DateTime to, out string LogMessage)
            {
                string sBuffer = "";
                sBuffer = "FOR " + Name + GlobalDefs.cSubsetExt + "\r\n";
                sBuffer = sBuffer + "REPORT" + "\r\n";

                LogMessage = "Codesets " + CodeSetCount.ToString();

                for (int ix = 0; ix < CodeSetCount; ix++)
                {
                    sBuffer = sBuffer + "# " + CodeSetList[ix].Description + "\r\n";
                    sBuffer = sBuffer + "PRINT " + GlobalDefs.JournalFields + "\r\n";
                    sBuffer = sBuffer + "FROM JOURNALS (ALL FOR PATIENT)" + "\r\n";
                    sBuffer = sBuffer + "WHERE CODE IN (" + CodeSetList[ix].HQLTextIn + ")" + "\r\n";
                    if (CodeSetList[ix].Order == 2)
                        sBuffer = sBuffer + "AND CODE NOT_IN (" + CodeSetList[ix].HQLTextNotIn + ")" + "\r\n";
                    sBuffer = sBuffer + "AND DATE IN (\"" + from.ToShortDateString() + "\"-\"" + to.ToShortDateString() + "\")" + "\r\n";
                    LogMessage = LogMessage + ", " + CodeSetList[ix].Description;
                }

                return sBuffer;
            }
        }
    }
}
