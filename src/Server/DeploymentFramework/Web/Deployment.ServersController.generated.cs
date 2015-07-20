// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
using Baud.Deployment.Web.Framework.Security;
namespace Baud.Deployment.Web.Areas.Deployment.Controllers
{
    public partial class ServersController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ServersController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Index()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Detail()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detail);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult EditParameters()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditParameters);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult AddParameter()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddParameter);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ServersController Actions { get { return MVC.Deployment.Servers; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Deployment";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Servers";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Servers";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Detail = "Detail";
            public readonly string Edit = "Edit";
            public readonly string Add = "Add";
            public readonly string EditParameters = "EditParameters";
            public readonly string AddParameter = "AddParameter";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Detail = "Detail";
            public const string Edit = "Edit";
            public const string Add = "Add";
            public const string EditParameters = "EditParameters";
            public const string AddParameter = "AddParameter";
        }


        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Add = "Add";
                public readonly string Detail = "Detail";
                public readonly string Edit = "Edit";
                public readonly string EditParameters = "EditParameters";
                public readonly string Index = "Index";
            }
            public readonly string Add = "~/Areas/Deployment/Views/Servers/Add.cshtml";
            public readonly string Detail = "~/Areas/Deployment/Views/Servers/Detail.cshtml";
            public readonly string Edit = "~/Areas/Deployment/Views/Servers/Edit.cshtml";
            public readonly string EditParameters = "~/Areas/Deployment/Views/Servers/EditParameters.cshtml";
            public readonly string Index = "~/Areas/Deployment/Views/Servers/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ServersController : Baud.Deployment.Web.Areas.Deployment.Controllers.ServersController
    {
        public T4MVC_ServersController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Baud.Deployment.Web.Areas.Deployment.Models.Servers.IndexFilter filter, Baud.Deployment.Web.Framework.Web.PagingData paging);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index(Baud.Deployment.Web.Areas.Deployment.Models.Servers.IndexFilter filter, Baud.Deployment.Web.Framework.Web.PagingData paging)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "filter", filter);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "paging", paging);
            IndexOverride(callInfo, filter, paging);
            return callInfo;
        }

        [NonAction]
        partial void DetailOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Detail(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Detail);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DetailOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, System.Web.Mvc.FormCollection form);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int id, System.Web.Mvc.FormCollection form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            EditOverride(callInfo, id, form);
            return callInfo;
        }

        [NonAction]
        partial void AddOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Add()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Add);
            AddOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void AddOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Web.Mvc.FormCollection form);

        [NonAction]
        public override System.Web.Mvc.ActionResult Add(System.Web.Mvc.FormCollection form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Add);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            AddOverride(callInfo, form);
            return callInfo;
        }

        [NonAction]
        partial void EditParametersOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int serverID, System.Web.Mvc.FormCollection form);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditParameters(int serverID, System.Web.Mvc.FormCollection form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditParameters);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "serverID", serverID);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            EditParametersOverride(callInfo, serverID, form);
            return callInfo;
        }

        [NonAction]
        partial void EditParametersOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int serverID);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditParameters(int serverID)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditParameters);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "serverID", serverID);
            EditParametersOverride(callInfo, serverID);
            return callInfo;
        }

        [NonAction]
        partial void AddParameterOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int serverID, System.Web.Mvc.FormCollection form);

        [NonAction]
        public override System.Web.Mvc.ActionResult AddParameter(int serverID, System.Web.Mvc.FormCollection form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddParameter);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "serverID", serverID);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            AddParameterOverride(callInfo, serverID, form);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
