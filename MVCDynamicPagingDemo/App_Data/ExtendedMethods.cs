using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Pritey.Framework.Common;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.Helpers;
using ENT = Pritey.Framework.Entity;
using BAL = Pritey.Framework.Logic;
using Truesys.Application.Admin;
using Microsoft.AspNet.Identity.Owin;
using Pritey.Framework.Common;

public static class ExtendedMethods
{
    public static bool IsInAnyRole(this IPrincipal user,Enumration.EntryMode mode)
    {
        var userRoles = Roles.GetRolesForUser(user.Identity.Name);

        //return userRoles.Any(u => roles.Contains(u));
        return true;
    }

    //public static UserAccessLevelEntity GetUserAccessLevel(this IPrincipal user, ENT.Enumration.MasterType mstType)
    //{
    //    MySession.Current.UserAccessLevelEntity = new UserAccessLevelEntity();
    //    if (user.IsInRole("Administrator"))
    //    {
    //        MySession.Current.UserAccessLevelEntity.isView = true;
    //        MySession.Current.UserAccessLevelEntity.isAdd = true;
    //        MySession.Current.UserAccessLevelEntity.isEdit = true;
    //        MySession.Current.UserAccessLevelEntity.isDelete = true;
    //    }
    //    else
    //    {
    //        Guid usrid = new Guid(user.Identity.GetUserId());
    //        int mstid = (Int32)mstType;
    //        if (MySession.Current.UserProfileAccessList.Count == 0)
    //        {
    //            MySession.Current.UserProfileAccessList = new BAL.ProfileUserAccessLevel().GetUserAccessList(usrid);
    //        }

    //        List<ENT.ProfileUserAccessLevel> lstFind = MySession.Current.UserProfileAccessList.Where(x => x.ModuleID == mstid).ToList();
    //        if (lstFind.Count > 0)
    //        {
    //            MySession.Current.UserAccessLevelEntity.isView = lstFind.FirstOrDefault().isView;
    //            MySession.Current.UserAccessLevelEntity.isAdd = lstFind.FirstOrDefault().isAdd;
    //            MySession.Current.UserAccessLevelEntity.isEdit = lstFind.FirstOrDefault().isEdit;
    //            MySession.Current.UserAccessLevelEntity.isDelete = lstFind.FirstOrDefault().isDelete;
    //        }
    //    }
    //    return MySession.Current.UserAccessLevelEntity;
    //}

    public static Guid GetLogged_Userid(this IPrincipal user)
    {
        try
        {
            string userid = user.Identity.GetUserId();

            if(!string.IsNullOrEmpty(userid))
            {
                Guid usrid = new Guid(user.Identity.GetUserId());
                return usrid;
            }
            else
            {
                return Guid.Parse("00000000-0000-0000-0000-000000000000");
            }
        }
        catch (Exception ex)
        {
            return Guid.Parse("00000000-0000-0000-0000-000000000000");
        }
       
        
    }

