using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BAL = Pritey.Framework.Logic;
using ENT = Pritey.Framework.Entity;

public static class CommanMethods
{
    public static bool CreateAccessLevelCookie() {
        bool blnResult = true;
        try {
            //HttpContext.Current.Request.Cookies.Clear();
            //HttpCookie isView = new HttpCookie("isView", MySession.Current.UserAccessLevelEntity.isView.ToString());
            //isView.Expires = DateTime.Now.AddDays(10);
            //HttpCookie isAdd = new HttpCookie("isAdd", MySession.Current.UserAccessLevelEntity.isAdd.ToString());
            //isAdd.Expires = DateTime.Now.AddDays(10);
            //HttpCookie isEdit = new HttpCookie("isEdit", MySession.Current.UserAccessLevelEntity.isEdit.ToString());
            //isAdd.Expires = DateTime.Now.AddDays(10);
            //HttpCookie isDelete = new HttpCookie("isDelete", MySession.Current.UserAccessLevelEntity.isDelete.ToString());
            //isAdd.Expires = DateTime.Now.AddDays(10);
            //HttpContext.Current.Request.Cookies.Add(isAdd);
            //HttpContext.Current.Request.Cookies.Add(isEdit);
            //HttpContext.Current.Request.Cookies.Add(isDelete);
            //HttpContext.Current.Request.Cookies.Add(isView);
        } catch (Exception) {
            blnResult = false;
        }
        return blnResult;
    }

    


//    public static void WriteActivityLog(ENT.Enumration.AppLogType Logtype, string strMessage)
//    {
//        ENT.UserActivityLog modal = new ENT.UserActivityLog();
//        BAL.UserActivityLog objBAL = new BAL.UserActivityLog();
//        try
//        {
//            modal.ual_logtype = Logtype;
//            modal.ual_logtext = strMessage;
//            modal.EntryMode = Truesys.Framework.Common.Enumration.EntryMode.ADD;
//            objBAL.Insert(modal);
//        }
//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
//        catch (Exception ex)
//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
//        {
//        }
//    }

//    public static void WriteSystemAlert(ENT.Enumration.AlertCategory Category, ENT.Enumration.AlertType Type, string strSubject, string strMessage)
//    {
//        ENT.SystemAlerts modal = new ENT.SystemAlerts();
//        BAL.SystemAlerts objBAL = new BAL.SystemAlerts();
//        modal.EntryMode = Truesys.Framework.Common.Enumration.EntryMode.ADD;
//        modal.msg_category = Category;
//        modal.msg_type = Type;
//        modal.msg_subject = strSubject;
//        modal.msg_message = strMessage;
//        try
//        {
//            objBAL.Insert(modal);
//        }
//        catch (Exception)
//        {
//        }
//    }

//    public static void WriteSystemAlert(ENT.Enumration.AlertCategory Category, ENT.Enumration.AlertType Type, string strSubject, string strMessage, string strUserid)
//    {
//        try
//        {
//            ENT.SystemAlerts modal = new ENT.SystemAlerts();
//            BAL.SystemAlerts objBAL = new BAL.SystemAlerts();
//            modal.EntryMode = Truesys.Framework.Common.Enumration.EntryMode.ADD;
//            modal.msg_userid = new Guid(strUserid);
//            modal.msg_category = Category;
//            modal.msg_type = Type;
//            modal.msg_subject = strSubject;
//            modal.msg_message = strMessage;
//            objBAL.Insert(modal);
//        }
//        catch (Exception)
//        {
//        }
//    }

}






public class CsvExport<T> where T : class
{
    public List<T> Objects;

    public CsvExport(List<T> objects)
    {
        Objects = objects;
    }

    public string Export()
    {
        return Export(true);
    }

    public string Export(bool includeHeaderLine)
    {

        StringBuilder sb = new StringBuilder();
        //Get properties using reflection.
        IList<PropertyInfo> propertyInfos = typeof(T).GetProperties();

        if (includeHeaderLine)
        {
            //add header line.
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                sb.Append(propertyInfo.Name).Append(",");
            }
            sb.Remove(sb.Length - 1, 1).AppendLine();
        }

        //add value for each property.
        foreach (T obj in Objects)
        {
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                sb.Append(MakeValueCsvFriendly(propertyInfo.GetValue(obj, null))).Append(",");
            }
            sb.Remove(sb.Length - 1, 1).AppendLine();
        }

        return sb.ToString();
    }

    //export to a file.
    public void ExportToFile(string path)
    {
        File.WriteAllText(path, Export());
    }

    //export as binary data.
    public byte[] ExportToBytes()
    {
        return Encoding.UTF8.GetBytes(Export());
    }

    //get the csv value for field.
    private string MakeValueCsvFriendly(object value)
    {
        if (value == null) return "";
        if (value is Nullable && ((INullable)value).IsNull) return "";

        if (value is DateTime)
        {
            if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                return ((DateTime)value).ToString("yyyy-MM-dd");
            return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
        }
        string output = value.ToString();

        if (output.Contains(",") || output.Contains("\""))
            output = '"' + output.Replace("\"", "\"\"") + '"';

        return output;

    }
}