using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Baud.Deployment.Web.Framework.Web.Layout;
using Baud.Deployment.Web.Framework.Web.Layout.Handlers;
using ChameleonForms;
using ChameleonForms.Component.Config;
using ChameleonForms.Enums;
using ChameleonForms.Templates;

namespace Baud.Deployment.Web.Framework.Web
{
    public static class LayoutExtensions
    {
        public static IForm<TModel, IFormTemplate> BeginCustomForm<TModel>(this HtmlHelper<TModel> helper, string action = "", FormMethod method = FormMethod.Post, HtmlAttributes htmlAttributes = null, EncType? enctype = null)
        {
            return new CustomForm<TModel, IFormTemplate>(helper, new CustomFormTemplate(), action, method, htmlAttributes, enctype);
        }

        public static IForm<TModel, IFormTemplate> BeginReadOnlyForm<TModel>(this HtmlHelper<TModel> helper, string action = "", FormMethod method = FormMethod.Post, HtmlAttributes htmlAttributes = null, EncType? enctype = null)
        {
            return new ReadOnlyForm<TModel, IFormTemplate>(helper, new ReadOnlyFormTemplate(), action, method, htmlAttributes, enctype);
        }

        public static IFieldConfiguration AsSearchbox(this IFieldConfiguration field)
        {
            field.SetCustomControlName(SearchBoxHandler.SearchBoxControlName);

            return field
                .HideRequiredHint();
        }

        public static IFieldConfiguration AsSelectList(this IFieldConfiguration field, IEnumerable itemsSource, string optionLabel = null)
        {
            if (itemsSource == null)
            {
                throw new ArgumentNullException("itemsSource");
            }

            field.SetCustomControlName(SelectListHandler.SelectListControlName);
            field.Bag.ItemsSource = itemsSource;
            field.Bag.OptionLabel = optionLabel;
            return field.AsSelector();
        }

        public static IFieldConfiguration AsSelector(this IFieldConfiguration field)
        {
            field.AddClass("selector");
            return field;
        }

        public static IFieldConfiguration AsDatePicker(this IFieldConfiguration fc)
        {
            return fc.AddClass("datepicker").WithFormatString(@"{0:d}");
        }

        public static IFieldConfiguration AsTextOnly(this IFieldConfiguration fc)
        {
            fc.SetCustomControlName(TextOnlyFieldHandler.TextOnlyControlName);
            fc.HideRequiredHint();
            return fc;
        }

        public static IFieldConfiguration HideRequiredHint(this IFieldConfiguration field)
        {
            field.Bag.HideRequiredHint = true;
            return field;
        }

        public static IFieldConfiguration DisplayAsInputGroup(this IFieldConfiguration field)
        {
            field.Bag.DisplayAsInputGroup = true;
            return field;
        }
    }
}