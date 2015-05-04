using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baud.Deployment.Web.Framework.Web
{
    public class SimpleHtmlAttributes : System.Collections.Generic.Dictionary<string, object>
    {
        public SimpleHtmlAttributes()
        {
        }

        public SimpleHtmlAttributes(string attribute, object value)
        {
            this.Add(attribute, value);
        }

        public SimpleHtmlAttributes With(string attribute, object value)
        {
            this[attribute] = value;
            return this;
        }

        public SimpleHtmlAttributes WithClass(string value)
        {
            this["class"] = value;
            return this;
        }
    }
}