using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace UTILS
{
    class Utils
    {

        public bool SaveFileFromArrayList(string FilePath, ArrayList Data, ArrayList ColumnHeaders, bool Pseudonymise = false, int PseudoDateShift = 0)
        {
            bool bRet = true;

            StreamWriter fOutFile = new StreamWriter(FilePath);

            fOutFile.WriteLine(PrintStringArray(ColumnHeaders));

            foreach (string sLine in Data)
            {
                string[] sFields = sLine.Split(',');

                if (sFields.Length != ColumnHeaders.Count)
                {
                    bRet = false;
                }

                string sRow = "";
                foreach (string sAttributeName in ColumnHeaders)
                {
                    int pos = ColumnHeaders.IndexOf(sAttributeName);
                    if (pos != -1)
                    {
                        string sData;
                        if (sAttributeName.IndexOf("DATE") != -1)
                            sData = CleanField(pos, sFields, "DATE");
                        else
                            sData = CleanField(pos, sFields, "STRING");

                        if (Pseudonymise)
                        {
                            sData = Pseudomymise(sData, sAttributeName, PseudoDateShift);
                        }

                        sData = Quote(sData);

                        if (sRow == "")
                            sRow = sData;
                        else
                            sRow += "," + sData;
                    }
                }
                fOutFile.WriteLine(sRow);
            }

            fOutFile.Close();

            return bRet;
        }

        public string PrintFieldV1(int iX, string[] sFields, string Type)
        {
            string sOut;

            if (Type == "DATE") // yyyyMMdd style
            {
                if (iX == -1) sOut = "\"\"";
                else sOut = Quote(CleanDateV1(sFields[iX]));
            }
            else if (Type == "HCPTYPE") // Just first character
            {
                if (iX == -1) sOut = "\"\"";
                else
                    sOut = Quote(CleanString(sFields[iX].Substring(0,1)));
            }
            else
            {
                if (iX == -1) sOut = "\"\"";
                else sOut = Quote(CleanString(sFields[iX]));
            }
            return sOut;
        }

        public string CleanField(int iX, string[] sFields, string Type)
        {
            string sOut;
            if (Type == "DATE")
            {
                if (iX == -1) sOut = "\"\"";
                else sOut = CleanDate(sFields[iX]);
            }
            else
            {
                if (iX == -1) sOut = "\"\"";
                else sOut = CleanString(sFields[iX]);
            }

            return sOut;
        }

        public string Quote(string s)
        {
            return "\"" + s + "\"";
        }

        public string CleanString(string s)
        {
            s = s.Replace("\"","");
            s = s.Trim();
            if (s == "Invalid Id") s = "";
            return s;
        }

        public string CleanDateV1(string s)
        {
            s = s.Replace("\"", "");
            s = s.Trim();
            if (s == "99999999") s = "";
            return s;
        }

        public string CleanDate(string s)
        {
            s = s.Replace("\"", "");
            s = s.Trim();
            if (s == "99999999") s = "";
            if (s.Length == 8)
                s = s.Substring(0, 4) + "-" + s.Substring(4, 2) + "-" + s.Substring(6, 2);
            return s;
        }

        public void LogMessage(StreamWriter LogFile, string Message, int status = 0)
        {
            LogFile.WriteLine(DateTime.Now.ToShortDateString() + "," + DateTime.Now.ToLongTimeString() + "," + status.ToString() + "," + Message);
        }

        public string FolderDialog(string sPath)
        {
            string sRes = "";
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            folderDlg.SelectedPath = sPath;
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                sRes = folderDlg.SelectedPath;
            }

            return sRes;
        }

        public string FileDialog(string sPath)
        {
            string sRes = "";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "tpl files (*.tpl)|*.tpl|All files (*.*)|*.*";
            dialog.InitialDirectory = sPath;
            dialog.Title = "Select a template file";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                sRes = dialog.FileName;
            }

            return sRes;
        }

        public bool CheckCreateFolder(string folderpath)
        {
            bool bSuccess = false;

            if (folderpath == "" || folderpath == "\\")
            {
                bSuccess = false;
            }
            else
            {

                if (!Directory.Exists(folderpath))
                {

                    switch (MessageBox.Show("The folder was not found (" + folderpath + "), create the folder?",
                                                "Output folder not found",
                                                MessageBoxButtons.YesNoCancel,
                                                MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            Directory.CreateDirectory(folderpath);
                            bSuccess = true;
                            break;
                        case DialogResult.No:
                            bSuccess = false;
                            break;
                        case DialogResult.Cancel:
                            bSuccess = false;
                            break;
                    }
                }
                else
                {
                    bSuccess = true;
                }
            }

            return bSuccess;
        }

        public void ClearFolder(string path)
        {
            DirectoryInfo target = new DirectoryInfo(path);

            foreach (FileInfo file in target.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in target.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string PrintStringArray(ArrayList sArray)
        {
            string sRes = "";
            foreach (string sAttributeName in sArray)
            {
                if (sArray.IndexOf(sAttributeName) != -1)
                {
                    if (sRes == "")
                        sRes = sAttributeName;
                    else
                        sRes += "," + sAttributeName;
                }
            }

            return sRes;
        }

        public string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public string Pseudomymise(string Attribute, string AttributeName, int iTimeShift)
        {

           DateTime dtDate;
           string sPseudoVal = Attribute;
           byte[] tmpSource;
           byte[] tmpHash;

            if (Attribute != "")
            {
                // Suppress
                if (
                    AttributeName.IndexOf("SURNAME") != -1 || AttributeName.IndexOf("FORENAME") != -1 ||
                    AttributeName.IndexOf("ADRRESS") != -1
                    )
                {
                    sPseudoVal = "";
                }
                // Hash
                else if (
                    AttributeName.IndexOf("NHS_NUMBER") != -1 || AttributeName.IndexOf("REFERENCE") != -1 ||
                    AttributeName.IndexOf("POSTCODE") != -1 || AttributeName.IndexOf("GP") != -1 ||
                    AttributeName.IndexOf("GP_USUAL") != -1 || AttributeName.IndexOf("PCG") != -1 ||
                    AttributeName.IndexOf("SURGERY") != -1 || AttributeName.IndexOf("PRACTICE") != -1 ||
                    AttributeName.IndexOf("PRACT_NUMBER") != -1 
                    )
                {
                    tmpSource = ASCIIEncoding.ASCII.GetBytes(Attribute);
                    tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                    sPseudoVal = ByteArrayToString(tmpHash);
                }
                // Date Adjust
                else if (AttributeName.IndexOf("DATE") != -1)
                {
                    string sDate = Attribute;
                    if (Attribute.Length == 8)
                        sDate = Attribute.Substring(0, 4) + "-" + Attribute.Substring(4, 2) + "-" + Attribute.Substring(6, 2);

                    if (DateTime.TryParse(sDate, out dtDate))
                    {
                        // Time shift all dates
                        dtDate = dtDate.AddYears(0 - iTimeShift);

                        if (AttributeName == "DATE_OF_BIRTH")
                        {
                            DateTime dtNow = DateTime.Today.AddYears(0 - iTimeShift);
                            int age = dtNow.Year - dtDate.Year;

                            if (age >= 0 && age < 5) dtDate = dtNow;
                            else if (age >= 5 && age < 12) dtDate = dtNow.AddYears(0 - 5);
                            else if (age >= 12 && age < 18) dtDate = dtNow.AddYears(0 - 12);
                            else if (age >= 18 && age < 35) dtDate = dtNow.AddYears(0 - 18);
                            else if (age >= 35 && age < 45) dtDate = dtNow.AddYears(0 - 35);
                            else if (age >= 45 && age < 55) dtDate = dtNow.AddYears(0 - 45);
                            else if (age >= 55 && age < 65) dtDate = dtNow.AddYears(0 - 55);
                            else if (age >= 65 && age < 70) dtDate = dtNow.AddYears(0 - 65);
                            else if (age >= 70 && age < 75) dtDate = dtNow.AddYears(0 - 70);
                            else if (age >= 75 && age < 80) dtDate = dtNow.AddYears(0 - 75);
                            else if (age >= 80 && age < 85) dtDate = dtNow.AddYears(0 - 80);
                            else if (age > 85) dtDate = dtNow.AddYears(0 - 85);
                        }
                    }

                    if (Attribute.Length == 8)
                        sPseudoVal = dtDate.ToString("yyyyMMdd");
                    else
                        sPseudoVal = dtDate.ToString("yyyy-MM-dd");
                }                

            }

            return sPseudoVal;
       }

    }
}
