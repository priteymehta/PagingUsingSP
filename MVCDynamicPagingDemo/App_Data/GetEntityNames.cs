using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BAL = CRM.Framework.Logic;
using ENT = CRM.Framework.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Principal;
using Truesys.Application.Admin;
using CRMParag;

namespace Truesys.Application.Admin
{
    public static class GetEntityName
    {
        private static ApplicationUserManager _userManager;

        public static ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public static List<T> GetCreateUpdateName<T>(this List<T> list)
        {
            IList<PropertyInfo> propertyInfos = typeof(T).GetProperties();
            //add value for each property.
            foreach (T obj in list)
            {
                string strCreateBy = "", strUpdateBy = "";
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.Name == "CreatedBy")
                    {
                        try
                        {
                            strCreateBy = propertyInfo.GetValue(obj, null).ToString();
                            string strname = UserManager.FindById(strCreateBy).UserName;
                            obj.GetType().GetProperty("CreatedByName").SetValue(obj, strname, null);
                        }
                        catch { }
                    }
                    if (propertyInfo.Name == "UpdatedBy")
                    {
                        try
                        {
                            strUpdateBy = propertyInfo.GetValue(obj, null).ToString();
                            string strname = UserManager.FindById(strUpdateBy).UserName;
                            obj.GetType().GetProperty("UpdatedByName").SetValue(obj, strname, null);
                        }
                        catch { }
                    }
                }
            }
            return list;
        }


        public static string GetUsername(this Guid userid)
        {
            string strReturn = "User Not Found.!!";
            var user = UserManager.FindById(userid.ToString());
            if(user != null) { strReturn = user.UserName + " (" + user.Email + ")"; }
            return strReturn;
        }

        // to be continued...
    }
}


