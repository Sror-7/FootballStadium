using FootballStadium_Business;
using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FootballStadium.Global_Classes
{
    internal class clsGlobal
    {
        public static clsUser CurrentUser;
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public static bool DeleteFileFromWindowsRegister(string keyPath, string valueName)
        {

            try
            {
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue(valueName);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("UnauthorizedAccessException: Run the program with administrative privileges.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                //this will store pass an username in system database.
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LoginInformationFootballStadium";


                string valueName = "UserNameAndPassword";
                string valueData = Username + "#//#" + Password;

                if (Username == "")
                {
                    DeleteFileFromWindowsRegister(@"SOFTWARE\LoginInformationFootballStadium\", valueName);
                    return true;
                }

                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LoginInformationFootballStadium";
                string valueName = "UserNameAndPassword";

                string value = Registry.GetValue(keyPath, valueName, null) as string;

                if (value != null)
                {
                    string[] result = value.Split(new string[] { "#//#" }, StringSplitOptions.None);

                    Username = result[0];
                    Password = result[1];
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
