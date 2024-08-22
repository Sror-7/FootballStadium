using FootballStadium_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballStadium_Business
{
    public class clsUserPermission
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        public int PermissionID { get; set; }
        public int UserID { get; set; }
        public string FormName { get; set; }
        public bool FullAccess { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Show { get; set; }
        public bool Delete { get; set; }

        public clsUserPermission()
        {
            this.PermissionID = -1;
            this.UserID = -1;
            this.FormName = "";
            this.FullAccess = false;
            this.Add = false;
            this.Edit = false;
            this.Show = false;
            this.Delete = false;
            
            this.Mode = enMode.AddNew;
        }

        private clsUserPermission(int PermissionID, int UserID, string FormName, bool FullAccess, bool Add, bool Edit, bool Show, bool Delete)
        {
            this.PermissionID = PermissionID;
            this.UserID = UserID;
            this.FormName = FormName;
            this.FullAccess = FullAccess;
            this.Add = Add;
            this.Edit = Edit;
            this.Show = Show;
            this.Delete = Delete;

            Mode = enMode.Update;
        }

        public static clsUserPermission Find(int UserID, string FormName)
        {
            int PermissionID = -1;
            bool FullAccess = false, Add = false, Edit = false, Show = false, Delete = false;

            if (clsUserPermissionData.GetPermissionsByID(ref PermissionID, UserID, FormName, ref FullAccess, ref Add, ref Edit, ref Show, ref Delete))
                return new clsUserPermission(PermissionID, UserID, FormName, FullAccess, Add, Edit, Show, Delete);

            else
                return null;
        }
        private bool _AddNewPermission()
        {
            this.FullAccess = (this.Add && this.Edit && this.Show && this.Delete);
            
            this.PermissionID = clsUserPermissionData.AddNewPermission(this.UserID, this.FormName, this.FullAccess,this.Add,this.Edit, this.Show,this.Delete);

            return (this.PermissionID != -1);
        }
        private bool _UpdatePermission()
        {
            this.FullAccess = (this.Add && this.Edit && this.Show && this.Delete);

            return clsUserPermissionData.UpdatePermission(this.UserID, this.FormName, this.FullAccess, this.Add, this.Edit, this.Show, this.Delete);
        }
        public void Clear()
        {
            this.PermissionID = -1;
            this.UserID = -1;
            this.FormName = "";
            this.FullAccess = false;
            this.Add = false;
            this.Edit = false;
            this.Show = false;
            this.Delete = false;

            this.Mode = enMode.AddNew;
        }
        public void UpdateMode()
        {
            Mode = enMode.Update;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPermission())
                    {
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePermission();
            }

            return false;
        }
    }
}
