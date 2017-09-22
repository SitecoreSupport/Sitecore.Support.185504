using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GenerateLink;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Sites;

namespace Sitecore.Support.Modules.EmailCampaign.Core.Pipelines.GenerateLink
{
  public class ExpandDynamicLink : GenerateLinkProcessor
  {
    public override void Process(GenerateLinkPipelineArgs args)
    {
      Assert.IsNotNull(args, "Arguments can't be null");
      Assert.IsNotNull(args.Url, "Url link can't be null");
      DynamicLink dynamicLink;
      if (args.Url.IndexOf("~/link.aspx?", StringComparison.InvariantCulture) >= 0 && DynamicLink.TryParse(args.Url, out dynamicLink))
      {
        UrlOptions defaultUrlOptions = new UrlOptions();
        defaultUrlOptions.SiteResolving = true;
       // defaultUrlOptions.Site = SiteContext.GetSite(args.WebsiteConfigurationName);
        using (new SiteContextSwitcher(SiteContext.GetSite(args.WebsiteConfigurationName)))
        {
          args.Url = LinkManager.GetItemUrl(new ItemUtilExt().GetItem(dynamicLink.ItemId), defaultUrlOptions);
        }

      }
    }
  }
}