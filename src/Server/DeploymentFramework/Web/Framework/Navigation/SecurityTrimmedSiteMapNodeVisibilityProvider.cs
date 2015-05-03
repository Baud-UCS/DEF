using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baud.Deployment.Web.Framework.Navigation
{
    public class SecurityTrimmedSiteMapNodeVisibilityProvider : MvcSiteMapProvider.SiteMapNodeVisibilityProviderBase
    {
        public override bool AppliesTo(string providerName)
        {
            return providerName == "SecurityTrimmedVisibilityProvider";
        }

        public override bool IsVisible(MvcSiteMapProvider.ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            return node.Clickable || node.HasChildNodes;
        }
    }
}