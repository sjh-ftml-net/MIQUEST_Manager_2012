using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MIQUEST_Query_Manager
{
    class Utils
    {
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
