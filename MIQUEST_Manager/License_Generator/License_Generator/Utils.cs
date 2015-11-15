using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace UtilSpace
{
    class Utils
    {
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

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

        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public string EncryptStringAES(string plainText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            string outStr = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public string DecryptStringAES(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
    }
}
