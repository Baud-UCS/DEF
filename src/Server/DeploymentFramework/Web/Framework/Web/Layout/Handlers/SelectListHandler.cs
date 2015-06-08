using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChameleonForms.Component.Config;
using ChameleonForms.Enums;
using ChameleonForms.FieldGenerators;
using ChameleonForms.FieldGenerators.Handlers;

namespace Baud.Deployment.Web.Framework.Web.Layout.Handlers
{
    public static class SelectListHandler
    {
        public const string SelectListControlName = "SelectList";
    }

    public class SelectListHandler<TModel, T> : FieldGeneratorHandler<TModel, T>
    {
        public SelectListHandler(IFieldGenerator<TModel, T> fieldGenerator)
            : base(fieldGenerator)
        {
        }

        public override bool CanHandle()
        {
            return true;
        }

        public override IHtmlString GenerateFieldHtml(ChameleonForms.Component.Config.IReadonlyFieldConfiguration fieldConfiguration)
        {
            var itemsSource = fieldConfiguration.GetBagData<IEnumerable>("ItemsSource");
            var selectedValue = FieldGenerator.GetValue();
            IEnumerable<SelectListItem> selectList = new SelectList(itemsSource, "ID", "Text", selectedValue);

            var optionLabel = fieldConfiguration.Bag.OptionLabel as string;
            if (optionLabel != null)
            {
                selectList = new[] { new SelectListItem { Value = "", Text = optionLabel } }.Concat(selectList);
            }

            return GetSelectListHtml(selectList, FieldGenerator, fieldConfiguration);
        }

        public override ChameleonForms.Enums.FieldDisplayType GetDisplayType(ChameleonForms.Component.Config.IReadonlyFieldConfiguration fieldConfiguration)
        {
            return fieldConfiguration.DisplayType == FieldDisplayType.List ? FieldDisplayType.List : FieldDisplayType.DropDown;
        }

        public override void PrepareFieldConfiguration(IFieldConfiguration fieldConfiguration)
        {
            // There is a bug in the unobtrusive validation for numeric fields that are a radio button
            //  when there is a radio button for "no value selected" i.e. value="" then it can't be selected
            //  as an option since it tries to validate the empty string as a number.
            // This turns off unobtrusive validation in that circumstance
            if (fieldConfiguration.DisplayType == FieldDisplayType.List && !FieldGenerator.Metadata.IsRequired && IsNumeric(FieldGenerator) && !HasMultipleValues(FieldGenerator))
            {
                fieldConfiguration.Attr("data-val", "false");
            }

            // If a list is being displayed there is no element for the label to point to so drop it
            if (fieldConfiguration.DisplayType == FieldDisplayType.List)
            {
                fieldConfiguration.WithoutLabel();
            }
        }
    }
}