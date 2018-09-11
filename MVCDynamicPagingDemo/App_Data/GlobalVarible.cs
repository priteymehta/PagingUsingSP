using System;
using System.Collections.Generic;
using ENT = CRM.Framework.Entity;
using BAL = CRM.Framework.Logic;

public static class GlobalVarible
{

    public static string GetMessage()
    {
        try
        {
            if (MySession.Current.MessageResult.Message.Count != 0)
            {
                string strResult = "";
                if (!MySession.Current.MessageResult.isReadData)
                {
                    string errorlist = "<ul>";
                    foreach (string str in MySession.Current.MessageResult.Message)
                    {
                        errorlist += string.Format("<li>{0}</li>", str);
                    }
                    errorlist += "</ul>";
                    if (MySession.Current.MessageResult.EntryStatus)
                    {
                        strResult = string.Format("<div class='alert bg-green alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>{0}</div>", errorlist);
                    }
                    else
                    {
                        strResult = string.Format("<div class='alert bg-red alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>{0}</div>", errorlist);
                    }
                }
                return strResult;
            }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        finally
        {
            Clear();
        }
    }

    public static string GetMessageHTML()
    {
        try
        {
            if (MySession.Current.MessageResult.Message.Count != 0)
            {
                string strResult = "";
                if (!MySession.Current.MessageResult.isReadData)
                {
                    string errorlist = "<ul>";
                    foreach (string str in MySession.Current.MessageResult.Message)
                    {
                        errorlist += string.Format("<li>{0}</li>", str);
                    }
                    errorlist += "</ul>";
                    if (MySession.Current.MessageResult.EntryStatus)
                    {
                        strResult = string.Format("<div class='alert alert-success'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a>{0}</div>", errorlist);
                    }
                    else
                    {
                        strResult = string.Format("<div class='alert alert-danger'><a href ='#' class='close' data-dismiss='alert' aria-label='close'></a>{0}</div>", errorlist);
                    }
                }
                MySession.Current.MessageResult.Message.Clear();
                return strResult;
            }
            else { return string.Empty; }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static void Clear()
    {
        MySession.Current.MessageResult.isReadData = true;
        MySession.Current.MessageResult.EntryStatus = false;
        MySession.Current.MessageResult.Message.Clear();
    }

    public static void AddError(string Error)
    {
        MySession.Current.MessageResult.isReadData = false;
        MySession.Current.MessageResult.EntryStatus = false;
        MySession.Current.MessageResult.Message.Add(Error);
        MySession.Current.MessageResult.Messages = string.Join(Environment.NewLine, MySession.Current.MessageResult.Message);
        MySession.Current.MessageResult.MessageHtml = GetMessageHTML();
    }

    public static void AddMessage(string Error)
    {
        MySession.Current.MessageResult.isReadData = false;
        MySession.Current.MessageResult.EntryStatus = true;
        MySession.Current.MessageResult.Message.Add(Error);
        MySession.Current.MessageResult.Messages = string.Join(Environment.NewLine, MySession.Current.MessageResult.Message);
        MySession.Current.MessageResult.MessageHtml = GetMessageHTML();
    }
    
}
