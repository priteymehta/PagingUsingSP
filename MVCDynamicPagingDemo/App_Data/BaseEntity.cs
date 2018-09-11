using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FormResultEntity
{
    public bool EntryStatus { get; set; }

    public bool isExecption { get; set; }

    public List<string> Message { get; set; }

    public string Messages
    {
        //get
        //{
        //    string errorlist = "";
        //    foreach (string str in this.Message)
        //    {
        //        errorlist += str;
        //    }
        //    Message.Clear();
        //    return errorlist;

        //} 
        get; set;
    }

    public string MessageHtml
    {
        get; set;
        
    }

    public Guid ReturnID { get; set; }

    public bool isReadData { get; set; }

    public FormResultEntity()
    {
        this.Message = new List<string>();
    }
}

public class UserAccessLevelEntity {

    public UserAccessLevelEntity() {
        this.isView = false;
        this.isAdd = false;
        this.isEdit = false;
        this.isDelete = false;
    }

    public bool isView { get; set; }
    public bool isAdd { get; set; }
    public bool isEdit { get; set; }
    public bool isDelete { get; set; }
}