    public static string GetUserFullName(this IPrincipal user)
    {
        try
        {
            string userid = user.Identity.GetUserId();
            if(!string.IsNullOrEmpty(userid))
            {
                if (string.IsNullOrEmpty(MySession.Current.UserProfile.up_name))
                {
                    MySession.Current.UserProfile = (ENT.UserProfile)new BAL.UserProfile().UserProfile_Get(user.GetLogged_Userid());
                    MySession.Current.UserFullname = MySession.Current.UserProfile.up_name;
                    MySession.Current.UserType = (CRM.Framework.Common.Enumration.UserType)MySession.Current.UserProfile.up_usertype;
                }
                return MySession.Current.UserFullname + "@" + MySession.Current.UserType;
            }
            else
            {
                Guid usrid = new Guid(user.Identity.GetUserId());
                if (string.IsNullOrEmpty(MySession.Current.UserProfile.up_name))
                {
                    MySession.Current.UserProfile = (ENT.UserProfile)new BAL.UserProfile().UserProfile_Get(user.GetLogged_Userid());
                    MySession.Current.UserFullname = MySession.Current.UserProfile.up_name;
                    MySession.Current.UserType = (CRM.Framework.Common.Enumration.UserType)MySession.Current.UserProfile.up_usertype;
                }
                return "Admin@";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
       
        
        
    }

    public static bool isInRoles(this IPrincipal user, params string[] Roles)
    {
        foreach (string str in Roles)
        {
            if (user.IsInRole(str)) { return true; }
        }
        return false;
    }

    public static string GetFormatedDate(this DateTime date)
    {
        return date.ToString("dd/MM/yyyy");
    }

    public static string GetFormatedTime(this DateTime date)
    {
        return date.ToString("hh:mm:ss tt");
    }

    public static string GetFormatedDateTime(this DateTime date)
    {
        return date.ToString("dd/MM/yyyy hh:mm:ss tt");
    }

    public static string GetFromatedAmount(this Decimal amt)
    {
        return amt.ToString("c");
    }

    public static bool ValidatePANCardNumber(this String str)
    {

        bool blnResult = false;
        var match = Regex.Match(str, @"[A-Z]{5}\d{4}[A-Z]{1}", RegexOptions.IgnoreCase);

        if (match.Success)
        {
            blnResult = true;
        }

        return blnResult;
    }

    public static bool ValidateUserName(this String str)
    {
        bool blnResult = false;
        var match = Regex.Match(str, @"^[a-zA-Z][a-zA-Z0-9]{3,9}$", RegexOptions.IgnoreCase);

        if (match.Success)
        {
            blnResult = true;
        }
        return blnResult;
    }

    public static bool ValidateDate(this String strDate, string dateFormat)
    {
        bool blnResult = false;
        if (Regex.IsMatch(strDate, "(((0|1)[0-9]|2[1-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$"))
        {
            DateTime dt;
            blnResult = DateTime.TryParseExact(strDate, dateFormat, new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
        }
        else
        {
            blnResult = false;
        }
        return blnResult;
    }

    public static string ListToCSVString<T>(this ICollection<T> lst, string seprater)
    {
        return string.Join(seprater, lst);
    }

    public static string GetSubString(this string strValue, int Length)
    {
        if (strValue.Length > Length)
        {
            return strValue.Substring(0, Length);
        }
        else
        {
            return strValue;
        }
    }

    public static string GetSubString(this Int64 strValue, int Length)
    {
        if (strValue.ToString().Length > Length)
        {
            return strValue.ToString().Substring(0, Length);
        }
        else
        {
            return strValue.ToString();
        }
    }

    public static void SortList<T>(List<T> list, string columnName, string sortDir)
    {
        var property = typeof(T).GetProperty(columnName);
        var multiplier = sortDir != "asc" ? -1 : 1;
        list.Sort((t1, t2) =>
        {
            var col1 = property.GetValue(t1);
            var col2 = property.GetValue(t2);
            return multiplier * Comparer<object>.Default.Compare(col1, col2);
        });
    }

    public static bool ValiDateMasterArry(string[] strarr)
    {
        bool blnResult = true;
        if (strarr != null)
        {
            if (strarr.Count() > 0)
            {
                foreach (string el in strarr)
                {
                    Guid outID = new Guid();
                    if (Guid.TryParse(el, out outID))
                    {
                        if (el.ToString() == "00000000-0000-0000-0000-000000000000")
                        {
                            blnResult = false;
                            break;
                        }
                    }
                    else { blnResult = false; break; }
                }
            }
            else { blnResult = false; }
        }
        else { blnResult = false; }
        return blnResult;
    }

    //public static Dictionary<string, ENT.Enumration.MasterMappingType> ArrayToDirectory(string[] arr, ENT.Enumration.MasterMappingType enumMaster)
    //{
    //    Dictionary<string, ENT.Enumration.MasterMappingType> dctResult = new Dictionary<string, ENT.Enumration.MasterMappingType>();
    //    foreach (string el in arr)
    //    {
    //        dctResult.Add(el, enumMaster);
    //    }
    //    return dctResult;
    //}
}
