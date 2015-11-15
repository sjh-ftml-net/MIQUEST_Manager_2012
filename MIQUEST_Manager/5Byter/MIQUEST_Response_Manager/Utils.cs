using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UTILS
{
    class Utils
    {
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

        public string PrintField(int iX, string[] sFields, string Type)
        {
            string sOut;
            if (Type == "DATE")
            {
                if (iX == -1) sOut = "\"\"";
                else sOut = Quote(CleanDate(sFields[iX]));
            }
            else
            {
                if (iX == -1) sOut = "\"\"";
                else sOut = Quote(CleanString(sFields[iX]));
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

    }
}
