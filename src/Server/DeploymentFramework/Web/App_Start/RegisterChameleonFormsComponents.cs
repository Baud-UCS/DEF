using System;
using System.Web.Mvc;
using ChameleonForms;
using ChameleonForms.ModelBinders;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Baud.Deployment.Web.App_Start.RegisterChameleonFormsComponents), "Start")]

namespace Baud.Deployment.Web.App_Start
{
    public static class RegisterChameleonFormsComponents
    {
        public static void Start()
        {
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            FormTemplate.Default = new ChameleonForms.Templates.TwitterBootstrap3.TwitterBootstrapFormTemplate();
        }
    }
}