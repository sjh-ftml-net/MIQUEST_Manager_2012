using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace MIQUEST_Query_Manager
{
    class Parser
    {
        private HomeForm homeForm;

        public bool FatalError = false;
        public string ErrorMessage = "";

        private char cDelimiter = ';';
        private Stack stsLineNumberStack = new Stack();

        private string sTemplateFileName;
        int sLineNumber;

        public Parser(HomeForm homeForm)
        {
            this.homeForm = homeForm;
        }

        public void Parse(string sFileName, StreamWriter fLogFile)
        {
            string[] sFields;
            string sBlockText;

            sLineNumber = 0;
            string FileName = sFileName;

            if (!File.Exists(FileName))
            {
                FatalError = true;
                ErrorMessage = "File not found: " + FileName;
            }
            else
            {

                fLogFile.WriteLine("Processing: " + sFileName);
                sTemplateFileName = sFileName;

                StreamReader fTemplate = new StreamReader(sFileName);
                string sLine = ReadTemplatesLine(fTemplate);

                while (sLine != null && !FatalError)
                {
                    if (!sLine.StartsWith("#") && sLine != "")
                    {
                        if (sLine.StartsWith("$INCLUDE"))
                        {
                            sFields = sLine.Split(cDelimiter);
                            string sIncFileName = Path.GetDirectoryName(sFileName);
                            if (!sIncFileName.EndsWith("\\")) sIncFileName = sIncFileName + "\\";
                            sIncFileName = sIncFileName + sFields[1].Trim();

                            // Recurse through INCLUDE files stack sLine_number
                            stsLineNumberStack.Push(sLineNumber);
                            Parse(sIncFileName, fLogFile);
                            sLineNumber = Convert.ToInt32(stsLineNumberStack.Pop());
                            sTemplateFileName = sFileName;
                        }
                        else if (sLine.StartsWith("$COHORT"))
                        {
                            // Read the text in this block
                            sBlockText = ReadBlock(fTemplate, sLine, "$ECOHORT");
                            if (!FatalError)
                            {
                                if (homeForm.cCohorts.Add(sBlockText))
                                {
                                    if (homeForm.cCohorts.Top().Map == "")
                                        fLogFile.WriteLine("Created Cohort: " + homeForm.cCohorts.Top().Name + ", " + homeForm.cCohorts.Top().Description);
                                    else
                                        fLogFile.WriteLine("Created Cohort: " + homeForm.cCohorts.Top().Name + ", " + homeForm.cCohorts.Top().Description + " **MAPPED");
                                }
                                else
                                {
                                    FatalError = true;
                                    ErrorMessage = homeForm.cCohorts.ErrorMessage;
                                }
                            }
                        }
                        else if (sLine.StartsWith("$START_JOURNAL_SET"))
                        {
                            sBlockText = ReadBlock(fTemplate, sLine, "$END_JOURNAL_SET");
                            if (!FatalError)
                            {
                                if (homeForm.cJournal.Add(sBlockText))
                                {
                                    fLogFile.WriteLine("Created Journal Set: " + homeForm.cJournal.Top().Name);
                                }
                                else
                                {
                                    FatalError = true;
                                    ErrorMessage = homeForm.cJournal.ErrorMessage;
                                }
                            }
                        }
                        else if (sLine.StartsWith("$START_JOURNAL_GROUP"))
                        {
                            sBlockText = ReadBlock(fTemplate, sLine, "$END_JOURNAL_GROUP");
                            fLogFile.WriteLine("Processing Journal Group");
                            //bFatal = process_journal_group(filename, sLine, template_file);
                        }
                        else if (sLine.StartsWith("$GENERATE_ENCOUNTERS"))
                        {
                            fLogFile.WriteLine("Generating Encounters");
                            //bFatal = generate_encounters();
                        }
                        else if (sLine.StartsWith("$GENERATE_REFERRALS"))
                        {
                            fLogFile.WriteLine("Generating Referrals");
                            //bFatal = generate_referrals();
                        }
                        else
                        {
                            fLogFile.WriteLine("Unknown Keyword Encountered (" + sLine + ")");
                        }
                    }

                    if (!FatalError) sLine = ReadTemplatesLine(fTemplate);
                    if (homeForm.ifProgressBar.Value < 100) homeForm.ifProgressBar.Value = homeForm.ifProgressBar.Value + 5;
                }

                fTemplate.Close();
                fLogFile.WriteLine("Completed : " + sFileName);
            }
        }


        private string ReadBlock(StreamReader fTemplate, string sLine, string sTerminator)
        {
            string sBuffer = "";

            //sLine = ReadTemplatesLine(fTemplate);
            while (sLine != null && !sLine.StartsWith(sTerminator))
            {
                if (!sLine.StartsWith("#") && sLine != "")
                {
                    if (sBuffer == "")
                        sBuffer = sLine;
                    else
                        sBuffer = sBuffer + "\r\n" + sLine;
                }
                sLine = ReadTemplatesLine(fTemplate);
            }

            // Check we ended ok
            if (sLine == null)
            {
                //homeForm.bFatal = true;
                //homeForm.sErrorMessage = "Unexpected end of file " + sTemplateFileName + " before " + sTerminator;
                FatalError = true;
                ErrorMessage = "Unexpected end of file " + sTemplateFileName + " before " + sTerminator;
            }

            return sBuffer;
        }

        private string ReadTemplatesLine(StreamReader fTemplate)
        {
            string sLine = fTemplate.ReadLine();
            if (sLine != null)
            {
                sLineNumber++;
                sLine = sLine.Trim();
            }
            return sLine;
        }

    }
}
