using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Truesys.Application.Admin
{
    public class CustomControllerClass : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string strControllerName = requestContext.HttpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            //switch (strControllerName)
            //{
            //    case "BoardMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.BoardMaster);
            //        break;
            //    case "ChapterMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.ChapterMaster);
            //        break;
            //    case "ClassMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.ClassMaster);
            //        break;
            //    case "CoinContentPrice":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.ContentPriceMaster);
            //        break;
            //    case "CointMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.CoinMaster);
            //        break;
            //    case "MediumMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.MediumMaster);
            //        break;
            //    case "QuestionManagement":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.BoardQuestionManage);
            //        break;
            //    case "SectionMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.SectionMaster);
            //        break;
            //    case "SubjectMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.SubjectMaster);
            //        break;
            //    case "TopicMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.TopicMaster);
            //        break;
            //    case "YearMaster":
            //        MySession.Current.UserAccessLevelEntity = User.GetUserAccessLevel(Framework.Entity.Enumration.MasterType.YearMaster);
            //        break;
            //}
        }
    }
}