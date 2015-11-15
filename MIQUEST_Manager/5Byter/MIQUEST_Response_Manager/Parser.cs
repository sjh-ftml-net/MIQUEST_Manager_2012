using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UTILS;

namespace MIQUEST_Response_Manager
{
    class Parser
    {
        HomeForm homeForm;
        Utils cUtils = new Utils();

        public Parser(HomeForm hf)
        {
            homeForm = hf;
        }

        // ----------------------------------------------------------------------------------------------------
        // Process the response files (step through each)
        // ----------------------------------------------------------------------------------------------------
        public bool ProcessResponseFiles(string sPath)
        {
            bool bFatal = false;

            DirectoryInfo di = new DirectoryInfo(sPath);
            FileInfo[] rgFiles = di.GetFiles("QOF*R.TPL", SearchOption.TopDirectoryOnly);
            homeForm.MQ_ProgressBar.Maximum = rgFiles.Length * 2;

            StreamWriter fFILE1 = new StreamWriter(homeForm.cGlobals.sOutPath + "TEMP.txt");

                foreach (FileInfo fi in rgFiles)
                {
                    ProcessResponseFile(fi.FullName, fFILE1);
                    homeForm.MQ_ProgressBar.Value = homeForm.MQ_ProgressBar.Value + 1;
                }

                fFILE1.Close();

            return bFatal;
        }

        // ----------------------------------------------------------------------------------------------------
        // Process a response file
        // ----------------------------------------------------------------------------------------------------
        private void ProcessResponseFile(string sFilePathName, StreamWriter fFILE1)
        {
            bool bReportFile = true;

            StreamReader fResponseFile = new StreamReader(sFilePathName);
            string sLine = fResponseFile.ReadLine().Trim();
            while (sLine != null)
            {

                if (sLine != "" && !sLine.StartsWith("#") && bReportFile)
                {

                    if (sLine.StartsWith("WHERE CODE IN") || sLine.StartsWith("AND CODE IN"))
                    {
                        string s = sLine.Substring(sLine.IndexOf('(') + 1, sLine.IndexOf(')') - sLine.IndexOf('(') - 1);
                        fFILE1.WriteLine(s);
                    }
                    if (sLine.StartsWith("WHERE CODE NOT_IN") || sLine.StartsWith("AND CODE NOT_IN"))
                    {
                        string s = sLine.Substring(sLine.IndexOf('(') + 1, sLine.IndexOf(')') - sLine.IndexOf('(') - 1);
                        fFILE1.WriteLine("! " + s);
                    }
                }
                sLine = fResponseFile.ReadLine();
            }

        }
    }
}
